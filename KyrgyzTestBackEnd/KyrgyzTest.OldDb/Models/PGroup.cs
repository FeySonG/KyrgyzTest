using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_Groups")]
public partial class PGroup
{
    [Key]
    public int GroupId { get; set; }

    [StringLength(50)]
    public string GroupName { get; set; } = null!;

    public int? TestId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime BeginDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    public int? RegulationId { get; set; }

    public int EditorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EditTime { get; set; }

    public bool Active { get; set; }

    public int? RegulationId2 { get; set; }

    public int? RegulationId3 { get; set; }

    public DateOnly AttemptDate1 { get; set; }

    public DateOnly AttemptDate2 { get; set; }

    public int? TestId2 { get; set; }

    [StringLength(20)]
    public string? AttemptTimeInterval1 { get; set; }

    [StringLength(20)]
    public string? AttemptTimeInterval2 { get; set; }

    [ForeignKey("EditorId")]
    [InverseProperty("PGroups")]
    public virtual PAccount Editor { get; set; } = null!;

    [InverseProperty("Group")]
    public virtual ICollection<PGroupProperty> PGroupProperties { get; set; } = new List<PGroupProperty>();

    [InverseProperty("Group")]
    public virtual ICollection<PGroupRespondent> PGroupRespondents { get; set; } = new List<PGroupRespondent>();

    [InverseProperty("Group")]
    public virtual ICollection<PPaymentOperation> PPaymentOperations { get; set; } = new List<PPaymentOperation>();

    [ForeignKey("RegulationId")]
    [InverseProperty("PGroupRegulations")]
    public virtual Regulation? Regulation { get; set; }

    [ForeignKey("RegulationId2")]
    [InverseProperty("PGroupRegulationId2Navigations")]
    public virtual Regulation? RegulationId2Navigation { get; set; }

    [ForeignKey("RegulationId3")]
    [InverseProperty("PGroupRegulationId3Navigations")]
    public virtual Regulation? RegulationId3Navigation { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("PGroupTests")]
    public virtual TestStorage? Test { get; set; }

    [ForeignKey("TestId2")]
    [InverseProperty("PGroupTestId2Navigations")]
    public virtual TestStorage? TestId2Navigation { get; set; }
}
