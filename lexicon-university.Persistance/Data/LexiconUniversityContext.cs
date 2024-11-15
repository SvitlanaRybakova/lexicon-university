﻿using System;
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
        public DbSet<Course> Course { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Enrollment>().HasKey(e => new { e.CourseId, e.StudentId });

            // Refactor: Separate code into distinct modules (new student config class)
            modelBuilder.ApplyConfiguration(new StudentConfiguration());

            modelBuilder.Entity<Course>().ToTable("Course", c => c.IsTemporal());
        }

        // set the current data/time when the student table has been edited
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries<Student>().Where(e => e.State == EntityState.Modified))
            {
                entry.Property("Edited").CurrentValue = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
