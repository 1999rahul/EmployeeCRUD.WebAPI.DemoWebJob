using EmployeeCrud.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace EmployeeCrud.Data
{
    public class EmployeeDBContext:DbContext
    {
       public EmployeeDBContext(DbContextOptions options):base(options) { }
       public DbSet<Employee> employee { get; set; }

    }
}
