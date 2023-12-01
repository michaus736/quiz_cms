using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class AttemptExtensions
    {

        public static AttemptResponse ToResponse(this Attempt attempt)
        {
            return new AttemptResponse
            (
                attempt.Id,
                attempt.CreateDate,
                attempt.EditionDate,
                attempt.UsersId,
                attempt.Answers.ToCollectionResponse().ToList(),
                attempt.IdNavigation.ToResponse(),
                attempt.AnswersNavigation.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<AttemptResponse> ToCollectionResponse(this IEnumerable<Attempt> attempts)
        {
            return attempts.Select(ToResponse) ?? new List<AttemptResponse>();
        }

        public static Attempt ToEntity(this AttemptRequest request)
        {
            var attempt = new Attempt
            {
                UserId = request.UserId,
                Answers = request.Answers.ConvertCollection().ToList()
            };

            return attempt;
        }

        public static List<Answer> ConvertCollection(this List<AnswerRequest> answerRequests)
        {
            return answerRequests.Select(a => a.ToEntity()).ToList();
        }
    }
}
