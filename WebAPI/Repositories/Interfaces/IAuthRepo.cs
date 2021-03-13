using Models.Dtos;
using Models.Models;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAuthRepo
    {
        Task<ServiceResponse<int>> RegisterUser(RegisterDto user, string password);
        Task<ServiceResponse<LoginResponse>> LoginUser(string email, string password);
        Task<ServiceResponse<LoginResponse>> LoginAdmin(string email, string password);
        Task<bool> EmailExists(string email);
        Task<IEnumerable<User>> getUsers();
    }
}
