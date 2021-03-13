using Microsoft.EntityFrameworkCore;
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
    public class WishlistRepo : IWishlistRepo
    {
        private readonly DataContext _context;

        public WishlistRepo(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<UserWishlist> GetUserWishlist(int id)
        {
            var data = _context.UserWishlists.Where(u => u.UserId == id).ToList();
            return data;
        }

        public async Task<ServiceResponse<string>> SaveUserWishlist(WishlistDto wishlistDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var wishListDetails = new UserWishlist()
            {
                ItemName = wishlistDto.ItemName,
                ItemDescription = wishlistDto.ItemDescription,
                WebLink = wishlistDto.WebLink,
                UserId = Convert.ToInt32(wishlistDto.UserId)
            };

            _context.UserWishlists.Add(wishListDetails);
            await _context.SaveChangesAsync();
            response.Message = "Wishlist data saved sucessfully";
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateUserWishlist(int id, WishlistDto wishlistDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var wishListDetails = _context.UserWishlists.FirstOrDefault(w => w.Id == id);
            wishListDetails.ItemName = wishlistDto.ItemName;
            wishListDetails.ItemDescription = wishlistDto.ItemDescription;
            wishListDetails.WebLink = wishlistDto.WebLink;

            _context.UserWishlists.Update(wishListDetails);
            await _context.SaveChangesAsync();
            response.Message = "Wishlist data updated sucessfully";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteUserWishlist(int id)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            var userWishListdata = _context.UserWishlists.Where(u => u.Id == id).FirstOrDefault();
            _context.UserWishlists.Remove(userWishListdata);
            await _context.SaveChangesAsync();

            response.Message = "Wishlist data deleted sucessfully";
            return response;
        }

        public async Task<IEnumerable<UserWishlistResponseModel>> GetUserWishlistAdmin()
        {
            List<UserWishlistResponseModel> userWishlistResponseModel = await
                (from userWishlist in _context.UserWishlists
                 join user in _context.Users
                 on userWishlist.UserId equals user.Id
                 select new UserWishlistResponseModel
                 {
                    UserWishlistId = userWishlist.Id,
                    ItemName = userWishlist.ItemName,
                    ItemDescription = userWishlist.ItemDescription,
                    WebLink = userWishlist.WebLink,
                    UserId = user.Id,
                    FullName = user.FirstName + " " + user.LastName
                 }).ToListAsync();
            return userWishlistResponseModel;
        }

        public async Task<IEnumerable<UserlistResponseModel>> GetUserlistAdmin()
        {
            List<UserlistResponseModel> userlistResponseModel = await
                (from userWishlist in _context.UserWishlists
                join user in _context.Users
                on userWishlist.UserId equals user.Id
                select new UserlistResponseModel
                {
                    UserId = user.Id,
                    FullName = user.FirstName + " " + user.LastName
                }).Distinct().ToListAsync();
            return userlistResponseModel;
        }
    }
}
