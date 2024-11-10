using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lexicon_university.Core.Entities;
using lexicon_university.Persistance.Configurations;

namespace lexicon_university.Persistance.Data
{
    public class LexiconUniversityContext : DbContext
    {
        public LexiconUniversityContext(DbContextOptions<LexiconUniversityContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // not track entities by default for all queries made through this context.
        }

        public DbSet<Student> Student { get; set; } = default!;
        //public DbSet<Enrollment> Enrollments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Enrollment>().HasKey(e => new { e.CourseId, e.StudentId });

            // Refactor: Separate code into distinct modules (new student config class)
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
        }
    }
}
