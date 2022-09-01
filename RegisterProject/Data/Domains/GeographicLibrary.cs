using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RegisterProject.Data.Domains
{
    public class GeographicLibrary
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Address")]
        public string Tanim { get; set; }
        public int UstId { get; set; }
        //public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
