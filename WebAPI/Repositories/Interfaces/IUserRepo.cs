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
    public interface IUserRepo
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetDeletedUsers();
        Task<ServiceResponse<string>> SaveUser(UserDto userDto);
        Task<ServiceResponse<string>> RecoverUser(int id);
        Task<ServiceResponse<string>> UpdateUser(int id, UserDto userDto);
        Task<ServiceResponse<string>> DeleteUser(int id);
    }
}
