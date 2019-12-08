using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.Extensions;
using Associations.Common.RequestModels;
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

        public int TotalRecords
        {
            get { return _uow.WordsRepository.TotalRecords; }
        }

        public WordsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WordsDTO>> GetAllEntitiesAsync(PaginationUrlQuery paginationUrlQuery = null)
        {
            var entities = await _uow.WordsRepository.GetRangeAsync(
                include: r => r.Include(o => o.Rels),
                paginationUrlQuery: paginationUrlQuery);

            var dtos = _mapper.Map<List<Words>, List<WordsDTO>>(entities);

            return dtos;

        }
        public async Task<WordsToListDTO> GetEntityByIdAsync(int id)
        {
            var entity = await _uow.WordsRepository.GetEntityAsync(id);

            var dtos = _mapper.Map<Words, WordsToListDTO>(entity);

            return dtos;

        }
        public async Task<IEnumerable<WordsToListDTO>> GetMainEntitiesAsync(PaginationUrlQuery paginationUrlQuery = null)
        {
            var entities = await _uow.WordsRepository.GetRangeAsync(
                filter: f => f.Type == 1, // 1 it's type of main words
                paginationUrlQuery: paginationUrlQuery
                );

            var dtos = _mapper.Map<List<Words>, List<WordsToListDTO>>(entities);

            return dtos;
        }
        public async Task<WordsDTO> CreateEntityAsync(WordRequestModel modelRequest)
        {
            var entity = _mapper.Map<WordRequestModel, Words>(modelRequest);

            entity = await _uow.WordsRepository.CreateEntityAsync(entity);
            var result = await _uow.SaveAsync();
            if (!result)
            {
                return null;
            }

            if (entity == null) return null;

            var dto = _mapper.Map<Words, WordsDTO>(entity);

            return dto;
        }

        public async Task<bool> UpdateEntityByIdAsync(WordRequestModel modelRequest, int id)
        {
            var entity = _mapper.Map<WordRequestModel, Words>(modelRequest);
            entity.Id = id;

            var updated = await _uow.WordsRepository.UpdateAsync(entity);
            var result = await _uow.SaveAsync();

            return result;
        }

        public Task<IEnumerable<WordsDTO>> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.WordsRepository.DeleteAsync(id);

            var result = await _uow.SaveAsync();

            return result;
        }

        public async Task<IEnumerable<WordsDTO>> GetFilteredEntitiesAsync(
        string searchingUrlQuery = null,
        PaginationUrlQuery paginationUrlQuery = null)
        {
            Expression<Func<Words, bool>> filter = e => true;

            if (!string.IsNullOrEmpty(searchingUrlQuery))
            {
                filter = filter.And(e => e.Word.Contains(searchingUrlQuery));

                int i;
                if (!Int32.TryParse(searchingUrlQuery, out i))
                {
                    i = -1;
                }
                if (i != -1 && i != 0)
                {
                    filter = filter.Or(e => e.Id == i);
                }
            }

            var entities = await _uow.WordsRepository.GetRangeAsync(
                include: r => r.Include(o => o.Rels),
                filter: filter,
                paginationUrlQuery: paginationUrlQuery);

            var dtos = _mapper.Map<List<Words>, List<WordsDTO>>(entities);

            return dtos;
        }

    }
}
