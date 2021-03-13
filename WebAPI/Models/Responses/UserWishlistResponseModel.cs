using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class UserWishlistResponseModel
    {
        public int UserWishlistId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string WebLink { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
    }
}
