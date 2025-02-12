using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Entities;

namespace Selu383.SP25.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = Env.GetString("CONNECTION_STRING");
            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dbContext.Database.Migrate(); // Ensure database is updated
                SeedData(dbContext); // Insert initial records if not present
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void SeedData(DataContext dbContext)
        {
            if (!dbContext.Theaters.Any()) // Check if theaters table is empty
            {
                dbContext.Theaters.AddRange(
                    new List<Theater>
                    {
                        new Theater
                        {
                            Name = "Grand Cinema",
                            Address = "123 Main St",
                            SeatCount = 150,
                        },
                        new Theater
                        {
                            Name = "Luxury Movie House",
                            Address = "456 Elm St",
                            SeatCount = 200,
                        },
                        new Theater
                        {
                            Name = "Luxury Movie House 2",
                            Address = "456 Elm St",
                            SeatCount = 200,
                        },
                    }
                );

                dbContext.SaveChanges(); // Save changes to the database
            }
        }
    }
}
