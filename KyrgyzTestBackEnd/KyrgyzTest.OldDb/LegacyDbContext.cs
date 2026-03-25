using KyrgyzTest.OldDb.Models;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDb;

public partial class LegacyDbContext : DbContext
{
    public LegacyDbContext()
    {
    }

    public LegacyDbContext(DbContextOptions<LegacyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthorTest> AuthorTests { get; set; }

    public virtual DbSet<BlankTestVariant> BlankTestVariants { get; set; }

    public virtual DbSet<DataOfQuestion> DataOfQuestions { get; set; }

    public virtual DbSet<DatabaseVersion> DatabaseVersions { get; set; }

    public virtual DbSet<ListOfQuestion> ListOfQuestions { get; set; }

    public virtual DbSet<Localization> Localizations { get; set; }

    public virtual DbSet<MmPole> MmPoles { get; set; }

    public virtual DbSet<MonthKg> MonthKgs { get; set; }

    public virtual DbSet<OtchetBlank> OtchetBlanks { get; set; }

    public virtual DbSet<PAccount> PAccounts { get; set; }

    public virtual DbSet<PAccountProperty> PAccountProperties { get; set; }

    public virtual DbSet<PAccountRole> PAccountRoles { get; set; }

    public virtual DbSet<PCompany> PCompanies { get; set; }

    public virtual DbSet<PCompanyProperty> PCompanyProperties { get; set; }

    public virtual DbSet<PEvent> PEvents { get; set; }

    public virtual DbSet<PGroup> PGroups { get; set; }

    public virtual DbSet<PGroupProperty> PGroupProperties { get; set; }

    public virtual DbSet<PGroupRespondent> PGroupRespondents { get; set; }

    public virtual DbSet<PGroupRespondentProperty> PGroupRespondentProperties { get; set; }

    public virtual DbSet<PPaymentAccount> PPaymentAccounts { get; set; }

    public virtual DbSet<PPaymentOperation> PPaymentOperations { get; set; }

    public virtual DbSet<PProperty> PProperties { get; set; }

    public virtual DbSet<PPropertySet> PPropertySets { get; set; }

    public virtual DbSet<PPropertyValue> PPropertyValues { get; set; }

    public virtual DbSet<PTestSetting> PTestSettings { get; set; }

    public virtual DbSet<PTrainingCost> PTrainingCosts { get; set; }

    public virtual DbSet<Regulation> Regulations { get; set; }

    public virtual DbSet<RegulationLog> RegulationLogs { get; set; }

    public virtual DbSet<RrnkTest> RrnkTests { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<Temp> Temps { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<TestResultBallInfo> TestResultBallInfos { get; set; }

    public virtual DbSet<TestStorage> TestStorages { get; set; }

    public virtual DbSet<UserAnswer> UserAnswers { get; set; }

    public virtual DbSet<VBlank1> VBlank1s { get; set; }

    public virtual DbSet<VBlank2> VBlank2s { get; set; }

    public virtual DbSet<VBlank3> VBlank3s { get; set; }

    public virtual DbSet<VBlank4> VBlank4s { get; set; }

    public virtual DbSet<VBlank5> VBlank5s { get; set; }

    public virtual DbSet<VBlank6> VBlank6s { get; set; }

    public virtual DbSet<VBlank7> VBlank7s { get; set; }

    public virtual DbSet<VBlank7Advanced> VBlank7Advanceds { get; set; }

    public virtual DbSet<VStatistik> VStatistiks { get; set; }

    public virtual DbSet<VTest1> VTest1s { get; set; }

    public virtual DbSet<VTest2> VTest2s { get; set; }

    public virtual DbSet<VTest3> VTest3s { get; set; }

    public virtual DbSet<VTest4> VTest4s { get; set; }

    public virtual DbSet<VTest4Advanced> VTest4Advanceds { get; set; }

    public virtual DbSet<VTest5> VTest5s { get; set; }

    public virtual DbSet<VTest5Advanced> VTest5Advanceds { get; set; }

    public virtual DbSet<VUserAnswersUpdate> VUserAnswersUpdates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=AVN_test;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorTest>(entity =>
        {
            entity.Property(e => e.FormatTestType).HasDefaultValue("Standard");
        });

        modelBuilder.Entity<BlankTestVariant>(entity =>
        {
            entity.Property(e => e.GenTime).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<DataOfQuestion>(entity =>
        {
            entity.Property(e => e.ComplexityCountQuestions1).HasDefaultValue(1);
            entity.Property(e => e.ComplexityOfQuestion).HasDefaultValue(1);

            entity.HasOne(d => d.IdTestNavigation).WithMany(p => p.DataOfQuestions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Data_of_questions_List_of_questions");
        });

        modelBuilder.Entity<Localization>(entity => { entity.Property(e => e.TextEn).HasDefaultValue(""); });

        modelBuilder.Entity<MmPole>(entity => { entity.Property(e => e.Id).ValueGeneratedNever(); });

        modelBuilder.Entity<OtchetBlank>(entity => { entity.ToView("otchet_blank"); });

        modelBuilder.Entity<PAccount>(entity =>
        {
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<PAccountProperty>(entity =>
        {
            entity.HasOne(d => d.Account).WithMany(p => p.PAccountProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_AccountProperties_P_Accounts");

            entity.HasOne(d => d.PropertyValue).WithMany(p => p.PAccountProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_AccountProperties_P_PropertyValues");
        });

        modelBuilder.Entity<PAccountRole>(entity =>
        {
            entity.HasOne(d => d.Account).WithMany(p => p.PAccountRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_AccountRoles_P_Accounts");
        });

        modelBuilder.Entity<PCompanyProperty>(entity =>
        {
            entity.HasOne(d => d.Company).WithMany(p => p.PCompanyProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_CompanyProperties_P_Companies");

            entity.HasOne(d => d.PropertyValue).WithMany(p => p.PCompanyProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_CompanyProperties_P_PropertyValues");
        });

        modelBuilder.Entity<PGroup>(entity =>
        {
            entity.Property(e => e.AttemptDate1).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.AttemptDate2).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Editor).WithMany(p => p.PGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_Groups_P_Accounts");

            entity.HasOne(d => d.Regulation).WithMany(p => p.PGroupRegulations)
                .HasConstraintName("FK_P_Groups_Regulation");

            entity.HasOne(d => d.RegulationId2Navigation).WithMany(p => p.PGroupRegulationId2Navigations)
                .HasConstraintName("FK_P_Groups_Regulation2");

            entity.HasOne(d => d.RegulationId3Navigation).WithMany(p => p.PGroupRegulationId3Navigations)
                .HasConstraintName("FK_P_Groups_Regulation3");

            entity.HasOne(d => d.Test).WithMany(p => p.PGroupTests).HasConstraintName("FK_P_Groups_TestStorage");

            entity.HasOne(d => d.TestId2Navigation).WithMany(p => p.PGroupTestId2Navigations)
                .HasConstraintName("FK_P_Groups_TestStorage1");
        });

        modelBuilder.Entity<PGroupProperty>(entity =>
        {
            entity.HasOne(d => d.Group).WithMany(p => p.PGroupProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_GroupProperties_P_Groups");

            entity.HasOne(d => d.PropertyValue).WithMany(p => p.PGroupProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_GroupProperties_P_PropertyValues");
        });

        modelBuilder.Entity<PGroupRespondent>(entity =>
        {
            entity.HasOne(d => d.Account).WithMany(p => p.PGroupRespondents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_GroupRespondent_P_Accounts");

            entity.HasOne(d => d.Company).WithMany(p => p.PGroupRespondents)
                .HasConstraintName("FK_P_GroupRespondent_P_Companies");

            entity.HasOne(d => d.Group).WithMany(p => p.PGroupRespondents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_GroupRespondent_P_Groups");
        });

        modelBuilder.Entity<PGroupRespondentProperty>(entity =>
        {
            entity.HasOne(d => d.GroupRespondent).WithMany(p => p.PGroupRespondentProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_GroupRespondentProperties_P_GroupRespondent");

            entity.HasOne(d => d.PropertyValue).WithMany(p => p.PGroupRespondentProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_GroupRespondentProperties_P_PropertyValues");
        });

        modelBuilder.Entity<PPaymentAccount>(entity =>
        {
            entity.Property(e => e.PaymentAccountId).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.PPaymentAccounts)
                .HasConstraintName("FK_P_PaymentAccounts_P_Companies");

            entity.HasOne(d => d.UserAccount).WithMany(p => p.PPaymentAccounts)
                .HasConstraintName("FK_P_PaymentAccounts_P_Accounts");
        });

        modelBuilder.Entity<PPaymentOperation>(entity =>
        {
            entity.Property(e => e.OperationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TypePay).HasDefaultValue("Default");

            entity.HasOne(d => d.Group).WithMany(p => p.PPaymentOperations)
                .HasConstraintName("FK_P_PaymentOperations_P_Groups");

            entity.HasOne(d => d.PaymentAccount).WithMany(p => p.PPaymentOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_PaymentOperations_P_PaymentAccounts");

            entity.HasOne(d => d.RespondentAccount).WithMany(p => p.PPaymentOperationRespondentAccounts)
                .HasConstraintName("FK_P_PaymentOperations_P_Accounts");

            entity.HasOne(d => d.ResponsibleAccount).WithMany(p => p.PPaymentOperationResponsibleAccounts)
                .HasConstraintName("FK_P_PaymentOperations_P_Accounts1");
        });

        modelBuilder.Entity<PProperty>(entity =>
        {
            entity.Property(e => e.FixedValues).HasDefaultValue(true);
            entity.Property(e => e.SequenceNumber).HasDefaultValue(1);

            entity.HasOne(d => d.DefaultPropertyValue).WithMany(p => p.PProperties)
                .HasConstraintName("FK_P_Properties_P_PropertyValues");

            entity.HasOne(d => d.ParentProperty).WithMany(p => p.InverseParentProperty)
                .HasConstraintName("FK_P_Properties_P_Properties");
        });

        modelBuilder.Entity<PPropertySet>(entity =>
        {
            entity.Property(e => e.SequenceNumber).HasDefaultValue(1);

            entity.HasOne(d => d.Property).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_PropertySet_P_Properties");
        });

        modelBuilder.Entity<PPropertyValue>(entity =>
        {
            entity.Property(e => e.SequenceNumber).HasDefaultValue(1);

            entity.HasOne(d => d.ParentValue).WithMany(p => p.InverseParentValue)
                .HasConstraintName("FK_P_PropertyValues_P_PropertyValues");

            entity.HasOne(d => d.Property).WithMany(p => p.PPropertyValues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_PropertyValues_P_Properties");
        });

        modelBuilder.Entity<PTestSetting>(entity =>
        {
            entity.HasOne(d => d.Regulation).WithMany(p => p.PTestSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_TestSettings_Regulation");

            entity.HasOne(d => d.Test).WithMany(p => p.PTestSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_TestSettings_TestStorage");

            entity.HasOne(d => d.TestPropertyValue).WithMany(p => p.PTestSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_TestSettings_P_PropertyValues");
        });

        modelBuilder.Entity<PTrainingCost>(entity =>
        {
            entity.HasOne(d => d.GroupPropertyValue).WithMany(p => p.PTrainingCosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_P_TrainingCost_P_PropertyValues");
        });

        modelBuilder.Entity<Regulation>(entity =>
        {
            entity.Property(e => e.BallTable).HasDefaultValue("1;2;3;4;5;6;7;8;9;10");
            entity.Property(e => e.CountAttempt).HasDefaultValue(1);
            entity.Property(e => e.FormatTestType).HasDefaultValue("Standard");
            entity.Property(e => e.IdFE).HasDefaultValue(1);
            entity.Property(e => e.RegulationType).HasDefaultValue("Complexity");
            entity.Property(e => e.TestTime).HasDefaultValue(30);
        });

        modelBuilder.Entity<RegulationLog>(entity =>
        {
            entity.Property(e => e.LogTime).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<RrnkTest>(entity =>
        {
            entity.Property(e => e.AvnLogin).HasDefaultValue("-");
            entity.Property(e => e.EditTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FormatTestType).HasDefaultValue("Standard");
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.Property(e => e.Category).HasDefaultValue("1");

            entity.HasOne(d => d.Test).WithMany(p => p.Statistics)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Statistics_TestStorage");
        });

        modelBuilder.Entity<Temp>(entity =>
        {
            entity.ToView("temp");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestResult__7C4F7684");

            entity.ToTable("TestResult", tb => tb.HasTrigger("UPDATE_Rows"));

            entity.Property(e => e.Ball).HasDefaultValue(0m);
            entity.Property(e => e.FormatTestType).HasDefaultValue("Standard");
        });

        modelBuilder.Entity<TestResultBallInfo>(entity =>
        {
            entity.HasOne(d => d.TestResult).WithMany(p => p.TestResultBallInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestResultBallInfo_TestResult");
        });

        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.ToTable(tb =>
            {
                tb.HasTrigger("Insert_Rows");
                tb.HasTrigger("Update_Row");
            });

            entity.Property(e => e.AvnDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<VBlank1>(entity => { entity.ToView("v_blank_1"); });

        modelBuilder.Entity<VBlank2>(entity => { entity.ToView("v_blank_2"); });

        modelBuilder.Entity<VBlank3>(entity => { entity.ToView("v_blank_3"); });

        modelBuilder.Entity<VBlank4>(entity => { entity.ToView("v_blank_4"); });

        modelBuilder.Entity<VBlank5>(entity => { entity.ToView("v_blank_5"); });

        modelBuilder.Entity<VBlank6>(entity => { entity.ToView("v_blank_6"); });

        modelBuilder.Entity<VBlank7>(entity => { entity.ToView("v_blank_7"); });

        modelBuilder.Entity<VBlank7Advanced>(entity => { entity.ToView("v_blank_7_advanced"); });

        modelBuilder.Entity<VStatistik>(entity => { entity.ToView("V_statistik"); });

        modelBuilder.Entity<VTest1>(entity => { entity.ToView("v_test_1"); });

        modelBuilder.Entity<VTest2>(entity => { entity.ToView("v_test_2"); });

        modelBuilder.Entity<VTest3>(entity => { entity.ToView("v_test_3"); });

        modelBuilder.Entity<VTest4>(entity => { entity.ToView("v_test_4"); });

        modelBuilder.Entity<VTest4Advanced>(entity => { entity.ToView("v_test_4_advanced"); });

        modelBuilder.Entity<VTest5>(entity => { entity.ToView("v_test_5"); });

        modelBuilder.Entity<VTest5Advanced>(entity => { entity.ToView("v_test_5_advanced"); });

        modelBuilder.Entity<VUserAnswersUpdate>(entity => { entity.ToView("V_UserAnswers_update"); });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}