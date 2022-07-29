using Microsoft.EntityFrameworkCore;
using studentsApi.Entity;

namespace studentsApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}
}