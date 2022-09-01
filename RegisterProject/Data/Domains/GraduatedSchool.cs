using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterProject.Data.Domains
{
    public class GraduatedSchool
    {
        [Key]
        public int GraduatedSchoolId { get; set; }
        public string GraduatedSchoolName { get; set; }
        public int SchoolId { get; set; }
    }
}
