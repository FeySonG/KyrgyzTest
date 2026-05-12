using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_PaymentAccounts")]
public partial class PPaymentAccount
{
    [Key]
    public int PaymentAccountId { get; set; }

    public int? UserAccountId { get; set; }

    public int? CompanyId { get; set; }

    [Column(TypeName = "money")]
    public decimal Balance { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("PPaymentAccounts")]
    public virtual PCompany? Company { get; set; }

    [InverseProperty("PaymentAccount")]
    public virtual ICollection<PPaymentOperation> PPaymentOperations { get; set; } = new List<PPaymentOperation>();

    [ForeignKey("UserAccountId")]
    [InverseProperty("PPaymentAccounts")]
    public virtual PAccount? UserAccount { get; set; }
}
