using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.ModelsDTO
{
    public class PhotoCloudinaryDTO
    {
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreatedOn { get; set; }

        public PhotoCloudinaryDTO()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
