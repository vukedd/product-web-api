using ProductWebAPI.Models;

namespace ProductWebAPI.Interface
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
