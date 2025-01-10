using Microsoft.EntityFrameworkCore;
using School24.Models;

namespace School24.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
        {

        }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Absence> Absences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .HasKey(s => s.SchoolId);

            modelBuilder.Entity<School>()
                .Property(s => s.SchoolName)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasKey(st => st.StudentId);

            modelBuilder.Entity<Student>()
                .Property(st => st.StudentName)
                .IsRequired();

            modelBuilder.Entity<Absence>()
                .HasKey(a => a.AbsenceId);

            modelBuilder.Entity<Absence>()
                .Property(a => a.AbsenceLength)
                .IsRequired(false);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Absences)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Students)
                .WithOne(st => st.School)
                .HasForeignKey(st => st.SchoolId);
        }
    }
}
