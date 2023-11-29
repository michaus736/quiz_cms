using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result> CreateCategory(CategoryResponse category)
        {
            await _categoryRepository.InsertAsync(category);

            return Result.Ok();
        }

        public async Task<Result> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteAsync(id);

            return Result.Ok();
        }

        public async Task<ResultWithModel<IEnumerable<CategoryResponse>>> GetCategories()
        {
            var categories = await _categoryRepository
                .GetAll()
                .OrderBy(x=>x.Id)
                .ToListAsync();

            if(categories is null)
                throw new ArgumentNullException(nameof(categories));

            return ResultWithModel<IEnumerable<CategoryResponse>>.Ok(categories.ConvertCollection().ToList());
        }

        public async Task<ResultWithModel<CategoryResponse>> GetCategory(int categoryId)
        {
            var category = await _categoryRepository.GetAsync(categoryId);

            if(category is null)
                throw new ArgumentNullException($"category #{categoryId} not found");

            return ResultWithModel<CategoryResponse>.Ok(category.Convert());
        }

        public async Task<Result> UpdateCategory(CategoryResponse category)
        {
            await _categoryRepository.UpdateAsync(category);

            return Result.Ok();
        }
    }
}
