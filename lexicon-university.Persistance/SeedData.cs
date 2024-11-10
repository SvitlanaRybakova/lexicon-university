using Bogus;
using lexicon_university.Core.Entities;
using lexicon_university.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
namespace LexiconUniversity.Persistance
{
    public class SeedData
    {
        private static Faker faker;
        public static async Task InitAsync(LexiconUniversityContext context)
        {
            if (await context.Student.AnyAsync()) return;
            faker = new Faker("sv");
            var students = GenerateStudents(50);
            await context.AddRangeAsync(students);
            var courses = GenerateCourses(20);
            await context.AddRangeAsync(courses);
            var enrollments = GenerateEnrollments(courses, students);
            await context.AddRangeAsync(enrollments);
            await context.SaveChangesAsync();
        }
        private static IEnumerable<Enrollment> GenerateEnrollments(IEnumerable<Course> courses, IEnumerable<Student> students)
        {
            var rnd = new Random();
            var enrollments = new List<Enrollment>();
            foreach (var student in students)
            {
                foreach (var course in courses)
                {
                    if (rnd.Next(0, 5) == 0)
                    {
                        var enrollment = new Enrollment
                        {
                            Course = course,
                            Student = student,
                            Grade = rnd.Next(1, 6)
                        };
                        enrollments.Add(enrollment);
                    }
                }
            }
            return enrollments;
        }
        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            var courses = new List<Course>();
            for (int i = 0; i < numberOfCourses; i++)
            {
                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs());
                var course = new Course { Title = title };
                courses.Add(course);
            }
            return courses;
        }
        private static IEnumerable<Student> GenerateStudents(int numberOfStudents)
        {
            var students = new List<Student>();
            for (int i = 0; i < numberOfStudents; i++)
            {
                var avatar = "https://thispersondoesnotexist.com";
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var email = faker.Internet.Email(fName, lName, "lexicon.se");
                var student = new Student()
                {
                    Avatar = avatar,
                    FirstName = fName,
                    LastName = lName,
                    Email = email,
                    Address = new Address
                    {
                        Street = faker.Address.StreetName(),
                        City = faker.Address.City(),
                        ZipCode = faker.Address.ZipCode()
                    }
                };
                students.Add(student);
            }
            return students;
        }
    }
}
