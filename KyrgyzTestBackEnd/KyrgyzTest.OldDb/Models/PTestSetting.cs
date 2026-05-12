using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_TestSettings")]
public partial class PTestSetting
{
    [Key]
    public int TestSettingsId { get; set; }

    public int TestId { get; set; }

    public int TestPropertyValueId { get; set; }

    public int GroupPropertyValueId { get; set; }

    public int RegulationId { get; set; }

    [ForeignKey("RegulationId")]
    [InverseProperty("PTestSettings")]
    public virtual Regulation Regulation { get; set; } = null!;

    [ForeignKey("TestId")]
    [InverseProperty("PTestSettings")]
    public virtual TestStorage Test { get; set; } = null!;

    [ForeignKey("TestPropertyValueId")]
    [InverseProperty("PTestSettings")]
    public virtual PPropertyValue TestPropertyValue { get; set; } = null!;
}
