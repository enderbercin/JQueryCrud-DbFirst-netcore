using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterProject.Data.Domains
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        [DisplayName("Gender")]
        public Gender gender { get; set; }
        public enum Gender
        {
            Man,
            Women
        }
        public string Description { get; set; }
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public virtual MediaLibrary MediaLibrary { get; set; }

        public string CityId { get; set; }
        [ForeignKey("GeographicLibraryId")]
        [DisplayName("City")]
        public int GeographicLibraryId { get; set; }

        public virtual GeographicLibrary GeographicLibrary { get; set; }

        public int EmployeeSchoolId { get; set; }
        
        [NotMapped]
        public ICollection<EmployeeSchool> EmployeeSchools { get; set; }

    }
}