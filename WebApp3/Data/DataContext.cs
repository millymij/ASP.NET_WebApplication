using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;


namespace WebApplication3.Data
{
    // This class extends DbContext from Entity Framework Core, enabling it to manage the application's data access logic.
    // Through connection string from the configuration file manages database operations and relationships between Country, Company, and Address entities.

    public class DataContext : DbContext
    {
        readonly DbContextOptions<DataContext> options;
        
        // connection string
        public DataContext(DbContextOptions<DataContext> _options) : base(_options)
        {
            this.options = _options;
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }

        // Entity Configurations specifying one-to-many relationships between entities.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Company>()
                .HasMany(c => c.AddressList)
                .WithOne(c => c.Company)
                .HasForeignKey(f=>f.CompanyId);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Addresses)
                .WithOne(c => c.Country)
                .HasForeignKey(f=>f.CountryId);
        }

        // for migrations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // configures the context to use SQL Server with a specified connection string if options is null.
            if (options == null) {
                optionsBuilder.UseSqlServer("server=34.252.88.110;Database=Camilla;Trusted_Connection=False;");
             }
        }


    }
}
