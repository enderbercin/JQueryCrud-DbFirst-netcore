using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace RegisterProject.Models
{
    public class MediaLibraryViewModel
    {
        public int MediaId { get; set; }
        public string PictureName { get; set; }
        public string ReorganizedPictureName { get; set; }
        public IFormFile ProfilePictureFile { get; set; }
    }
}
