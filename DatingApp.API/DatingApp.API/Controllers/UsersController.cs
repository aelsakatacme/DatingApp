using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models;
using DatingApp.API.ModelsDTO;
using DatingApp.API.IServices;
using System.Security.Claims;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet, Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet, Route("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpPut, Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDTO userDTO)
        {
            if (userDTO.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            return Ok(await _userService.Update(userDTO));
        }

        [HttpPost, Route("RemoveUser/{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            return Ok(await _userService.Remove(id));
        }

    }
}