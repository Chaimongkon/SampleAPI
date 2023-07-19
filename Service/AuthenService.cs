using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Data;
using SampleAPI.Dtos;
using SampleAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SampleAPI.Service
{
    public class AuthenService : IAuthenService
    {
        private readonly IConfiguration _configuration;
        private readonly SampleDBContext _context;

        public AuthenService(IConfiguration configuration, SampleDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public UserLoginDtos? Login(LoginDtos dto)
        {
            User? user = _context.Users.Where(w => w.Username == dto.Username).FirstOrDefault();

            if (user is null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                UserLoginDtos tokenUser = CreateTokenUser(user.Id, user.Username);

                user.Token = tokenUser.AccessToken;

                _context.SaveChanges();

                return tokenUser;
            }
            else
            {
                return null;
            }
        }

        public RegisterDtos? Register(RegisterDtos dto)
        {
            try
            {
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                if (!string.IsNullOrEmpty(passwordHash))
                {
                    User user = new();
                    user.Id = Guid.NewGuid();
                    user.Fullname = dto.Fullname;
                    user.Username = dto.Username;
                    user.SaltHash = Convert.ToBase64String(salt);
                    user.Password = passwordHash;
                    user.Email = dto.Email;

                    _context.Users.Add(user);
                    int resultSaved = _context.SaveChanges();
                    if (resultSaved > 0)
                        return dto;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private UserLoginDtos CreateTokenUser(Guid userId, string username)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                new Claim(ClaimTypes.Name,username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int expiredDays = int.Parse(_configuration.GetSection("Jwt:ExpireDays").Value);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(expiredDays),
                SigningCredentials = credentials,
                IssuedAt = DateTime.Now,
                Issuer = _configuration.GetSection("Jwt:Issuer").Value,
                Audience = _configuration.GetSection("Jwt:Audience").Value
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginDtos userData = new UserLoginDtos()
            {
                UserId = userId,
                Username = username,
                AccessToken = tokenHandler.WriteToken(token),
                Issuer = tokenDescriptor.Issuer,
                IssueAt = String.Format("{0:r}", tokenDescriptor.IssuedAt),
                Audience = _configuration.GetSection("Jwt:Audience").Value,
                ExpiresIn = ((DateTimeOffset)tokenDescriptor.Expires).ToUnixTimeMilliseconds(),
                ExpireDate = String.Format("{0:r}", tokenDescriptor.Expires)
            };

            return userData;
        }
        public List<User> GetAllUsers()
        {
            var users = _context.Users.ToList();

            return users;
        }
        public User GetUser(Int32 id)
        {
            var user = _context.Users.Find(id);

            if (user is null)
                user = new User();

            return user;

        }
    }
}
