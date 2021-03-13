using Models.Dtos;
using Models.Models;
using Models.Responses;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;

        public UserRepo(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _context.Users.Where(u => u.IsDeleted == false).ToList();
            return users;
        }

        public IEnumerable<User> GetDeletedUsers()
        {
            var users = _context.Users.Where(u => u.IsDeleted == true).ToList();
            return users;
        }

        public async Task<ServiceResponse<string>> SaveUser(UserDto userDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userDetails = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Address = userDto.Address,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2
            };

            _context.Users.Add(userDetails);
            await _context.SaveChangesAsync();
            response.Message = "User data saved sucessfully";
            return response;
        }

        public async Task<ServiceResponse<string>> RecoverUser(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var userDetails = _context.Users.FirstOrDefault(u => u.Id == id);
            userDetails.IsDeleted = false;

            _context.Users.Update(userDetails);
            await _context.SaveChangesAsync();
            response.Message = "User recoverd sucessfully";
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateUser(int id, UserDto userDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userDetails = _context.Users.FirstOrDefault(a => a.Id == id);

            userDetails.FirstName = userDto.FirstName;
            userDetails.LastName = userDto.LastName;
            userDetails.Address = userDto.Address;
            userDetails.Email = userDto.Email;
            userDetails.PasswordHash = passwordHash;
            userDetails.PasswordSalt = passwordSalt;

            _context.Users.Update(userDetails);
            await _context.SaveChangesAsync();
            response.Message = "User data updated sucessfully";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteUser(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var userDetails = _context.Users.Where(u => u.Id == id).FirstOrDefault();

            _context.Users.Remove(userDetails);
            await _context.SaveChangesAsync();
            response.Message = "User data deleted sucessfully";
            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            };
        }
    }
}
