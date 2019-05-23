using DatingApp.API.Models;
using DatingApp.API.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.IServices
{
    public interface IUserService
    {
        Task<UserDTO> Update(UserDTO user);
        Task<bool> Remove(int id);
        Task<UserDTO> GetUser(int id);
        Task<IEnumerable<UserDTO>> GetUsers();
    }
}
