using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string JwtToken { get; set; }
        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
    }
}
