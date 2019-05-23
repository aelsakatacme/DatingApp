using System;
using System.Threading.Tasks;
using DatingApp.API.IRepositories;
using DatingApp.API.Models;
using DatingApp.API.Context;
using DatingApp.API.Helpers;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.ModelsDTO;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DatingApp.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatingAppContext _Context;
        private readonly IMapper _IMapper;
        IConfiguration _Configuration;

        public AuthRepository(DatingAppContext context, IMapper mapper, IConfiguration configuration)
        {
            _Context = context;
            _IMapper = mapper;
            _Configuration = configuration;
        }


        public async Task<User> Register(UserRegisterDTO userDTO)
        {
            try
            {
                User user = _IMapper.Map<UserRegisterDTO, User>(userDTO);
                //HASH PASSWORD
                byte[] passwordHash, passwordSalt;
                Helper.CreatePasswordHash(userDTO.Password, out passwordHash, out passwordSalt);
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;

                user.KnownAs = user.FullName;
                user.CreatedOn = DateTime.Now;

                //SAVE
                await _Context.Users.AddAsync(user);
                await _Context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex) { return null; }
        }

        public async Task<User> Login(string username, string password)
        {
            var existUser = await _Context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (existUser != null)
            {
                var passwordHash = Helper.GetPasswordHash(existUser.PasswordSalt, password);
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != existUser.PasswordHash[i])
                        return null;
                }
                return existUser;
            }
            return null;
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _Context.Users.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }

        public string CreateToken(User user)
        {
            Claim[] calims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), //nameid
                new Claim(ClaimTypes.Name, user.Username), //uniquename
            };

            SymmetricSecurityKey SignatureKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_Configuration["JWTSettings:SignatureKey"]));
            SigningCredentials creds = new SigningCredentials(SignatureKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(calims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddHours(6),
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public User GetLoggedUser(string tokenString)
        {
            if (!string.IsNullOrEmpty(tokenString))
            {
                var jwtEncodedString = tokenString.Substring(7);
                var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                var nameid = token.Claims.First(c => c.Type == "nameid").Value;
                int userId;
                if (int.TryParse(nameid, out userId))
                {
                    return _Context.Users.FirstOrDefault(x => x.Id == userId);
                }
            }
            return null;
        }

    }
}
