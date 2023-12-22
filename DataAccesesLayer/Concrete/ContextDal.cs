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

		public  DbSet<Contact> Contacts { get; set; }
		public  DbSet<Transaction> Transactions { get; set; }

	}
}
