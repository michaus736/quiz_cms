using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class AssignUserRequest
    {
        public string UserName { get; set; }
        public int QuizId { get; set; }

        public AssignUserRequest() { }

        public AssignUserRequest(string userName, int quizId)
        {
            UserName = userName;
            QuizId = quizId;
        }
    }
}
