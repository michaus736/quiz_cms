using QuizVistaApiBusinnesLayer.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class TagRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private TagRequest() { }

        public TagRequest(int id,
            string name)
        {
            Id = id;
            Name = name;
        }


    }
}
