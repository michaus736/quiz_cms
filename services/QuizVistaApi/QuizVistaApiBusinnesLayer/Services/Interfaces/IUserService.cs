using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterUser(UserRequest request);
    }
}
