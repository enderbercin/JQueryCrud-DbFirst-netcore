using RegisterProject.Data.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace RegisterProject.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public int GenderId { get; set; }

        public string Description { get; set; }
        public int MediaId { get; set; }
        public MediaLibrary MediaLibrary { get; set; }
        public string CityId { get; set; }
        public int GeographicLibraryId { get; set; }
        public GeographicLibrary GeographicLibrary { get; set; }
        public  EmployeeSchool EmployeeSchool { get; set; }
        public int EmployeeSchoolId { get; set; }
        public IFormFile ProfilePictureFile { get; set; }


        public int SchoolSchoolId { get; set; }
        public int SGratuatedSchoolId { get; set; }
        public int SEmployeeId { get; set; }
        public string SGratuatedSchoolName { get; set; }


    }
    //public class SchoolModel
    //{
    //    public string SchoolName { get; set; }
    //    public int SchoolTypeId { get; set; }
    //}
}
