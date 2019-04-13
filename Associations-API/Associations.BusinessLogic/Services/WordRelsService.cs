using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.UrlQueries;
using Associations.DataAccess.Entity;
using Associations.DataAccess.Interfaces;
using AutoMapper;
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

        public async Task<IEnumerable<WordRelsDTO>> GetMainEntitiesAsync()
        {
            Expression<Func<Words, bool>> filter = e => true;
            var entities = await _uow.WordRelsRepository.GetRangeAsync(
                 include: r => r.Include(o => o.word),
                 filter: f => f.WordId == f.word.Id);


            var dtos = _mapper.Map<List<WordRels>, List<WordRelsDTO>>(entities);
            return dtos;
        }

        public Task<IEnumerable<WordRelsDTO>> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null)
        {
            throw new NotImplementedException();
        }
    }
}
