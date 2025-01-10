namespace School24.DTOs
{
    public class StudentAbsenceDto
    {
        public required string SchoolName { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; internal set; }
        public int AbsenceLength { get; set; }
    }
}
