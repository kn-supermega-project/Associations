﻿using Associations.BusinessLogic.Interfaces;
using Associations.Common.DTOs;
using Associations.Common.Pagination;
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
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<WordsDTO>>> Get()
        {
            var dtos = await _wordsService.GetAllEntitiesAsync();
            if (!dtos.Any())
            {
                return NoContent();
            }

            return Ok(dtos);
        }
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
        [HttpGet("main")]
        public virtual async Task<ActionResult<IEnumerable<WordsDTO>>> Get([FromQuery]PaginationUrlQuery paginationUrlQuery = null)
        {
            var dtos = await _wordsService.GetMainEntitiesAsync(paginationUrlQuery);
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

    }
}
