using AssociationsAPI.DTOs;
using AssociationsAPI.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordsRepository _repo;
        private readonly IMapper _mapper;

        public WordsController(IMapper mapper, IWordsRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetWords()
        {
            var words = await _repo.GetWords();
            var wordsToReturn = _mapper.Map<IEnumerable<WordsDTO>>(words);

            return Ok(wordsToReturn);
        }
    }
}
