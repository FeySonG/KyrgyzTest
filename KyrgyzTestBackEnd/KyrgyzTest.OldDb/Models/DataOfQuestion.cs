using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb.Models;

[Table("Data_of_questions")]
public partial class DataOfQuestion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_test")]
    public int? IdTest { get; set; }

    [Column("num_quest")]
    public int? NumQuest { get; set; }

    public byte[]? Vopros { get; set; }

    [Column("pOtv")]
    public byte[]? POtv { get; set; }

    [Column("nOtv1")]
    public byte[]? NOtv1 { get; set; }

    [Column("nOtv2")]
    public byte[]? NOtv2 { get; set; }

    [Column("nOtv3")]
    public byte[]? NOtv3 { get; set; }

    [Column("Cash_blank_Vopros")]
    public byte[]? CashBlankVopros { get; set; }

    [Column("Cash_blank_pOtv")]
    public byte[]? CashBlankPOtv { get; set; }

    [Column("Cash_blank_nOtv1")]
    public byte[]? CashBlankNOtv1 { get; set; }

    [Column("Cash_blank_nOtv2")]
    public byte[]? CashBlankNOtv2 { get; set; }

    [Column("Cash_blank_nOtv3")]
    public byte[]? CashBlankNOtv3 { get; set; }

    [Column("Cash_web_Vopros")]
    public byte[]? CashWebVopros { get; set; }

    [Column("Cash_web_pOtv")]
    public byte[]? CashWebPOtv { get; set; }

    [Column("Cash_web_nOtv1")]
    public byte[]? CashWebNOtv1 { get; set; }

    [Column("Cash_web_nOtv2")]
    public byte[]? CashWebNOtv2 { get; set; }

    [Column("Cash_web_nOtv3")]
    public byte[]? CashWebNOtv3 { get; set; }

    public int ComplexityCountQuestions1 { get; set; }

    public int ComplexityOfQuestion { get; set; }

    [ForeignKey("IdTest")]
    [InverseProperty("DataOfQuestions")]
    public virtual ListOfQuestion? IdTestNavigation { get; set; }
}
