using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class QuizRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? CmsTitleStyle { get; set; }
        public int AuthorId { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();

        public QuizRequest() { }

        public QuizRequest(int id, string name, string? description, int categoryId, string? cmsTitleStyle, int authorId, List<int> tagIds)
        {
            Id = id;
            Name = name;
            Description = description;
            CategoryId = categoryId;
            CmsTitleStyle = cmsTitleStyle;
            AuthorId = authorId;
            TagIds = tagIds;
        }
    }
}
