using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using DatingApp.API.IRepositories;
using AutoMapper;
using DatingApp.API.ModelsDTO;
using System.Security.Claims;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;


        public PhotosController(IConfiguration configuration, IDatingRepository repo, IMapper mapper)
        {
            _configuration = configuration;
            _repo = repo;
            _mapper = mapper;

            Account account = new Account(
                _configuration["CloudinarySetting:cloud_name"],
                _configuration["CloudinarySetting:api_key"],
                _configuration["CloudinarySetting:api_secret"]);

            _cloudinary = new Cloudinary(account);
        }


        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int Id)
        {
            Photo photo = await _repo.Get<Photo>(x => x.Id == Id);
            PhotoDTO photoDTO = _mapper.Map<PhotoDTO>(photo);
            return Ok(photoDTO);
        }

        [HttpPost("upload/user/{id}")]
        public async Task<IActionResult> UserPhotoUpload(int Id, [FromForm]PhotoCloudinaryDTO photoCloudinary)
        {
            if (photoCloudinary.File == null)
                return BadRequest("File Not Exist!");

            if (Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            User user = await _repo.GetUser(Id);

            //upload in cloudinary
            var uploaResult = this.CloudinaryUpload(photoCloudinary.File);

            Photo photo = _mapper.Map<Photo>(photoCloudinary);
            photo.UserId = user.Id;
            photo.Url = uploaResult.Uri.ToString();
            photo.PublicId = uploaResult.PublicId;
            if (!user.Photos.Any())
            {
                photo.IsMain = true;
            }

            _repo.Add<Photo>(ref photo);
            if (await _repo.SaveChanges())
            {
                PhotoDTO photoDTO = _mapper.Map<PhotoDTO>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoDTO);
            }
            return BadRequest("Could not add a photo");
        }


        private ImageUploadResult CloudinaryUpload(IFormFile File)
        {
            var uploaResult = new ImageUploadResult();
            if (File.Length > 0)
            {
                using (Stream stream = File.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(File.Name, stream),
                    };
                    uploaResult = _cloudinary.Upload(uploadParams);
                }
            }
            return uploaResult;
        }

    }
}