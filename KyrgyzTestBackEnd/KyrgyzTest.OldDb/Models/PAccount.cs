using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_Accounts")]
public partial class PAccount
{
    [Key]
    public int AccountId { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(50)]
    public string Login { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    public byte[]? Photo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RegistrationDate { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<PAccountProperty> PAccountProperties { get; set; } = new List<PAccountProperty>();

    [InverseProperty("Account")]
    public virtual ICollection<PAccountRole> PAccountRoles { get; set; } = new List<PAccountRole>();

    [InverseProperty("Account")]
    public virtual ICollection<PGroupRespondent> PGroupRespondents { get; set; } = new List<PGroupRespondent>();

    [InverseProperty("Editor")]
    public virtual ICollection<PGroup> PGroups { get; set; } = new List<PGroup>();

    [InverseProperty("UserAccount")]
    public virtual ICollection<PPaymentAccount> PPaymentAccounts { get; set; } = new List<PPaymentAccount>();

    [InverseProperty("RespondentAccount")]
    public virtual ICollection<PPaymentOperation> PPaymentOperationRespondentAccounts { get; set; } = new List<PPaymentOperation>();

    [InverseProperty("ResponsibleAccount")]
    public virtual ICollection<PPaymentOperation> PPaymentOperationResponsibleAccounts { get; set; } = new List<PPaymentOperation>();
}
