using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class RepositoryDependency
    {
        public static void AddRepositoryDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IWishlistRepo, WishlistRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
        }
    }
}
