using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProgrammingCompetitionService.Models;
using ProgrammingCompetitionService.Intrerfaces;

namespace ProgrammingCompetitionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _repository.GetByUserName(userLogin.UserName);

            if (user != null)
            {
                return Problem("User already exist.", statusCode: 409);
            }

            CreatePasswordHash(userLogin.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user = new User() { UserName = userLogin.UserName, PasswordHash = passwordHash, PasswordSalt = passwordSalt };

            await _repository.Add(user);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserAuthorized>> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _repository.GetByUserName(userLogin.UserName);

            if (user == null || !VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Problem("Wrong username or password.", statusCode: 403);
            }

            var token = CreateToken(user);
            UserAuthorized userAuthorized = new UserAuthorized()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Token = token,
                Role = user.Role
            };

            return Ok(userAuthorized);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMonths(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
