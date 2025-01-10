using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School24.Models
{
    public class Absence
    {
        [Key]
        public int AbsenceId { get; set; }
        public int? AbsenceLength { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

    }
}