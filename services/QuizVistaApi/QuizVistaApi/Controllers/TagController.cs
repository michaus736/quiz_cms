using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ResultWithModel<IEnumerable<TagResponse>>> GetAll()
        {
            return await _tagService.GetTags();
        }

        [HttpGet("tag/{id}")]
        public async Task<ResultWithModel<TagResponse>> GetTag(int tagId)
        {
            return await _tagService.GetTag(tagId);
        }

        [HttpPost("create")]
        public async Task<Result> CreateTag([FromBody] TagRequest tagRequest)
        {
            return await _tagService.CreateTag(tagRequest);
        }

        [HttpPut("eidt")]
        public async Task<Result> EditTag([FromBody] TagRequest tagRequest)
        {
            return await _tagService.UpdateTag(tagRequest);
        }

        [HttpDelete("delete")]
        public async Task<Result> DeleteTag([FromBody] TagRequest tagRequest)
        {
            return await _tagService.DeleteTag(tagRequest.Id);
        }




    }
}
