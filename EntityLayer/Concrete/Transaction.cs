using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Transaction
    {
        public int TransactionId { get; set; }
		[ForeignKey("CategoryID")]
		public int CategoryID { get; set; }

		[ForeignKey("Id")]
		public int UserID { get; set; }
		public Category? Category { get; set; }
        public User? User { get; set; }
        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
		[NotMapped]
		public Icon Icon { get; set; }

        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
            }
        }
		[NotMapped]
		public string? CategoryTitleWithIcon
		{
			get
			{
				if (Category == null)
					return "Null";
				//return this.Category.Title + " " + this.Category.TitleWithIcon; tablo ilişkisinden dolayı boş getirdi
				return this.Category.Title;
			}
		}
	}
}
