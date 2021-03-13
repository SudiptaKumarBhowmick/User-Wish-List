using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos;
using Models.Models;
using Models.Responses;
using Repositories.Interfaces;
using Services.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        private readonly TokenSettings _tokenSettings;

        public AuthRepo(DataContext context, IOptions<TokenSettings> tokenSettings)
        {
            _context = context;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<ServiceResponse<int>> RegisterUser(RegisterDto user, string password)
        {
            ServiceResponse<int> registerResponse = new ServiceResponse<int>();
            if (await EmailExists(user.Email))
            {
                registerResponse.Success = false;
                registerResponse.Message = "Email is already exists!";
                return registerResponse;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var userDetails = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2
            };

            await _context.Users.AddAsync(userDetails);
            await _context.SaveChangesAsync();
            registerResponse.Data = userDetails.Id;
            registerResponse.Message = "Registration is successfull.";
            return registerResponse;
        }

        public async Task<ServiceResponse<LoginResponse>> LoginUser(string email, string password)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.Equals(email.ToLower()) && u.IsDeleted == false);
            ServiceResponse<LoginResponse> response = new ServiceResponse<LoginResponse>() { Data = new LoginResponse() };
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid Email or Password!";
            }
            else
            {
                if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Invalid Email or Password!";
                }
                else
                {
                    var jwtToken = CreateTokenUser(user);
                    var role = _context.Roles.FirstOrDefault(r => r.RoleId == user.RoleId);

                    response.Data.Id = user.Id;
                    response.Data.JwtToken = jwtToken;
                    response.Data.RoleId = user.RoleId;
                    response.Data.RoleDescription = role.RoleDescription;
                    response.Message = "Login Successful!";
                }
            }

            return response;
        }

        public async Task<ServiceResponse<LoginResponse>> LoginAdmin(string email, string password)
        {
            Admin admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email.Equals(email.ToLower()));
            ServiceResponse<LoginResponse> response = new ServiceResponse<LoginResponse>() { Data = new LoginResponse() };
            if (admin == null)
            {
                response.Success = false;
                response.Message = "Invalid Email or Password!";
            }
            else
            {
                if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Invalid Email or Password!";
                }
                else
                {
                    var jwtToken = CreateTokenAdmin(admin);
                    var role = _context.Roles.FirstOrDefault(r => r.RoleId == admin.RoleId);

                    response.Data.Id = admin.Id;
                    response.Data.JwtToken = jwtToken;
                    response.Data.RoleId = admin.RoleId;
                    response.Data.RoleDescription = role.RoleDescription;
                    response.Message = "Login Successful!";
                }
            }

            return response;
        }

        public async Task<bool> EmailExists(string email)
        {
            if (await _context.Admins.AnyAsync(user => user.Email == email.ToLower())) return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            };
        }

        public async Task<IEnumerable<User>> getUsers()
        {
            return await _context.Users.ToListAsync();
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateTokenUser(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_tokenSettings.TokenSecretKey));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_tokenSettings.JwtTokenExpires),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string CreateTokenAdmin(Admin admin)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                new Claim(ClaimTypes.Name, admin.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_tokenSettings.TokenSecretKey));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_tokenSettings.JwtTokenExpires),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
