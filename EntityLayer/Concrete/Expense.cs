using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.Concrete;

public partial class Expense
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Column("PaymentMethodID")]
    public int? PaymentMethodId { get; set; }

    [StringLength(100)]
    public string? ExpenseType { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Expenses")]
    public virtual Category? Category { get; set; }

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Expenses")]
    public virtual PaymentMethod? PaymentMethod { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Expenses")]
    public virtual User? User { get; set; }
}
