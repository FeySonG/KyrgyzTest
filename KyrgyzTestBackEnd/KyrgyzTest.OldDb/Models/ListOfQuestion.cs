using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("List_of_questions")]
public partial class ListOfQuestion
{
    [Key]
    [Column("id_test")]
    public int IdTest { get; set; }

    public string? Semestr { get; set; }

    public string? Disciplina { get; set; }

    [Column("Count_of_questions")]
    public int? CountOfQuestions { get; set; }

    public string? FioPrepoda { get; set; }

    public string? Gruppy { get; set; }

    public bool IsAnnulled { get; set; }

    [InverseProperty("IdTestNavigation")]
    public virtual ICollection<DataOfQuestion> DataOfQuestions { get; set; } = new List<DataOfQuestion>();
}
