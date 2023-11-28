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

        Task<ResultWithModel<IEnumerable<Tag>>> GetTags();
        Task<ResultWithModel<IEnumerable<Tag>>> GetTagsForQuiz(int quizId);
        Task<ResultWithModel<Tag>> GetTag(int id);
        Task CreateTag(Tag tag);
        Task DeleteTag(int id);
        Task UpdateTag(Tag tag);

        #endregion
    }
}
