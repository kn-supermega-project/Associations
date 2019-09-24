using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.Pagination;
using Associations.Common.RequestModels;
using Associations.Common.UrlQueries;
using AssociationsAPI.DTOs;
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
    public class WordsController : Controller
    {
        private readonly IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }
        // GET: /words

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<WordsToListDTO>> Get(int id)
        {
            var dto = await _wordsService.GetEntityByIdAsync(id);
            if (dto == null)
            {
                return NoContent();
            }

            return Ok(dto);
        }
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<WordsDTO>>> Get([FromQuery]PaginationUrlQuery paginationUrlQuery = null)
        {
            var dtos = await _wordsService.GetAllEntitiesAsync(paginationUrlQuery);
            if (!dtos.Any())
            {
                return NoContent();
            }
            var pageInfo = new PageInfo()
            {
                PageNumber = paginationUrlQuery.PageNumber,
                PageSize = paginationUrlQuery.PageSize,
                TotalRecords = _wordsService.TotalRecords
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pageInfo));

            return Ok(dtos);
        }

        [HttpPost]
        public virtual async Task<ActionResult<WordsDTO>> Create([FromBody] WordRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dtos = await _wordsService.CreateEntityAsync(request);
            if (dtos == null)
            {
                return StatusCode(500);
            }

            return CreatedAtAction("GetById", new { id = dtos.Id }, dtos);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update([FromRoute]int id, [FromBody]WordRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _wordsService.UpdateEntityByIdAsync(request, id);
            if (!result)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

    }
}
