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
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public FollowService(IMapper mapper, IFollowRepository followRepository, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _followRepository = followRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<UserReportOutputDto>> GetFollowers(int uun)
        {
            var followerRows = await _followRepository.GetQueryableAsync().Where(x => x.Followed == uun)
                .OrderByDescending(x => x.Id).ToListAsync();
            var followerIds = followerRows.Select(x => x.Follower).ToList();
            return await GetUsersFromIdentityByUun(followerIds);
        }

        public async Task<List<UserReportOutputDto>> GetFollowings(int uun)
        {
            var followingRows = await _followRepository.GetQueryableAsync().Where(x => x.Follower == uun)
                .OrderByDescending(x => x.Id).ToListAsync();
            var followingIds = followingRows.Select(x => x.Followed).ToList();
            return await GetUsersFromIdentityByUun(followingIds);
        }

        public async Task<Follow> Follow(int usrUun, int followedUun)
        {
            var follow = new Follow()
            {
                Followed = followedUun,
                Follower = usrUun
            };
            return await _followRepository.AddAsync(follow);
        }

        public void UnFollow(int usrUun, int followedUun)
        {
            var follow = _followRepository.GetQueryableAsync()
                .SingleOrDefault(x => x.Follower == usrUun && x.Followed == followedUun);
            _followRepository.Remove(follow);
        }
        
        public FollowBasicInfoOutputDto BasicInfo(int usrUun)
        {
            return new FollowBasicInfoOutputDto
            {
                FollowersCount = _followRepository.GetQueryableAsync().Count(x => x.Follower == usrUun),
                FollowingsCount = _followRepository.GetQueryableAsync().Count(x => x.Followed == usrUun)
            };
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