using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Selu383.SP25.Api.Entities;



namespace Selu383.SP25.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {




            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ??
                throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));


            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.Migrate();

                SeedData(db);
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
                    Address = "789 Oak St",
                    SeatCount = 250,
                },
                    }
                );

                dbContext.SaveChanges(); // 🔹 Make sure to save changes!
            }
        }

    }
}
