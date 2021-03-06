﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore_API.Contracts;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;
        private readonly IConfiguration _config;

        public UsersController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILoggerService logger,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
        }
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var location = GetCollerActionNames();
            try
            {
                var username = userDTO.EmailAddress;
                var password = userDTO.Password;
                _logger.LogInfo($"{location} : Registration attempt for {username}");

                var user = new IdentityUser { Email = username, UserName = username };
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"{location}: {error.Code} {error.Description} ");
                    }
                    _logger.LogError($"{location}: {username} user registrtion attempt failed");
                }
                return Ok(new { result.Succeeded });
            }
            catch (Exception ex)
            {
                return InternalError($"{location}:{ex.Message} - {ex.InnerException}");
            }

        }

        /// <summary>
        /// user login endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// 
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var location = GetCollerActionNames();
            try
            {
                _logger.LogInfo($"{location} : Login attempt from user {userDTO.EmailAddress}");
                var result = await _signInManager.PasswordSignInAsync(userDTO.EmailAddress, userDTO.Password, false, false);
                if (result.Succeeded)
                {
                    _logger.LogInfo($"{location}:{userDTO.EmailAddress} Successfully Authenticate");
                    var user = await _userManager.FindByNameAsync(userDTO.EmailAddress);
                    var tokenString = await GenerateJSONWebToken(user);
                    return Ok(new { token = tokenString });
                }
                _logger.LogInfo($"{location}:{userDTO.EmailAddress} Not Authenticated");
                return Unauthorized(userDTO);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}:{ex.Message} - {ex.InnerException}");
            }
        }
        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)

            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_config["JWT:Issuer"]
               , _config["Jwt:Issuer"],
               claims,
               null,
               expires: DateTime.Now.AddMinutes(5),
               signingCredentials: credentials
               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetCollerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller}- {action}";
        }
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong.");
        }
    }
}