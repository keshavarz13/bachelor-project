using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Social.Controller.Contracts;
using Social.Models;
using Social.Repository;

namespace Social.Services.Imp
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IFollowRepository _followRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IFollowService _followService;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public PostService(IPostRepository postRepository, IMapper mapper, IFollowRepository followRepository,
            IFollowService followService, IBookRepository bookRepository, IHttpClientFactory httpClientFactory)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _followRepository = followRepository;
            _followService = followService;
            _bookRepository = bookRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<PostOutputDto>> GetFeed(int uun)
        {
            var following = await _followService.GetFollowings(uun);
            var followingIds = following.Select(x => x.UserUniqueNumber).ToList();
            var result = _mapper.Map<List<PostOutputDto>>(await _postRepository.GetQueryableAsync()
                .Where(post => followingIds.Contains((int)post.CreatorUserId) && post.PostType != PostType.Comment)
                .OrderByDescending(post => post.Id).ToListAsync());
            foreach (var post in result)
            {
                post.RelatedBookName = (await _bookRepository.GetByIdAsync(post.RelatedBook)).Name;
                post.CreatorUserName = following.Single(x => x.UserUniqueNumber == post.CreatorUserId).UserName;
            }

            return result;
        }

        public async Task<PostOutputDto> SendPost(int uun, PostInputDto inputDto)
        {
            if (inputDto.PostType == PostType.Comment && inputDto.RelatedPost == null)
                throw new Exception("related post can not be null");
            if (inputDto.PostType != PostType.Comment && inputDto.RelatedBook == null)
                throw new Exception("related book can not be null");
            var post = _mapper.Map<Post>(inputDto);
            post.CreationTime = DateTime.Now;
            post.CreatorUserId = uun;
            return _mapper.Map<PostOutputDto>(await _postRepository.AddAsync(post));
        }

        public async Task<PostDetailsOutputDto> GetPostDetails(long id)
        {
            var mainPost = _mapper.Map<PostDetailsOutputDto>(await _postRepository.GetByIdAsync(id));
            mainPost.Comments = _mapper.Map<List<CommentOutputDto>>(await _postRepository.GetQueryableAsync()
                .Where(post => post.RelatedPost == id).OrderByDescending(post => post.Id).ToListAsync());
            var usrIds = mainPost.Comments.Select(comment => comment.CreatorUserId).ToList();
            usrIds.Add(mainPost.CreatorUserId);
            var userInfos = await GetUsersFromIdentityByUun(usrIds); 
            mainPost.RelatedBookName = (await _bookRepository.GetByIdAsync(mainPost.RelatedBook)).Name;
            mainPost.CreatorUserIdName = userInfos.First(usr => usr.UserUniqueNumber == mainPost.CreatorUserId).UserName;
            mainPost.Name = userInfos.First(usr => usr.UserUniqueNumber == mainPost.CreatorUserId).Name;
            foreach (var comment in mainPost.Comments){
                comment.CreatorUserName =
                    userInfos.First(usr => usr.UserUniqueNumber == comment.CreatorUserId).UserName;
                comment.Name =  userInfos.First(usr => usr.UserUniqueNumber == comment.CreatorUserId).Name;
            }
            
            return mainPost;
        }

        private async Task<List<UserReportOutputDto>> GetUsersFromIdentityByUun(List<long> uuns)
        {
            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(uuns);
            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            var response =
                await _httpClientFactory.CreateClient()
                    .PostAsync("http://localhost:5000/api/v1/user/bulk-users-by-uun", data);
            if (!response.IsSuccessStatusCode)
                throw new Exception("There is a problem with the identity service.");
            var contentAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserReportOutputDto>>(contentAsString);
        }
    }
}