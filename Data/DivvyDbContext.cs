using Divvy.Models;
using Microsoft.EntityFrameworkCore;

namespace Divvy.Data
{
    public class DivvyDbContext(DbContextOptions<DivvyDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<Group> Groups { get; set; }
        public required DbSet<GroupMember> GroupMembers { get; set; }
        public required DbSet<Expense> Expenses { get; set; }
        public required DbSet<ExpenseSplit> ExpenseSplits { get; set; }
        public required DbSet<Settlement> Settlements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User Entity

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired();

            #endregion User Entity

            #region Group Entity

            modelBuilder.Entity<Group>()
                .HasKey(g => g.GroupId);

            modelBuilder.Entity<Group>()
                .Property(g => g.GroupName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .HasOne(gm => gm.CreatedBy)
                .WithMany(g => g.GroupsCreated)
                .HasForeignKey(gm => gm.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion Group Entity

            #region GroupMember Entity (Join table for User and Group)

            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.GroupId, gm.UserId });

            modelBuilder.Entity<GroupMember>()
                .Property(gm => gm.JoinedAt)
                .IsRequired();

            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.GroupMembers)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion GroupMember Entity (Join table for User and Group)

            #region Expense Entity

            modelBuilder.Entity<Expense>()
                .HasKey(e => e.ExpenseId);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Description)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .Property(e => e.Date)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Expenses)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.PaidBy)
                .WithMany(u => u.ExpensesPaid)
                .HasForeignKey(e => e.PaidById)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion Expense Entity

            #region ExpenseSplit Entity (Join table for User and Expense)

            modelBuilder.Entity<ExpenseSplit>()
                .HasKey(es => new { es.ExpenseId, es.UserId });

            modelBuilder.Entity<ExpenseSplit>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<ExpenseSplit>()
                .HasOne(es => es.Expense)
                .WithMany(e => e.ExpenseSplits)
                .HasForeignKey(es => es.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpenseSplit>()
                .HasOne(es => es.User)
                .WithMany(u => u.ExpenseSplits)
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion ExpenseSplit Entity (Join table for User and Expense)

            #region Settlement Entity

            modelBuilder.Entity<Settlement>()
                .HasKey(s => s.SettlementId);

            modelBuilder.Entity<Settlement>()
                .Property(s => s.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<Settlement>()
                .Property(s => s.Date)
                .IsRequired();

            modelBuilder.Entity<Settlement>()
                .HasOne(s => s.Group)
                .WithMany(u => u.Settlements)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Settlement>()
                .HasOne(s => s.Payer)
                .WithMany(u => u.SettlementsMade)
                .HasForeignKey(s => s.PayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Settlement>()
                .HasOne(s => s.Receiver)
                .WithMany(u => u.SettlementsReceived)
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion Settlement Entity
        }
    }
}