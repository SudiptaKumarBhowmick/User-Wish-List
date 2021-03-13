using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.Responses;
using Repositories;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class WishlistAdminController : BaseApiController
    {
        private readonly IWishlistRepo _wishlistRepo;

        public WishlistAdminController(IWishlistRepo wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }

        [HttpGet("user-list")]
        public Task<IEnumerable<UserlistResponseModel>> GetUserlistAdmin()
        {
            return _wishlistRepo.GetUserlistAdmin();
        }

        [HttpGet("user-wishlist")]
        public Task<IEnumerable<UserWishlistResponseModel>> GetUserWishlistAdmin()
        {
            return _wishlistRepo.GetUserWishlistAdmin();
        }

        [HttpPost]
        public async Task<IActionResult> PostUserWishlistAdmin(WishlistDto wishlistDto)
        {
            ServiceResponse<string> response = await _wishlistRepo.SaveUserWishlist(wishlistDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserWishlistAdmin(int id, [FromBody] WishlistDto wishlistDto)
        {
            ServiceResponse<string> response = await _wishlistRepo.UpdateUserWishlist(id, wishlistDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserWishlistAdmin(int id)
        {
            ServiceResponse<string> response = await _wishlistRepo.DeleteUserWishlist(id);
            return Ok(response);
        }
    }
}
