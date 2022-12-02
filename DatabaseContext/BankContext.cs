using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;

namespace DatabaseContext
{
    public class BankContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NPBank;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NaturalEntity>();
            modelBuilder.Entity<LegalEntity>();
            modelBuilder.Entity<Client>().UseTpcMappingStrategy();
            modelBuilder.Entity<ExchangeRate>().HasKey(e => new { e.FromCurrencyID, e.ToCurrencyID });
        }
    }
}