using System;
using Selu383.SP25.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Config;


namespace Selu383.SP25.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Theater> Theaters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TheaterConfig());

        }



    }
}

