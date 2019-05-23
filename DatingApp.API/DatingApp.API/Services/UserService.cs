using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.IRepositories;
using DatingApp.API.IServices;
using DatingApp.API.Models;
using DatingApp.API.ModelsDTO;

namespace DatingApp.API.Services
{
    public class UserService : IUserService
    {
        IMapper _mapper;
        IDatingRepository _repo;

        public UserService(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var user = await _repo.GetUser(userDTO.Id);
            //var user = await _repo.GetBy<User>(x => x.Id == userDTO.Id, x => x.Photos, x => x.City, x => x.Country);
            foreach (var item in user.Photos)
            {
                _repo.Remove<Photo>(item);
            }
            await _repo.SaveChanges();

            _mapper.Map<UserDTO, User>(userDTO, user);
            foreach (var item in user.Photos)
            {
                item.Id = 0;
            }
            _repo.Update<User>(ref user);
            await _repo.SaveChanges();
            return await GetUser(user.Id);
        }

        public async Task<bool> Remove(int id)
        {
            //var user = await _repo.GetUser(id);
            var user = await _repo.Get<User>(x => x.Id == id);
            _repo.Remove<User>(user);
            return await _repo.SaveChanges();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            //var user = await _repo.GetUser(id);
            var user = await _repo.Get<User>((x) => x.Id == id, (x) => x.Photos, x => x.City, x => x.Country, (x) => x.Gender);
            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            //var users = await _repo.GetUsers();
            var users = await _repo.GetAll<User>(x => x.Photos, x => x.City, x => x.Gender);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

    }
}
