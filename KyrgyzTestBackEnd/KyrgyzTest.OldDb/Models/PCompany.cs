using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_Companies")]
public partial class PCompany
{
    [Key]
    public int CompanyId { get; set; }

    [StringLength(200)]
    public string CompanyName { get; set; } = null!;

    [StringLength(250)]
    public string CompanyAddress { get; set; } = null!;

    [InverseProperty("Company")]
    public virtual ICollection<PCompanyProperty> PCompanyProperties { get; set; } = new List<PCompanyProperty>();

    [InverseProperty("Company")]
    public virtual ICollection<PGroupRespondent> PGroupRespondents { get; set; } = new List<PGroupRespondent>();

    [InverseProperty("Company")]
    public virtual ICollection<PPaymentAccount> PPaymentAccounts { get; set; } = new List<PPaymentAccount>();
}
