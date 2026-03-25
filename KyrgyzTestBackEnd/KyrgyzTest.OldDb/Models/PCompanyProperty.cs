using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_CompanyProperties")]
public partial class PCompanyProperty
{
    [Key]
    public int CompanyPropertyId { get; set; }

    public int CompanyId { get; set; }

    public int PropertyValueId { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("PCompanyProperties")]
    public virtual PCompany Company { get; set; } = null!;

    [ForeignKey("PropertyValueId")]
    [InverseProperty("PCompanyProperties")]
    public virtual PPropertyValue PropertyValue { get; set; } = null!;
}
