using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class AttemptRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<AnswerRequest> Answers { get; set; }

        public AttemptRequest()
        {
            Answers = new List<AnswerRequest>();
        }

        public AttemptRequest(int id, int userId, List<AnswerRequest> answers)
        {
            Id = id;
            UserId = userId;
            Answers = answers ?? new List<AnswerRequest>();
        }
    }
}
