using System;
using System.Collections.Generic;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.Repository;

public partial class MVCProjectDBDbContext : DbContext
{
    public MVCProjectDBDbContext()
    {
    }

    public MVCProjectDBDbContext(DbContextOptions<MVCProjectDBDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DOGAN\\SQLEXPRESS;Database=MVCProjectDB;user='Dogan';password=49;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27F3895625");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3214EC27E1EFD123");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expenses__3214EC272101176E");

            entity.HasOne(d => d.Category).WithMany(p => p.Expenses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Expenses__Catego__2645B050");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Expenses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Expenses__Paymen__2739D489");

            entity.HasOne(d => d.User).WithMany(p => p.Expenses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Expenses__UserID__25518C17");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC276C538C85");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC2784270112");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wallet__3214EC277EFCC8F0");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Wallet__UserID__1EA48E88");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
