using System.Collections.Generic;
using System.Threading.Tasks;
using Social.Controller.Contracts;
using Social.Models;

namespace Social.Services
{
    public interface IFollowService
    {
        Task<List<UserReportOutputDto>> GetFollowers(int uun);
        Task<List<UserReportOutputDto>> GetFollowings(int uun);
        Task<Follow> Follow(int usrUun, int followedUun);
        void UnFollow(int usrUun, int followedUun);
    }
}