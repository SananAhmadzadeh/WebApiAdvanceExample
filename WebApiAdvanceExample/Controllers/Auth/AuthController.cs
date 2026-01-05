using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiAdvanceExample.Entities.Auth;
using WebApiAdvanceExample.Entities.DTOs.AutDTOs;

namespace WebApiAdvanceExample.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly TokenOption? _tokenOption;
        public AuthController(UserManager<AppUser<Guid>> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _config = config;
            _tokenOption = _config.GetSection("TokenOptions").Get<TokenOption>();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var admin = _mapper.Map<AppUser<Guid>>(register);

            var resultAdmin = await _userManager.CreateAsync(admin, register.Password);

            if (!resultAdmin.Succeeded)
            {
                return BadRequest(new
                {
                    errors = resultAdmin.Errors,
                    Code = StatusCodes.Status400BadRequest
                });
            }

            await _roleManager.CreateAsync(new IdentityRole("Admin"));

            var resultRole = await _userManager.AddToRoleAsync(admin, "Admin");

            if (!resultRole.Succeeded)
            {
                return BadRequest(new
                {
                    errors = resultRole.Errors,
                    Code = StatusCodes.Status400BadRequest
                });
            }

            return Ok(new
            {
                Message = "Admin registered successfully",
                Code = StatusCodes.Status200OK
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            AppUser<Guid>? user = await _userManager.FindByNameAsync(login.UserName);

            if (user is null)
                return NotFound(new
                {
                    Message = "User not found",
                    Code = StatusCodes.Status404NotFound
                });
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!isValidPassword)
                return Unauthorized(new
                {
                    Message = "Invalid password",
                    Code = StatusCodes.Status401Unauthorized
                });

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtHeader header = new JwtHeader(signingCredentials);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var userRole in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            JwtPayload payload = new JwtPayload(audience: _tokenOption.Audience, issuer: _tokenOption.Issuer, claims: claims, expires: DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration), notBefore: DateTime.UtcNow);

            JwtSecurityToken token = new JwtSecurityToken(header, payload);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string jwt = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = jwt,
                Expiration = token.ValidTo,
                Code = StatusCodes.Status200OK
            });
        }
    }
}
