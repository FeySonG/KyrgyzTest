using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_GroupRespondent")]
public partial class PGroupRespondent
{
    [Key]
    public int GroupRespondentId { get; set; }

    public int GroupId { get; set; }

    public int AccountId { get; set; }

    [StringLength(20)]
    public string UserType { get; set; } = null!;

    public DateOnly? AttemptDate3 { get; set; }

    public DateOnly? AttemptDate2 { get; set; }

    [StringLength(100)]
    public string? Post { get; set; }

    public int? CompanyId { get; set; }

    [StringLength(100)]
    public string? Address { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("PGroupRespondents")]
    public virtual PAccount Account { get; set; } = null!;

    [ForeignKey("CompanyId")]
    [InverseProperty("PGroupRespondents")]
    public virtual PCompany? Company { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("PGroupRespondents")]
    public virtual PGroup Group { get; set; } = null!;

    [InverseProperty("GroupRespondent")]
    public virtual ICollection<PGroupRespondentProperty> PGroupRespondentProperties { get; set; } = new List<PGroupRespondentProperty>();
}
