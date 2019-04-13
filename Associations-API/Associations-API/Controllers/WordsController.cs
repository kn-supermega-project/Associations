using Associations.BusinessLogic.Interfaces;
using AssociationsAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("Main")]
        public virtual async Task<ActionResult<IEnumerable<WordsDTO>>> GetMain()
        {
            var dtos = await _wordsService.GetMainEntitiesAsync();
            if (!dtos.Any())
            {
                return NoContent();
            }

            return Ok(dtos);
        }

    }
}
