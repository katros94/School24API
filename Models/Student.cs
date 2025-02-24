﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School24.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public ICollection<Absence> Absences { get; set; }
        public string SchoolName { get; internal set; }
        public int AbsenceLength { get; internal set; }
    }
}
