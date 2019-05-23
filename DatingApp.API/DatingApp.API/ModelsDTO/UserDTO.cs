using DatingApp.API.Models;
using System;
using System.Collections.Generic;

namespace DatingApp.API.ModelsDTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string KnownAs { get; set; }

        public int? GenderId { get; set; }
        public string GenderName { get; set; }
        //public Gender Gender { get; set; }

        public int? CityId { get; set; }
        public string CityName { get; set; }
        //public City City { get; set; }

        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        //public Country Country { get; set; }


        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastActive { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }


        public ICollection<PhotoDTO> Photos { get; set; }

        #region Extra
        public int Age { get; set; }
        public string FullAge { get; set; }
        public string PhotoUrl { get; set; }
        #endregion
    }
}
