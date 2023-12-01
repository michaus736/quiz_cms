using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class QuizRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? CmsTitleStyle { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();

        public QuizRequest() { }

        public QuizRequest(string name, string? description, int categoryId, string? cmsTitleStyle, int userId, List<int> tagIds)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            CmsTitleStyle = cmsTitleStyle;
            UserId = userId;
            TagIds = tagIds;
        }
    }
}
