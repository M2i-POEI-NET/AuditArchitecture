using Microsoft.EntityFrameworkCore;

namespace Course.API
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }

        public DbSet<Course.Model.Entities.Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course.Model.Entities.Course>().ToTable("Courses");
        }
    }
}