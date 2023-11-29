using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class QuizExtensions
    {

        public static QuizResponse ToResponse(this Quiz quiz)
        {
            return new QuizResponse(
                quiz.Id,
                quiz.Name,
                quiz.Description,
                quiz.CreationDate,
                quiz.EditionDate,
                quiz.CategoryId,
                quiz.CmsTitleStyle,
                quiz.UserId,
                quiz.Category.ToResponse(),
                quiz.Questions.ToCollectionResponse().ToList(),
                quiz.User.ToResponse(),
                quiz.Tags.ToCollectionResponse().ToList(),
                quiz.Users.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<QuizResponse> ToCollectionResponse(this IEnumerable<Quiz> quizes) {
            return quizes.Select(ToResponse) ?? new List<QuizResponse>();
        }


    }
}
