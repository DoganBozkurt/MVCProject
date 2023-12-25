using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Icon
	{
		[Key]
		public int IconID { get; set; }
		public string? IIcon { get; set; }
		public string? Title { get; set; }
		[NotMapped]
		public string? TitleWithIcon
		{

			get
			{
				return this.IIcon + " " + this.Title;
			}
		}
	}
}
