using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.ModelsDTO
{
    public class UserRegisterDTO
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
