using Associations.Common.DTOs;
using Associations.Common.UrlQueries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Associations.BusinessLogic.Interfaces
{
    public interface IWordRelsService
    {
        Task<IEnumerable<WordRelsDTO>> GetAllEntitiesAsync();
        Task<List<WordRelsToListDTO>> GetEntityByMainWordIdAsync(int id, PaginationUrlQuery paginationUrlQuery = null);
        Task<WordRelsDTO> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null);
        int TotalRecords { get; }
    }
}
