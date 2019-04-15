using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.Pagination;
using Associations.Common.UrlQueries;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordRelsController : Controller
    {
        private readonly IWordRelsService _wordRelsService;

        public WordRelsController(IWordRelsService wordRelsService)
        {
            _wordRelsService = wordRelsService;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<WordRelsToListDTO>> GetByMainWordId(int id, [FromQuery]PaginationUrlQuery paginationUrlQuery = null)
        {
            var dto = await _wordRelsService.GetEntityByMainWordIdAsync(id, paginationUrlQuery);
            if (dto == null)
            {
                return NotFound();
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = paginationUrlQuery.PageNumber,
                PageSize = paginationUrlQuery.PageSize,
                TotalRecords = _wordRelsService.TotalRecords
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pageInfo));

            return Ok(dto);
        }
    }
}
