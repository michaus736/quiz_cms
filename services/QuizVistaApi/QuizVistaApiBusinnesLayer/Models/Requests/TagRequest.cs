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

        public string Name { get; set; } = string.Empty;

        public virtual List<QuizResponse> Quizzes { get; set; } = new List<QuizResponse>();

        private TagRequest() { }

        public TagRequest(string name, List<QuizResponse> quizzes)
        {
            Name = name;
            Quizzes = quizzes;
        }


    }
}
