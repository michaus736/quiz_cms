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
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepository;

        public TagService(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task CreateTag(Tag tag)
        {
            await _tagRepository.InsertAsync(tag);
        }

        public async Task DeleteTag(int id)
        {
            await _tagRepository.DeleteAsync(id);
        }

        public async Task<ResultWithModel<Tag>> GetTag(int id)
        {
            var tag = await _tagRepository.GetAsync(id);

            if(tag is null)
                throw new ArgumentNullException($"tag #{id} not found");

            return ResultWithModel<Tag>.Ok(tag);
        }

        public async Task<ResultWithModel<IEnumerable<Tag>>> GetTags()
        {
            var tags = await _tagRepository.GetAll()
                .OrderBy(x => x.Id)
                .ToListAsync();

            if(tags is null)
                throw new ArgumentNullException(nameof(tags));

            return ResultWithModel<IEnumerable<Tag>>.Ok(tags);
        }

        public async Task<ResultWithModel<IEnumerable<Tag>>> GetTagsForQuiz(int quizId)
        {
            var tags = await _tagRepository.GetAll()
                .Include(x => x.Quizzes)
                .Where(x => x.Id == quizId)
                .OrderBy(x => x.Id)
                .ToListAsync();

            if(tags is null)
                throw new ArgumentNullException($"tags for quiz #{quizId} not found");

            return ResultWithModel<IEnumerable<Tag>>.Ok(tags);
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagRepository.UpdateAsync(tag);
        }
    }
}
