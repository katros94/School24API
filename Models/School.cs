using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School24.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
