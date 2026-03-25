using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_PropertyValues")]
public partial class PPropertyValue
{
    [Key]
    public int PropertyValueId { get; set; }

    public string PropertyValue { get; set; } = null!;

    public int PropertyId { get; set; }

    public int? ParentValueId { get; set; }

    public int SequenceNumber { get; set; }

    [InverseProperty("ParentValue")]
    public virtual ICollection<PPropertyValue> InverseParentValue { get; set; } = new List<PPropertyValue>();

    [InverseProperty("PropertyValue")]
    public virtual ICollection<PAccountProperty> PAccountProperties { get; set; } = new List<PAccountProperty>();

    [InverseProperty("PropertyValue")]
    public virtual ICollection<PCompanyProperty> PCompanyProperties { get; set; } = new List<PCompanyProperty>();

    [InverseProperty("PropertyValue")]
    public virtual ICollection<PGroupProperty> PGroupProperties { get; set; } = new List<PGroupProperty>();

    [InverseProperty("PropertyValue")]
    public virtual ICollection<PGroupRespondentProperty> PGroupRespondentProperties { get; set; } = new List<PGroupRespondentProperty>();

    [InverseProperty("DefaultPropertyValue")]
    public virtual ICollection<PProperty> PProperties { get; set; } = new List<PProperty>();

    [InverseProperty("TestPropertyValue")]
    public virtual ICollection<PTestSetting> PTestSettings { get; set; } = new List<PTestSetting>();

    [InverseProperty("GroupPropertyValue")]
    public virtual ICollection<PTrainingCost> PTrainingCosts { get; set; } = new List<PTrainingCost>();

    [ForeignKey("ParentValueId")]
    [InverseProperty("InverseParentValue")]
    public virtual PPropertyValue? ParentValue { get; set; }

    [ForeignKey("PropertyId")]
    [InverseProperty("PPropertyValues")]
    public virtual PProperty Property { get; set; } = null!;
}
