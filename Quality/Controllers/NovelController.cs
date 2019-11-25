using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quality.Common.Dto.Novel;
using Quality.Service;

namespace Quality.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovelController : ControllerBase
    {
        private readonly INovelService _novelService;
        private readonly IChapterService _chapterService;

        public NovelController(INovelService novelService,
            IChapterService chapterService)
        {
            _novelService = novelService;
            _chapterService = chapterService;
        }

        [HttpPost]
        //[MyAuthorize]
        [IsAdmin]
        public async Task<IActionResult> Post([FromBody] NewNovelDto data)
        {
            var result = await _novelService.Create(data);

            return new JsonResult(result);
        }

        [HttpGet]
        //[MyAuthorize]
        public async Task<IActionResult> Get()
        {
            var result = await _novelService.All();
            return new JsonResult(result);
        }

        [HttpGet]
        //[MyAuthorize]
        [Route("{id}/chapters")]
        public async Task<IActionResult> GetChapters(int id)
        {
            var result = await _chapterService.ByNovelId(id);
            return new JsonResult(result);
        }
    }
}