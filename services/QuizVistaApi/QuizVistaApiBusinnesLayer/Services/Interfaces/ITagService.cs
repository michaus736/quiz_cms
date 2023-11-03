using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface ITagService
    {
        #region REST

        Task<ModelWithResult<IEnumerable<Tag>>> GetTags();
        Task<ModelWithResult<IEnumerable<Tag>>> GetTagsForQuiz(int quizId);
        Task<ModelWithResult<Tag>> GetTag(int id);
        Task CreateTag(Tag tag);
        Task DeleteTag(int id);
        Task UpdateTag(Tag tag);

        #endregion
    }
}
