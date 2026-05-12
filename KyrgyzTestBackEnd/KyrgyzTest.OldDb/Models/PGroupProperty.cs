using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_GroupProperties")]
public partial class PGroupProperty
{
    [Key]
    public int GroupPropertyId { get; set; }

    public int GroupId { get; set; }

    public int PropertyValueId { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("PGroupProperties")]
    public virtual PGroup Group { get; set; } = null!;

    [ForeignKey("PropertyValueId")]
    [InverseProperty("PGroupProperties")]
    public virtual PPropertyValue PropertyValue { get; set; } = null!;
}
