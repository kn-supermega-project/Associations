﻿using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.Pagination;
using Associations.Common.RequestModels;
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
        public virtual async Task<ActionResult<WordsToListDTO>> Get(int id)
        {
            var dto = await _wordRelsService.GetEntityByIdAsync(id);
            if (dto == null)
            {
                return NoContent();
            }

            return Ok(dto);
        }

        [HttpGet("main/{id}")]
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
        [HttpGet("filtered/{id}")]
        public virtual async Task<ActionResult<IEnumerable<WordRelsToListDTO>>> Get(
                int id,
                [FromQuery]string searchString = null,
                [FromQuery]PaginationUrlQuery paginationUrlQuery = null)
        {
            var dtos = await _wordRelsService.GetFilteredEntitiesAsync(id,searchString, paginationUrlQuery);

            if (dtos == null)
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

            return Ok(dtos);
        }
        [HttpPost]
        public virtual async Task<ActionResult<WordRelsToListDTO>> Create([FromBody] RelRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dtos = await _wordRelsService.CreateEntityAsync(request);
            if (dtos == null)
            {
                return StatusCode(500);
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _wordRelsService.DeleteEntityByIdAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update([FromRoute]int id, [FromBody]RelRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _wordRelsService.UpdateEntityByIdAsync(request, id);
            if (!result)
            {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
