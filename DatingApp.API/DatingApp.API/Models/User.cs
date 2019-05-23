using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatingApp.API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? LastActive { get; set; }

        public string KnownAs { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        #region FK_Gender
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        public int? GenderId { get; set; }
        #endregion

        #region FK_City
        [ForeignKey("CityId")]
        public City City { get; set; }
        public int? CityId { get; set; }
        #endregion

        #region FK_Country
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int? CountryId { get; set; }
        #endregion

        public ICollection<Photo> Photos { get; set; }

    }
}