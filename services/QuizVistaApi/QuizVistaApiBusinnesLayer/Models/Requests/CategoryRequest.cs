using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public CategoryRequest() { }

        public CategoryRequest(string name, string? description) {  Name = name; Description = description; }

    }
}
