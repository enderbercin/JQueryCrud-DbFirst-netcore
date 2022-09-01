using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterProject.Data.Domains
{
    [Keyless]
    public class EmployeeSchool
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int GraduatedSchoolId { get; set; }
        public GraduatedSchool GraduatedSchool { get; set; }

    }
}
