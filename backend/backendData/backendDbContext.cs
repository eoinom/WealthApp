using Microsoft.EntityFrameworkCore;
using backendData.Models;
using System.Linq;

namespace backendData
{
    public class backendDbContext : DbContext
    {
        public backendDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AccountValue> AccountValues { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<CryptoAccount> CryptoAccounts { get; set; }
        public DbSet<CryptoAccountValue> CryptoAccountValues { get; set; }
        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<CryptoValue> CryptoValues { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetValue> AssetValues { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountValue>()
                .HasOne(b => b.Account)
                .WithMany(a => a.AccountValues)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(b => b.AccountValues)
                .WithOne(a => a.Account)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
