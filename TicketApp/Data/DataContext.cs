using Microsoft.EntityFrameworkCore;
using TicketApp.Models;

namespace TicketApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { 
        
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Interval> Intervals { get; set; }
        public DbSet<OrderNumber> OrderNumbers { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transfert> Transferts { get; set;}
        public DbSet<Ticket> Tickets  { get; set; }
        public DbSet<TransfertStatus> TransfertsStatus { get; set;}
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<Counter> Counters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransfertStatus>().HasKey(ts => ts.TransferStatusId);
            modelBuilder.Entity<User>()
                .HasOne(p => p.branch)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.BranchFId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasPrincipalKey(p => p.BranchId);

            modelBuilder.Entity<Call>()
                .HasOne(p => p.users)
                .WithMany(p => p.calls)
                .HasForeignKey(p => p.UserFId)
                .HasPrincipalKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Call>()
                .HasOne(p => p.counters)
                .WithMany(p => p.calls)
                .HasForeignKey(p => p.CounterFId)
                .HasPrincipalKey(p => p.CounterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Counter>()
                .HasOne(p => p.branches)
                .WithMany(p => p.counters)
                .HasForeignKey(p => p.BranchFId)
                .HasPrincipalKey(p => p.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(p => p.titles)
                .WithMany(p => p.customers)
                .HasForeignKey(p => p.TitleFId)
                .HasPrincipalKey(p => p.TitleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interval>()
                .HasOne(p => p.currencies)
                .WithMany(p => p.intervals)
                .HasForeignKey(p => p.CurrencyFId)
                .HasPrincipalKey(p => p.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interval>()
                .HasOne(p => p.transferTypes)
                .WithMany(p => p.intervals)
                .HasForeignKey(p => p.TransferTypeFId)
                .HasPrincipalKey(p => p.TransferTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderNumber>()
                .HasOne(p => p.transferTypes)
                .WithMany(p => p.orderNumbers)
                .HasForeignKey(p => p.TransferTypeFId)
                .HasPrincipalKey(p => p.TransferTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderNumber>()
                .HasOne(p => p.branches)
                .WithMany(p => p.orderNumbers)
                .HasForeignKey(p => p.BranchFId)
                .HasPrincipalKey(p => p.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfert>()
                .HasOne(p => p.branches)
                .WithMany(p => p.transferts)
                .HasForeignKey(p => p.BranchFId)
                .HasPrincipalKey(p => p.BranchId)
                .OnDelete(DeleteBehavior.Restrict);        

            modelBuilder.Entity<Transfert>()
                .HasOne(p => p.users)
                .WithMany(p => p.transferts)
                .HasForeignKey(p => p.UserFId)
                .HasPrincipalKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfert>()
                .HasOne(p => p.currencies)
                .WithMany(p => p.transferts)
                .HasForeignKey(p => p.CurrencyFId)
                .HasPrincipalKey(p => p.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfert>()
                .HasOne(p => p.cards)
                .WithMany(p => p.transferts)
                .HasForeignKey(p => p.CardFId)
                .HasPrincipalKey(p => p.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfert>()
                .HasOne(p => p.transfertStatus)
                .WithMany(p => p.transferts)
                .HasForeignKey(p => p.TransferStatusFId)
                .HasPrincipalKey(p => p.TransferStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfert>()
                .HasOne(p => p.intervals)
                .WithMany(p => p.transferts)
                .HasForeignKey(p => p.IntervalFId)
                .HasPrincipalKey(p => p.IntervalId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.transfertStatus)
                .WithMany(p => p.tickets)
                .HasForeignKey(p => p.TransfertStatusFId)
                .HasPrincipalKey(p => p.TransferStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.transferType)
                .WithMany(p => p.tickets)
                .HasForeignKey(p => p.TransfertTypeFId)
                .HasPrincipalKey(p => p.TransferTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.currency)
                .WithMany(p => p.tickets)
                .HasForeignKey(p => p.CurrencyFId)
                .HasPrincipalKey(p => p.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(p => p.user)
                .WithMany(p => p.tickets)
                .HasForeignKey(p => p.UserFId)
                .HasPrincipalKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
