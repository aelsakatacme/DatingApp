using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.ModelsDTO;

namespace DatingApp.API.IRepositories
{
    public interface IAuthRepository
    {
        Task<User> Register(UserRegisterDTO user);
        Task<User> Login(string username, string password);
        Task<bool> UserExist(string username);
        string CreateToken(User user);
        User GetLoggedUser(string tokenstring);
    }
}
