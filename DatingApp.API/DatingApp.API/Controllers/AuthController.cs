using System;
using System.Threading.Tasks;
using DatingApp.API.IRepositories;
using DatingApp.API.Models;
using DatingApp.API.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userDTO.Username = userDTO.Username.ToLower();
            if (await _authRepository.UserExist(userDTO.Username))
                return BadRequest("user already exists");

            var createdUser = await _authRepository.Register(userDTO);

            return StatusCode(201);
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userDTO)
        {
            var user = await _authRepository.Login(userDTO.Username, userDTO.Password);
            if (user == null)
                return Unauthorized();

            var token = _authRepository.CreateToken(user);

            return Ok(new { token, user = new UserDTO { Id = user.Id, FullName = user.FullName, Username = user.Username } });
        }

        [HttpPost, Route("GetUser")]
        public User GetUser([FromBody]string tokenstring)
        {
            return _authRepository.GetLoggedUser(tokenstring);
        }

    }
}
