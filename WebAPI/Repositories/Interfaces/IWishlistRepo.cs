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
    public interface IWishlistRepo
    {
        IEnumerable<UserWishlist> GetUserWishlist(int id);
        Task<ServiceResponse<string>> SaveUserWishlist(WishlistDto wishlistDto);
        Task<ServiceResponse<string>> UpdateUserWishlist(int id, WishlistDto wishlistDto);
        Task<ServiceResponse<string>> DeleteUserWishlist(int id);
        Task<IEnumerable<UserWishlistResponseModel>> GetUserWishlistAdmin();
        Task<IEnumerable<UserlistResponseModel>> GetUserlistAdmin();
    }
}
