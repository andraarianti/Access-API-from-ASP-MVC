using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APISolution.BLL.DTOs;
using APISolution.BLL.Interfaces;
using APISolution.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APISolution.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBLL _userBLL;
        private readonly AppSettings _appSettings;
        public UsersController(IUserBLL userBLL, IOptions<AppSettings> appSettings)
        {
            _userBLL = userBLL;
            _appSettings = appSettings.Value;
        }

        //GET ALL 
        [HttpGet("all")]
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var result = await _userBLL.GetAll();
            return result;
        }

        //GET BY NAME
        [HttpGet("Name/{name}")]
        public async Task<UserDTO> GetByUsername(string name)
        {
            var result = await _userBLL.GetByUsername(name);
            return result;
        }

        [HttpGet("withRoles")]
        public async Task<IEnumerable<UserDTO>> GetUserWithRoles()
        {
            var result = await _userBLL.GetAllWithRoles();
            return result;
        }

        //Change Password
        [HttpPut]
        public async Task<IActionResult> ChangePassword(string username, string password)
        {
            try
            {
                //cek akun
                var user = await _userBLL.GetByUsername(username);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                await _userBLL.ChangePassword(username, password);
                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = await _userBLL.Login(loginDTO.Username, loginDTO.Password);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                var userRoles = await _userBLL.GetUserWithRoles(loginDTO.Username);

                //add authentication
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userRoles.Username));
                foreach (var role in userRoles.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName)); // Misalnya, asumsikan RoleName adalah properti yang menyimpan nama role
                }

                // Konfigurasi token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var userWithToken = new LoginDTO
                {
                    Username = loginDTO.Username,
                    Password = loginDTO.Password,
                    Token = tokenHandler.WriteToken(token)
                };
                return Ok(new { userWithToken, Roles = userRoles.Roles });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
