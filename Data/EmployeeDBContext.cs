using CRUD_Operation.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operation.Data
{
    public class EmployeeDBContext : DbContext
    {

        public EmployeeDBContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd(); 
        }

    }
}
