using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
		public int UserID { get; set; }
		public User? User { get; set; }

		[Required(ErrorMessage = "Title is required.")]
		public string? Title { get; set; }

		// İlişkiyi temsil etmek için kullanılan property
		public int IconID { get; set; }
        public Icon IconData { get; set; }

		[Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; } = "Expense";

		[NotMapped]
		public string? TitleWithIcon
		{
			get
			{
				if (IconData == null || IconData.IIcon.IsNullOrEmpty())
					return "No icon";
				return this.IconData.IIcon + " " + this.Title;
			}
		}
	}
}
