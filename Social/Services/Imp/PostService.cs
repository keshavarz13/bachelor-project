using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Social.Controller.Contracts;
using Social.Helper;
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

        public PostService(IPostRepository postRepository, IMapper mapper, IFollowRepository followRepository,
            IFollowService followService, IBookRepository bookRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _followRepository = followRepository;
            _followService = followService;
            _bookRepository = bookRepository;
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
                post.CreatorUserIdName = following.Single(x=> x.UserUniqueNumber == post.CreatorUserId).UserName;
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
    }
}