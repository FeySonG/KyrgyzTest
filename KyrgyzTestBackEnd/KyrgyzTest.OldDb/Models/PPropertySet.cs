using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Keyless]
[Table("P_PropertySet")]
public partial class PPropertySet
{
    public int PropertyId { get; set; }

    [StringLength(50)]
    public string NameSet { get; set; } = null!;

    public int SequenceNumber { get; set; }

    public bool RequiredProperty { get; set; }

    [ForeignKey("PropertyId")]
    public virtual PProperty Property { get; set; } = null!;
}
