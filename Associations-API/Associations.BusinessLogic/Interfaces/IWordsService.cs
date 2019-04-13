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
        Task<IEnumerable<WordsToListDTO>> GetMainEntitiesAsync();
        Task<IEnumerable<WordsDTO>> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null);
    }
}
