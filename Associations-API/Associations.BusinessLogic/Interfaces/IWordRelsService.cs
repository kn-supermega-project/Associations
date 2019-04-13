using Associations.Common.DTOs;
using Associations.Common.UrlQueries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Associations.BusinessLogic.Interfaces
{
    public interface IWordRelsService
    {
        Task<IEnumerable<WordRelsDTO>> GetAllEntitiesAsync();
        Task<IEnumerable<WordRelsDTO>> GetMainEntitiesAsync();
        Task<IEnumerable<WordRelsDTO>> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null);
    }
}
