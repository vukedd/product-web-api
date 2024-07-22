using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace ProductWebAPI.Models
{
    public class AppUser : IdentityUser
    {
        public List<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
    }
}
