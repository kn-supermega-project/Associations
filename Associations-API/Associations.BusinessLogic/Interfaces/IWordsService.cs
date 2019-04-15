using Associations.Common.DTOs;
using Associations.Common.UrlQueries;
using AssociationsAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Associations.BusinessLogic.Interfaces
{
    public interface IWordsService
    {
        Task<IEnumerable<WordsDTO>> GetAllEntitiesAsync();
        Task<WordsToListDTO> GetEntityByIdAsync(int id);
        Task<IEnumerable<WordsToListDTO>> GetMainEntitiesAsync(PaginationUrlQuery paginationUrlQuery = null);
        Task<IEnumerable<WordsDTO>> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null);
        int TotalRecords { get; }
    }
}
