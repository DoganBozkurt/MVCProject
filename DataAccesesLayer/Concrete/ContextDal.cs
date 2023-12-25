using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete
{
	public class ContextDal : IdentityDbContext<User, UserRole, int>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DOGAN\\SQLEXPRESS;Database=MVCProjectDB;user='Dogan';password=49;Encrypt=False");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.User)
				.WithMany()
				.HasForeignKey(t => t.UserID)
				.OnDelete(DeleteBehavior.Restrict);
		}

		public  DbSet<Contact> Contacts { get; set; }
		public  DbSet<Icon> Icons { get; set; }
		public  DbSet<Transaction> Transactions { get; set; }

	}
}
