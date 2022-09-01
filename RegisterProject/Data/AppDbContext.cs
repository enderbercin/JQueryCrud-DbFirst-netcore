using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegisterProject.Data.Domains;

namespace RegisterProject.Data
{
    public class AppDbContext:DbContext
    {
        protected readonly IConfiguration Configuration;
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{

        //}
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("RegisterAppDBConnectionString"));
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<MediaLibrary> MediaLibraries { get; set; }
        public DbSet<GeographicLibrary> GeographicLibraries { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<EmployeeSchool> EmployeeSchools { get; set; }
        public DbSet<GraduatedSchool> GraduatedSchools { get; set; }


    }

    
}

