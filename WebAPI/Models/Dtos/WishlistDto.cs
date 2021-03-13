using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class WishlistDto
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string WebLink { get; set; }
        public int UserId { get; set; }
    }
}
