using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_PaymentOperations")]
public partial class PPaymentOperation
{
    [Key]
    public int PaymentOperationId { get; set; }

    public int PaymentAccountId { get; set; }

    public int? RespondentAccountId { get; set; }

    public int? GroupId { get; set; }

    [Column(TypeName = "money")]
    public decimal Summ { get; set; }

    [StringLength(50)]
    public string OperationType { get; set; } = null!;

    public int? ResponsibleAccountId { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OperationDate { get; set; }

    [StringLength(20)]
    public string TypePay { get; set; } = null!;

    [StringLength(50)]
    public string? NumberPay { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DatePay { get; set; }

    public int? CanceledAccountId { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("PPaymentOperations")]
    public virtual PGroup? Group { get; set; }

    [ForeignKey("PaymentAccountId")]
    [InverseProperty("PPaymentOperations")]
    public virtual PPaymentAccount PaymentAccount { get; set; } = null!;

    [ForeignKey("RespondentAccountId")]
    [InverseProperty("PPaymentOperationRespondentAccounts")]
    public virtual PAccount? RespondentAccount { get; set; }

    [ForeignKey("ResponsibleAccountId")]
    [InverseProperty("PPaymentOperationResponsibleAccounts")]
    public virtual PAccount? ResponsibleAccount { get; set; }
}
