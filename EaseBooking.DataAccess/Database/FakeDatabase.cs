using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using EaseBooking.Entities.Genders;
using EaseBooking.Entities.HealthProblems;
using EaseBooking.Entities.Products;
using EaseBooking.Entities.TimeSlots;

namespace EaseBooking.DataAccess.Database
{
    public static class FakeDatabase
    {
        public static bool IsInitialized { get; private set; }
        private static bool SeedingIsInProgress { get; set; }

        public static void InitializeFakeDataBase()
        {
            if (IsInitialized) return;
            if (SeedingIsInProgress) return;
            SeedingIsInProgress = true;
            ClearAllTables();
            SeedGenders();
            SeedHealthProblems();
            SeedProduct();
            IsInitialized = true;
            SeedingIsInProgress = false;
        }

        private static void ClearAllTables()
        {
            var database = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Database"]));
            foreach (FileInfo file in database.GetFiles())
            {
                file.Delete();
            }
        }

        private static void SeedGenders()
        {
            var genderRepository = new GenericRepository<Gender, Guid>();
            genderRepository.Create(
                new Gender { Id = Guid.NewGuid(), Name = "Male" },
                new Gender { Id = Guid.NewGuid(), Name = "Female" }
                );
        }

        private static void SeedHealthProblems()
        {
            var healthProblemRepository = new GenericRepository<HealthProblem, Guid>();
            healthProblemRepository.Create(
                new HealthProblem { Id = Guid.NewGuid(), Name = "Heart disease" },
                new HealthProblem { Id = Guid.NewGuid(), Name = "Asthma" },
                new HealthProblem { Id = Guid.NewGuid(), Name = "Obesity" },
                new HealthProblem { Id = Guid.NewGuid(), Name = "Diabetes" },
                new HealthProblem { Id = Guid.NewGuid(), Name = "Headaches" },
                new HealthProblem { Id = Guid.NewGuid(), Name = "Depression" },
                new HealthProblem { Id = Guid.NewGuid(), Name = "Gastrointestinal problems" },
                new HealthProblem { Id = Guid.NewGuid(), Name = "Accelerated aging" }
                );
        }

        private static void SeedProduct()
        {           
            var productId = Guid.NewGuid();

            var product = new Product
            {
                Id = productId,
                Name = "Faldskærmsudspring - Tandem",
                Description = @"Et faldskærmsudspring i form af et tandemspring er en fantastisk oplevelse, som alle bør opleve. Der springes sammen med en instruktør, hvilket betyder der ikke skal bruges tid på teoretisk og praktisk forberedelse. Faktisk handler det bare om at følge nogle enkle instrukser og ellers bare nyde turen, fuldt ud. Oplevelsen kan prøves i hele landet, fra tidligt forår til oktober, hos vores udvalgte samarbejdspartnere (Faldskærmsklubben DFC, Centerjump, Skydive 2000, Østjyllands Faldskærmsklub og Dropzone Denmark) og med garanti for et adrenalinsus og evigt minde - den perfekte oplevelsesgave.",
                Link = "https://duglemmerdetaldrig.dk/oplev/faldskaermsudspring-tandem/",
                TimeSlots = new List<TimeSlot>()
            };

            for (int i = 9; i < 18; i++)
            {
                product.TimeSlots.Add(
                    new TimeSlot
                    {
                        Id = Guid.NewGuid(),
                        IsAvailable = true,
                        ProductId = productId,
                        StartDateTime = new DateTime(2016, 6, 30, i, 0, 0),
                        EndDateTime = new DateTime(2016, 6, 30, (i + 1), 0, 0)
                    }
                    );
            }

            var productRepository = new GenericRepository<Product, Guid>();
            productRepository.Create(product);

        }
    }
}
