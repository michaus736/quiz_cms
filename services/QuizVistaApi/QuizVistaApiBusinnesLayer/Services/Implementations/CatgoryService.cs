using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Models;
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
    public class CatgoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CatgoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategory(Category category)
        {
            await _categoryRepository.InsertAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<ModelWithResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepository
                .GetAll()
                .OrderBy(x=>x.CategoryId)
                .ToListAsync();

            if(categories is null)
                throw new ArgumentNullException(nameof(categories));

            return new ModelWithResult<IEnumerable<Category>>(categories);
        }

        public async Task<ModelWithResult<Category>> GetCategory(int categoryId)
        {
            var category = await _categoryRepository.GetAsync(categoryId);

            if(category is null)
                throw new ArgumentNullException(nameof(category));

            return new ModelWithResult<Category>(category);
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
