using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.API.Models
{
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PublicId { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }

        public bool IsMain { get; set; }

        public DateTime CreatedOn { get; set; }

        #region FK_User
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        #endregion
    }
}
