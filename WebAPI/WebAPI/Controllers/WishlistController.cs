using Microsoft.AspNetCore.Mvc;
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
    public class WishlistController : BaseApiController
    {
        private readonly IWishlistRepo _wishlistRepo;

        public WishlistController(IWishlistRepo wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }

        [HttpGet]
        public IEnumerable<UserWishlist> GetUserWishlist(int id)
        {
            return _wishlistRepo.GetUserWishlist(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostUserWishlist(WishlistDto wishlistDto)
        {
            ServiceResponse<string> response = await _wishlistRepo.SaveUserWishlist(wishlistDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserWishlist(int id, [FromBody] WishlistDto wishlistDto)
        {
            ServiceResponse<string> response = await _wishlistRepo.UpdateUserWishlist(id, wishlistDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<string> response = await _wishlistRepo.DeleteUserWishlist(id);
            return Ok(response);
        }
    }
}
