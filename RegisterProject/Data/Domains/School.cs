using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterProject.Data.Domains
{
    public class School
    {
        [Key]
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public ICollection<GraduatedSchool> GraduatedSchools { get; set; }
    }
}
