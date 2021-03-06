﻿using Associations.Common.DTOs;
using Associations.Common.RequestModels;
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
        Task<WordRelsToListDTO> GetEntityByIdAsync(int id);
        Task<List<WordRelsToListDTO>> GetEntityByMainWordIdAsync(int id, PaginationUrlQuery paginationUrlQuery = null);
        Task<WordRelsDTO> GetRangeOfEntitiesAsync(PaginationUrlQuery urlQuery = null);
        int TotalRecords { get; }
        Task<WordRelsDTO> CreateEntityAsync(RelRequestModel modelRequest);
        Task<bool> UpdateEntityByIdAsync(RelRequestModel modelRequest, int id);
        Task<bool> DeleteEntityByIdAsync(int id);
        Task<IEnumerable<WordRelsToListDTO>> GetFilteredEntitiesAsync(
        int id,
        string searchingUrlQuery = null,
        PaginationUrlQuery paginationUrlQuery = null);
    }
}
