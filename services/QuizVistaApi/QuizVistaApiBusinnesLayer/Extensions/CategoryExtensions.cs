using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class CategoryExtensions
    {


        public static CategoryResponse Convert(this Category category)
        {
            return new CategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.Quizzes.ConvertCollection().ToList()
            );
        }

        public static IEnumerable<CategoryResponse> ConvertCollection(this IEnumerable<Category> categories)
        {
            return categories.Select(Convert) ?? new List<CategoryResponse>();
        }

    }
}
