using System.Collections.Generic;
using System.Data.Entity;

namespace Lab8MVC.Models
{
    // Creates the database on first access and seeds sample data so that
    // searching and campus filtering can be demonstrated right away.
    public class Lab8DbInitializer : CreateDatabaseIfNotExists<Lab8MvcContext>
    {
        protected override void Seed(Lab8MvcContext context)
        {
            var albany = new UniversityCampus { Name = "Albany Campus" };
            var manawatu = new UniversityCampus { Name = "Manawatu Campus" };
            var wellington = new UniversityCampus { Name = "Wellington Campus" };

            var students = new List<Student>
            {
                new Student { Name = "Alice Chen", Address = "12 Ocean Road", UniversityCampus = albany },
                new Student { Name = "Ben Liu", Address = "8 College Street", UniversityCampus = albany },
                new Student { Name = "Cindy Wang", Address = "45 Fitzherbert Avenue", UniversityCampus = manawatu },
                new Student { Name = "David Zhang", Address = "3 Vivian Street", UniversityCampus = wellington },
                new Student { Name = "Emma Zhao", Address = "21 Campus Drive", UniversityCampus = manawatu },
                new Student { Name = "Frank Xu", Address = "7 Lambton Quay", UniversityCampus = wellington }
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
