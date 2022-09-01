using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterProject.Data.Domains
{
    public class MediaLibrary
    {
        [Key]
        public int MediaId { get; set; }
        public string PictureName { get; set; }

        [DisplayName("Profile Picture Name")]

        public string ReorganizedPictureName { get; set; }

        [NotMapped]
        [DisplayName("Upload Profile Picture")]
        public IFormFile ProfilePictureFile { get; set; }

        //public virtual List<Employee> Employees { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}