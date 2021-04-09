using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Userspace.Api.Resources.Auth;
using Userspace.Core.Models.Auth;

namespace Userspace.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        public AuthController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }
        /// <summary>
        /// JWT signup
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpResource userSignUpResource)
        {
            var user = _mapper.Map<SignUpResource, User>(userSignUpResource);
            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);
            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }
            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }
        /// <summary>
        /// JWT signin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInResource userSignInResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userSignInResource.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }
            var userSigninResult = await _userManager.CheckPasswordAsync(user, userSignInResource.Password);
            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(GenerateJwt(user, roles));
            }
            return BadRequest("Email or password incorrect.");
        }
        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
             new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
             new Claim(ClaimTypes.Name, user.UserName),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    //    [HttpGet]
    //    public async Task<string> GetCurrentUserId()
    //    {
    //        User usr = await GetCurrentUserAsync();
    //        return usr?.Id.ToString();
    //    }
    //    private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
      }
}
