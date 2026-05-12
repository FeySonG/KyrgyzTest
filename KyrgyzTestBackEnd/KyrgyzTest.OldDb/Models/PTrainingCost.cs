using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_TrainingCost")]
public partial class PTrainingCost
{
    [Key]
    public int TrainingCostId { get; set; }

    public int GroupPropertyValueId { get; set; }

    [Column(TypeName = "money")]
    public decimal Cost { get; set; }

    [Column(TypeName = "money")]
    public decimal Price3rdAttempt { get; set; }

    [ForeignKey("GroupPropertyValueId")]
    [InverseProperty("PTrainingCosts")]
    public virtual PPropertyValue GroupPropertyValue { get; set; } = null!;
}
