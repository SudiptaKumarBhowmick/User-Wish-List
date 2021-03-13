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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData
                (
                    new Role
                    {
                        RoleId = 1,
                        RoleDescription = "Admin",
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin",
                        LastUpdatedAt = DateTime.UtcNow,
                        LastUpdatedBy = "Admin"
                    },
                    new Role
                    {
                        RoleId = 2,
                        RoleDescription = "User",
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "Admin",
                        LastUpdatedAt = DateTime.UtcNow,
                        LastUpdatedBy = "Admin"
                    }
                );
        }
    }
}
