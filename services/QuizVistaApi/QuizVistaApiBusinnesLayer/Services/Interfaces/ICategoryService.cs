using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<Category>>> GetCategories();
        Task<ResultWithModel<Category>> GetCategory(int categoryId);

        Task DeleteCategory(int id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);


        #endregion
    }
}
