using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class TagExtensions
    {

        public static TagResponse Convert(this Tag tag)
        {
            return new TagResponse(
                tag.Id,
                tag.Name,
                tag.Quizzes.ConvertCollection().ToList()
            );
        }

        public static IEnumerable<TagResponse> ConvertCollection(this IEnumerable<Tag> tags)
        {
            return tags.Select(Convert) ?? new List<TagResponse>();
        }

    }
}
