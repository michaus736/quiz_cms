using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class AttemptRequest
    {
        public int UserId { get; set; }
        public List<AnswerRequest> Answers { get; set; }

        public AttemptRequest()
        {
            Answers = new List<AnswerRequest>();
        }

        public AttemptRequest(int userId, List<AnswerRequest> answers)
        {
            UserId = userId;
            Answers = answers ?? new List<AnswerRequest>();
        }
    }
}
