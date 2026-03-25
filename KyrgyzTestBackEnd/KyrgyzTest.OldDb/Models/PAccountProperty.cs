using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_AccountProperties")]
public partial class PAccountProperty
{
    [Key]
    public int AccountPropertyId { get; set; }

    public int AccountId { get; set; }

    public int PropertyValueId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("PAccountProperties")]
    public virtual PAccount Account { get; set; } = null!;

    [ForeignKey("PropertyValueId")]
    [InverseProperty("PAccountProperties")]
    public virtual PPropertyValue PropertyValue { get; set; } = null!;
}
