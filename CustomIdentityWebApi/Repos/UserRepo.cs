using AutoMapper;
using CustomIdentityWebApi.Models;
using CustomIdentityWebApi.Res.UserRes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UserRepo(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<User> CreateUserAsync(CreateUserRes res)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            User user = mapper.Map<User>(res);

            user.PasswordHash = Encoding.UTF8.GetString(sha1.ComputeHash(Encoding.UTF8.GetBytes(res.Password)));
            user.SecurityStamp = Guid.NewGuid().ToString();

            await context.AddAsync(user);

            return user;
        }

        public async Task<string> LoginAsync(LoginRes res)
        {
            User user = context.Users.SingleOrDefault(u => u.UserName == res.UserName);

            if (user is null)
                return null;

            var sha1 = new SHA1CryptoServiceProvider();
            string passwordHash = Encoding.UTF8.GetString(sha1.ComputeHash(Encoding.UTF8.GetBytes(res.Password)));
            if (!passwordHash.Equals(user.PasswordHash) || !user.Active)
                return null;

            await IncludeRelatedTables(user);

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user, int expireAfterDays = 30)
        {
            var claims = new[]
            {
                new Claim("UserName", user.UserName),
                new Claim("SecurityStamp",user.SecurityStamp),
                new Claim("UserId",user.Id.ToString()),
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.Role,user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));
            string aud = configuration["AuthSettings:Audience"];
            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(expireAfterDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public async Task IncludeRelatedTables(User user)
        {
            await context.Entry(user).Reference(i => i.Role).LoadAsync();
        }
    }
}
