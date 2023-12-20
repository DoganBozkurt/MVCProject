using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.Concrete;

public partial class PaymentMethod
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? BankAccountNumber { get; set; }

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
