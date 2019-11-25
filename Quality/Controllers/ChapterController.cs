using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quality.Common.Dto.NovelChapter;
using Quality.Service;

namespace Quality.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpPost]
        [IsAdmin]
        public async Task<IActionResult> Post([FromBody] NewChapterDto data)
        {
            var result = await _chapterService.Create(data);
            return new JsonResult(result);
        }
    }
}