using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ContextDal:DbContext//IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DOGAN\\SQLEXPRESS;Database=MVCProjectDB;user='Dogan';password=49;Encrypt=False");
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }

        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Category2> Categories2 { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}


