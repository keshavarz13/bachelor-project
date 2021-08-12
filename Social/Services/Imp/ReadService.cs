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
using Social.Helper;
using Social.Models;
using Social.Repository;

namespace Social.Services.Imp
{
    public class ReadService : IReadService
    {
        private readonly IReadRepository _readRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBookRepository _bookRepository;

        public ReadService(IReadRepository readRepository, IMapper mapper, IHttpClientFactory httpClientFactory,
            IBookRepository bookRepository)
        {
            _readRepository = readRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _bookRepository = bookRepository;
        }

        public async Task<ReadOutputDto> AddReadStatus(ReadInputDto inputDto)
        {
            return _mapper.Map<ReadOutputDto>(await _readRepository.AddAsync(_mapper.Map<Read>(inputDto)));
        }

        public async Task<ReadOutputDto> UpdateReadStatus(ReadInputDto inputDto)
        {
            return _mapper.Map<ReadOutputDto>(await _readRepository.AddAsync(_mapper.Map<Read>(inputDto)));
        }

        public async Task<List<ReadOutputDto>> GetUserReads(int uun)
        {
            var originalReads = await _readRepository.GetQueryableAsync().Where(read => read.UserId == uun)
                .OrderByDescending(read => read.Id).ToListAsync();
            var reads = _mapper.Map<List<ReadOutputDto>>(originalReads);
            var usrIds = new List<long>();
            usrIds.Add(uun);
            var usr = (await GetUsersFromIdentityByUun(usrIds)).First();

            foreach (var read in reads)
            {
                read.UserName = usr.UserName;
                read.BookName = (await _bookRepository.GetByIdAsync(read.BookId)).Name;
                read.DateTime = originalReads.Single(x => x.Id == read.Id).LastModificationTime ;
                read.IsEdited = !(originalReads.Single(x => x.Id == read.Id).LastModificationTime ==
                                 originalReads.Single(x => x.Id == read.Id).CreationTime);
            }

            return reads;
        }

        public async Task<List<ReadOutputDto>> GetBookReads(int bookId)
        {
            var originalReads = await _readRepository.GetQueryableAsync().Where(read => read.BookId == bookId)
                .OrderByDescending(read => read.Id).ToListAsync();
            var reads = _mapper.Map<List<ReadOutputDto>>(originalReads);
            var usrIds = reads.Select(r => r.UserId).ToList();
            var usrs = await GetUsersFromIdentityByUun(usrIds);
            var bookName = (await _bookRepository.GetByIdAsync(bookId)).Name;
            foreach (var read in reads)
            {
                read.UserName = usrs.First(r => r.UserUniqueNumber == read.UserId).UserName;
                read.BookName = bookName;
                read.DateTime = originalReads.Single(x => x.Id == read.Id).LastModificationTime ;
                read.IsEdited = !(originalReads.Single(x => x.Id == read.Id).LastModificationTime ==
                                  originalReads.Single(x => x.Id == read.Id).CreationTime);
            }

            return reads;
        }
        
        public async Task<List<EnumOutputDto>> GetReadStatuses()
        {
            var result = new List<EnumOutputDto>();
            foreach (int i in Enum.GetValues(typeof(ReadStatus)))
            {
                result.Add(new EnumOutputDto
                {
                    Value = ((BookCategory)i).ToString(),
                    PersianTitle = EnumHelper.GetEnumDescription((BookCategory)i)
                });
            }

            return result;
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