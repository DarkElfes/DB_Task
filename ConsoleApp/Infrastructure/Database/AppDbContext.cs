using ConsoleApp.Domain.AutomaticTellerMachines;
using ConsoleApp.Domain.Cards;
using ConsoleApp.Domain.Merchants;
using ConsoleApp.Domain.OnlineServices;
using ConsoleApp.Domain.Transactions;
using ConsoleApp.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Infrastructure.Database;

internal class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<ATM> ATMs { get; set; }
    public DbSet<Location> Locations { get; set; }

    public DbSet<Merchant> Merchants { get; set; }
    public DbSet<OnlineService> OnlineServices { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=BankDb;Trusted_Connection=True;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User entity configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.Email)
                .IsRequired();

            entity.Property(u => u.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(u => u.Password)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.DateOfBirth)
                .IsRequired();

            entity.HasMany(u => u.Cards)
                .WithOne(c => c.User)
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
        });


        // Card entity configuration
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Number)
                .HasMaxLength(16)
                .IsRequired();

            entity.Property(c => c.Balance)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(c => c.Currency)
                .HasConversion<string>()
                .HasMaxLength(3)
                .IsRequired();

            entity.Property(c => c.IsBlocked)
                .IsRequired();

            entity.Property(c => c.ExpirationDate)
                .IsRequired();

            entity.Property(c => c.Pin)
                .HasMaxLength(4)
                .IsRequired();

            entity.Property(c => c.CVV)
                .HasMaxLength(3)
                .IsRequired();

            entity.HasMany(c => c.OwnTransactions)
                .WithOne(t => t.Card)
                .HasForeignKey("CardId")
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(c => c.ReceivedTransactions)
                .WithOne(t => t.RecipientCard)
                .HasForeignKey("RecipientCardId")
                .OnDelete(DeleteBehavior.NoAction);
        });

        // Transaction entity configuration
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(t => t.TimeStamp)
                .IsRequired();

            entity.Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(9)
                .IsRequired();

            entity.Property(t => t.CardBalance)
                .HasPrecision(18, 2)
                .IsRequired();

            // Configure the discriminator column for the Transaction entity    
            entity.HasDiscriminator<string>("TransactionType")
                .HasValue<TransferTransaction>("Transfer")
                .HasValue<ATMTransaction>("ATM")
                .HasValue<OnlineServiceTransaction>("OnlineService");
        });

        // TransferTransaction entity configuration
        modelBuilder.Entity<TransferTransaction>(entity =>
        {
            entity.Property(tt => tt.RecipientCardBalance)
                .HasPrecision(18, 2)
                .IsRequired();
        });

        // ATMTransaction entity configuration
        modelBuilder.Entity<ATMTransaction>(entity =>
        {
            entity.Property(a => a.ATMTransactionType)
                .HasConversion<string>()
                .HasMaxLength (8)
                .IsRequired();
        });

        // OnlineServiceTransaction entity configuration
        modelBuilder.Entity<OnlineServiceTransaction>(entity =>
        {
            entity.Property(o => o.ServiceReceiptNumber)
                .HasMaxLength(8)
                .IsRequired();
        });


        // Location entity config
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.City)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(l => l.Address)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasMany(l => l.ATMs)
                .WithOne(a => a.Location)
                .HasForeignKey("LocationId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        // AutomaticTellerMachine entity config
        modelBuilder.Entity<ATM>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.WorkStatus)
                .HasConversion<string>()
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(a => a.Balance)
                .HasPrecision(18, 2)
                .IsRequired();
        });


        // OnlineService entity config
        modelBuilder.Entity<OnlineService>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.Name)
                .HasMaxLength(50)
                .IsRequired();
        });

        // Merchant entity config
        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(m => m.Number)
                .HasMaxLength(6)
                .IsRequired();

            entity.HasMany(m => m.OnlineServices)
                .WithOne(o => o.Merchant)
                .HasForeignKey("MerchantId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

    }
}
