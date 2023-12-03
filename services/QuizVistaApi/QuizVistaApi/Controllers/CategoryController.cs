using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System.Collections.Generic;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ResultWithModel<IEnumerable<CategoryResponse>>> GetCategories()
        {
            return await _categoryService.GetCategories();
        }

        [HttpPost("create")]
        public async Task<Result> CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            return await _categoryService.CreateCategory(categoryRequest);
        }

        [HttpPut("edit")]
        public async Task<Result> EditCategory([FromBody] CategoryRequest categoryRequest)
        {
            return await _categoryService.UpdateCategory(categoryRequest);
        }

        [HttpDelete("delete")]
        public async Task<Result> DeleteCategory([FromBody] CategoryRequest categoryRequest)
        {
            return await _categoryService.DeleteCategory(categoryRequest.Id);
        }













    }
}
