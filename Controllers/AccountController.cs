using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RefreshTokensWebApiExample.DataAccess;
using RefreshTokensWebApiExample.Services;

namespace RefreshTokensWebApiExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersDb _usersDb;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        public AccountController(UsersDb usersDb, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _usersDb = usersDb;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        [HttpPost]
        public async Task<IActionResult> Signup(string username, string password)
        {
            var user = _usersDb.Users.SingleOrDefault(u => u.Username == username);
            if (user != null) return StatusCode(409);

            _usersDb.Users.Add(new User 
            {
                Username = username,
                Password = _passwordHasher.GenerateIdentityV3Hash(password)
            });

            
            await _usersDb.SaveChangesAsync();            

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _usersDb.Users.SingleOrDefault(u => u.Username == username);
            if (user == null || !_passwordHasher.VerifyIdentityV3Hash(password, user.Password)) return BadRequest();
            
            var usersClaims = new [] 
            {
                new Claim(ClaimTypes.Name, user.Username),                
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwtToken = _tokenService.GenerateAccessToken(usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _usersDb.SaveChangesAsync();

            return new ObjectResult(new {
                token = jwtToken,
                refreshToken = refreshToken
            });            
        }
    }
}