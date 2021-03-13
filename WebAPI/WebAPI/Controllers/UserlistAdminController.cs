using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Models.Models;
using Models.Responses;
using Repositories;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class UserlistAdminController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUserRepo _userRepo;

        public UserlistAdminController(DataContext context, IUserRepo userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        [HttpGet("deleted-user-list")]
        public IEnumerable<User> GetDeletedUserlistAdmin()
        {
            return _userRepo.GetDeletedUsers(); ;
        }

        [HttpGet("user-list")]
        public IEnumerable<User> GetUserlistAdmin()
        {
            return _userRepo.GetUsers();
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> PostUserAdmin(UserDto userDto)
        {
            ServiceResponse<string> response = await _userRepo.SaveUser(userDto);
            return Ok(response);
        }

        [HttpPut("recover-user/{id:int}")]
        public async Task<IActionResult> PutRecoverUserAdmin(int id, UserDto userDto)
        {
            ServiceResponse<string> response = await _userRepo.RecoverUser(id);
            return Ok(response);
        }

        [HttpPut("update-user/{id:int}")]
        public async Task<IActionResult> PutUserAdmin(int id, [FromBody] UserDto userDto)
        {
            ServiceResponse<string> response = await _userRepo.UpdateUser(id, userDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAdmin(int id)
        {
            ServiceResponse<string> response = await _userRepo.DeleteUser(id);
            return Ok(response);
        }
    }
}
