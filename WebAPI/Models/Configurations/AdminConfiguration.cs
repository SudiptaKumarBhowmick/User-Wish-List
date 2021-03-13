using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        string password = "123456";
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            builder.HasData
                (
                    new Admin
                    {
                        Id = 1,
                        Name = "Sudipta Kumar Bhowmick",
                        Address = "Dhaka",
                        Email = "sudiptakumar.shuvo@gmail.com",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        RoleId = 1,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin",
                        LastUpdatedAt = DateTime.UtcNow,
                        LastUpdatedBy = "Admin"
                    }
                );
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
