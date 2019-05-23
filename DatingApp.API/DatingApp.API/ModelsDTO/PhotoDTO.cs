using System;

namespace DatingApp.API.ModelsDTO
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
    }
}
