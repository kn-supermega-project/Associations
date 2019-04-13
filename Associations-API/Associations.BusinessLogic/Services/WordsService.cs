using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.Extensions;
using Associations.Common.UrlQueries;
using Associations.DataAccess.Entity;
using Associations.DataAccess.Interfaces;
using AssociationsAPI.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Associations.BusinessLogic.Services
{
    public class WordsService : IWordsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public WordsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WordsDTO>> GetAllEntitiesAsync()
        {
            var entities = await _uow.WordsRepository.GetAllEntitiesAsync(
                include: r => r.Include(o => o.Rels));

            var dtos = _mapper.Map<List<Words>, List<WordsDTO>>(entities);

            return dtos;

        }
        public async Task<IEnumerable<WordsToListDTO>> GetMainEntitiesAsync()
        {
            var entities = await _uow.WordsRepository.GetRangeAsync(
                filter: f => f.Type == 1 // 1 it's type of main words
                );

            var dtos = _mapper.Map<List<Words>, List<WordsToListDTO>>(entities);

            return dtos;

        }

        public Task<IEnumerable<WordsDTO>> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null)
        {
            throw new NotImplementedException();
        }
    }
}
