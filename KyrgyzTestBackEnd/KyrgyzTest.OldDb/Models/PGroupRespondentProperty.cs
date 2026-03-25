using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_GroupRespondentProperties")]
public partial class PGroupRespondentProperty
{
    [Key]
    public int GroupRespondentPropertyId { get; set; }

    public int GroupRespondentId { get; set; }

    public int PropertyValueId { get; set; }

    [ForeignKey("GroupRespondentId")]
    [InverseProperty("PGroupRespondentProperties")]
    public virtual PGroupRespondent GroupRespondent { get; set; } = null!;

    [ForeignKey("PropertyValueId")]
    [InverseProperty("PGroupRespondentProperties")]
    public virtual PPropertyValue PropertyValue { get; set; } = null!;
}
