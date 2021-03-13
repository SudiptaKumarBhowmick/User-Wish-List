using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Models;
using Models.Responses;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthRepo _authRepo;

        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterDto registerDto)
        {
            ServiceResponse<int> response = await _authRepo.RegisterUser(registerDto, registerDto.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser(LoginDto loginDto)
        {
            ServiceResponse<LoginResponse> response = await _authRepo.LoginUser(loginDto.Email, loginDto.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdmin(LoginDto loginDto)
        {
            ServiceResponse<LoginResponse> response = await _authRepo.LoginAdmin(loginDto.Email, loginDto.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("getusers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _authRepo.getUsers();
        }
    }
}
