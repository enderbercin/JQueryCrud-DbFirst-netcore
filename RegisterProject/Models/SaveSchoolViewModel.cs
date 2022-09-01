using System.Collections.Generic;

namespace RegisterProject.Models
{
    public class SaveSchoolViewModel
    {
        public int SchoolSchoolId { get; set; }
        public int SGratuatedSchoolId { get; set; }
        public int SEmployeeId { get; set; }
        public string SGratuatedSchoolName { get; set; }
        public List<SchoolModel> SList { get; set; }
        public List<SavedSchoolModel> SavedList { get; set; }


    }

    public class SchoolModel
    {
        public string SchoolName { get; set; }
        public int SchoolTypeId { get; set; }
    }
    public class SavedSchoolModel
    {
        public int GraduatedSchoolId { get; set; }
        public string GraduatedSchoolName { get; set; }
        public int SchoolId { get; set; }
    }
}
   
