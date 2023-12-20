using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.Concrete;

[Table("Wallet")]
public partial class Wallet
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Income { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Wallets")]
    public virtual User? User { get; set; }
}
