using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.Extensions;
using Associations.Common.RequestModels;
using Associations.Common.UrlQueries;
using Associations.DataAccess.Entity;
using Associations.DataAccess.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Associations.BusinessLogic.Services
{
    public class WordRelsService : IWordRelsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public int TotalRecords
        {
            get { return _uow.WordRelsRepository.TotalRecords; }
        }

        public WordRelsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordRelsDTO>> GetAllEntitiesAsync()
        {
            var entities = await _uow.WordRelsRepository.GetAllEntitiesAsync();

            var dtos = _mapper.Map<List<WordRels>, List<WordRelsDTO>>(entities);

            return dtos;
        }
        public async Task<WordRelsToListDTO> GetEntityByIdAsync(int id)
        {
            var entity = await _uow.WordRelsRepository.GetEntityAsync(id);

            var dtos = _mapper.Map<WordRels, WordRelsToListDTO>(entity);

            return dtos;

        }

        public async Task<List<WordRelsToListDTO>> GetEntityByMainWordIdAsync(int id, [FromQuery]PaginationUrlQuery paginationUrlQuery = null)
        {
            var entity = await _uow.WordRelsRepository.GetRangeAsync(
                 include: r => r.Include(e => e.RelWord),
                 filter: r => r.WordId == id,
                 paginationUrlQuery: paginationUrlQuery);

            if (entity == null) return null;

            var dto = _mapper.Map<List<WordRels>, List<WordRelsToListDTO>>(entity);

            return dto;
        }


        public async Task<WordRelsDTO> CreateEntityAsync(RelRequestModel modelRequest)
        {
            var entity = _mapper.Map<RelRequestModel, WordRels>(modelRequest);

            entity = await _uow.WordRelsRepository.CreateEntityAsync(entity);
            var result = await _uow.SaveAsync();
            if (!result)
            {
                return null;
            }

            if (entity == null) return null;

            var dto = _mapper.Map<WordRels, WordRelsDTO>(entity);

            return dto;
        }
        Task<WordRelsDTO> IWordRelsService.GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.WordRelsRepository.DeleteAsync(id);

            var result = await _uow.SaveAsync();

            return result;
        }
        public async Task<bool> UpdateEntityByIdAsync(RelRequestModel modelRequest, int id)
        {
            var entity = _mapper.Map<RelRequestModel, WordRels>(modelRequest);
            entity.Id = id;

            var updated = await _uow.WordRelsRepository.UpdateAsync(entity);
            var result = await _uow.SaveAsync();

            return result;
        }
        public async Task<IEnumerable<WordRelsToListDTO>> GetFilteredEntitiesAsync(
        int id,
        string searchingUrlQuery = null,
        PaginationUrlQuery paginationUrlQuery = null)
        {
            Expression<Func<WordRels, bool>> filter = e => true;

            filter = filter.And(e => e.WordId == id);

            if (!string.IsNullOrEmpty(searchingUrlQuery))
            {
                filter = filter.And(e => e.RelWord.Word.Contains(searchingUrlQuery));
            }

            var entities = await _uow.WordRelsRepository.GetRangeAsync(
                include: r => r.Include(e => e.RelWord),
                filter: filter,
                paginationUrlQuery: paginationUrlQuery);

            var dtos = _mapper.Map<List<WordRels>, List<WordRelsToListDTO>>(entities);

            return dtos;
        }
    }
}
