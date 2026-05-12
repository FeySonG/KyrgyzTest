using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("P_AccountRoles")]
public partial class PAccountRole
{
    [Key]
    public int AccountRoleId { get; set; }

    public int AccountId { get; set; }

    [StringLength(20)]
    public string Role { get; set; } = null!;

    [ForeignKey("AccountId")]
    [InverseProperty("PAccountRoles")]
    public virtual PAccount Account { get; set; } = null!;
}
