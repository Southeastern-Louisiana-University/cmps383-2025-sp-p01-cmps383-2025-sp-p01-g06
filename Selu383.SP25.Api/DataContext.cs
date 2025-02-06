using Selu383.SP25.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Selu383.SP25.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Theater> Theaters { get; set; }
    }
    
}

