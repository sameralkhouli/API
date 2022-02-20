using Inlamningsuppgift.Models;
using Inlamningsuppgift.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlamningsuppgift
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<ProductEntity> Products { get; set; }
    }
}

