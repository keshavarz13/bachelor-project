using System.Collections.Generic;
using System.Threading.Tasks;
using Social.Controller.Contracts;

namespace Social.Services
{
    public interface IReadService
    {
        Task<ReadOutputDto> AddReadStatus(ReadInputDto inputDto);
        Task<ReadOutputDto> UpdateReadStatus(ReadInputDto inputDto);
        Task<List<EnumOutputDto>> GetReadStatuses();
        Task<List<ReadOutputDto>> GetUserReads(int uun);
        Task<List<ReadOutputDto>> GetBookReads(int bookId);
    }
}