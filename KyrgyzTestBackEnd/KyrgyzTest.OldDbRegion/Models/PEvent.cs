using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDbRegion.Models;

[Table("P_Events")]
public partial class PEvent
{
    [Key]
    public int EventId { get; set; }

    public int? AccountId { get; set; }

    [StringLength(50)]
    public string EventName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime EventDate { get; set; }

    [StringLength(300)]
    public string EventDescription { get; set; } = null!;

    [StringLength(50)]
    public string? DeviceKey { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("PEvents")]
    public virtual PAccount? Account { get; set; }
}
