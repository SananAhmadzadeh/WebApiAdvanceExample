using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public AuthController(UserManager<AppUser<Guid>> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var user = _mapper.Map<AppUser<Guid>>(register);

            var resultUser = await _userManager.CreateAsync(user, register.Password);

            if (!resultUser.Succeeded)
            {
                return BadRequest(new
                {
                    errors = resultUser.Errors,
                    Code = StatusCodes.Status400BadRequest
                });
            }

            await _roleManager.CreateAsync(new IdentityRole("User"));

            var resultRole = await _userManager.AddToRoleAsync(user, "User");

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
                Message = "User registered successfully",
                Code = StatusCodes.Status200OK
            });
        }
    }
}
