using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quality.Common.Dto.Genre;
using Quality.Service;

namespace Quality.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _genreService.All();
            return new JsonResult(result);
        }

        [HttpPost]
        [IsAdmin]
        public async Task<IActionResult> Post([FromBody] NewGenreDto data)
        {
            var result = await _genreService.Create(data);
            return new JsonResult(result);
        }
    }
}