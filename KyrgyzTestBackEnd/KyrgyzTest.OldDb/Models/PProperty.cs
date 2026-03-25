using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_Properties")]
public partial class PProperty
{
    [Key]
    public int PropertyId { get; set; }

    [StringLength(50)]
    public string PropertyName { get; set; } = null!;

    [StringLength(20)]
    public string PropertyType { get; set; } = null!;

    public bool FixedValues { get; set; }

    public int SequenceNumber { get; set; }

    public int? ParentPropertyId { get; set; }

    public int? DefaultPropertyValueId { get; set; }

    [ForeignKey("DefaultPropertyValueId")]
    [InverseProperty("PProperties")]
    public virtual PPropertyValue? DefaultPropertyValue { get; set; }

    [InverseProperty("ParentProperty")]
    public virtual ICollection<PProperty> InverseParentProperty { get; set; } = new List<PProperty>();

    [InverseProperty("Property")]
    public virtual ICollection<PPropertyValue> PPropertyValues { get; set; } = new List<PPropertyValue>();

    [ForeignKey("ParentPropertyId")]
    [InverseProperty("InverseParentProperty")]
    public virtual PProperty? ParentProperty { get; set; }
}
