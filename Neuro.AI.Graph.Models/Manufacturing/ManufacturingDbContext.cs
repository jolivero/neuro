using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ManufacturingDbContext : DbContext
{
    public ManufacturingDbContext()
    {
    }

    public ManufacturingDbContext(DbContextOptions<ManufacturingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChangeRequestDetail> ChangeRequestDetails { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<DailyPlanning> DailyPlannings { get; set; }

    public virtual DbSet<DailyTask> DailyTasks { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<MachineReport> MachineReports { get; set; }

    public virtual DbSet<MonthlyPlanning> MonthlyPlannings { get; set; }

    public virtual DbSet<NonCompliantPartsRecord> NonCompliantPartsRecords { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<ProducedPartsRecord> ProducedPartsRecords { get; set; }

    public virtual DbSet<ProductionChangeRequest> ProductionChangeRequests { get; set; }

    public virtual DbSet<ProductionLine> ProductionLines { get; set; }

    public virtual DbSet<ProductionLineRecipe> ProductionLineRecipes { get; set; }

    public virtual DbSet<ProductionLineRecipeMaterial> ProductionLineRecipeMaterials { get; set; }

    public virtual DbSet<ProductionRecord> ProductionRecords { get; set; }

    public virtual DbSet<QualityClasification> QualityClasifications { get; set; }

    public virtual DbSet<QualityRecord> QualityRecords { get; set; }

    public virtual DbSet<RequestCategory> RequestCategories { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Turn> Turns { get; set; }

    public virtual DbSet<TurnDetail> TurnDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersSkill> UsersSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChangeRequestDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__ChangeRe__135C316D5881E622");

            entity.ToTable("ChangeRequestDetail");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentTime).HasMaxLength(255);
            entity.Property(e => e.CurrentValue).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.NewTime).HasMaxLength(255);
            entity.Property(e => e.NewValue).HasMaxLength(255);
            entity.Property(e => e.RequestType).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CurrentGroup).WithMany(p => p.ChangeRequestDetailCurrentGroups)
                .HasForeignKey(d => d.CurrentGroupId)
                .HasConstraintName("FK__ChangeReq__Curre__59511E61");

            entity.HasOne(d => d.CurrentLine).WithMany(p => p.ChangeRequestDetailCurrentLines)
                .HasForeignKey(d => d.CurrentLineId)
                .HasConstraintName("FK__ChangeReq__Curre__5768D5EF");

            entity.HasOne(d => d.CurrentMachine).WithMany(p => p.ChangeRequestDetailCurrentMachines)
                .HasForeignKey(d => d.CurrentMachineId)
                .HasConstraintName("FK__ChangeReq__Curre__5D21AF45");

            entity.HasOne(d => d.CurrentPart).WithMany(p => p.ChangeRequestDetailCurrentParts)
                .HasForeignKey(d => d.CurrentPartId)
                .HasConstraintName("FK__ChangeReq__Curre__60F24029");

            entity.HasOne(d => d.CurrentStation).WithMany(p => p.ChangeRequestDetailCurrentStations)
                .HasForeignKey(d => d.CurrentStationId)
                .HasConstraintName("FK__ChangeReq__Curre__5B3966D3");

            entity.HasOne(d => d.CurrentTurn).WithMany(p => p.ChangeRequestDetailCurrentTurns)
                .HasForeignKey(d => d.CurrentTurnId)
                .HasConstraintName("FK__ChangeReq__Curre__5F09F7B7");

            entity.HasOne(d => d.CurrentUser).WithMany(p => p.ChangeRequestDetailCurrentUsers)
                .HasForeignKey(d => d.CurrentUserId)
                .HasConstraintName("FK__ChangeReq__Curre__55808D7D");

            entity.HasOne(d => d.NewGroup).WithMany(p => p.ChangeRequestDetailNewGroups)
                .HasForeignKey(d => d.NewGroupId)
                .HasConstraintName("FK__ChangeReq__NewGr__5A45429A");

            entity.HasOne(d => d.NewLine).WithMany(p => p.ChangeRequestDetailNewLines)
                .HasForeignKey(d => d.NewLineId)
                .HasConstraintName("FK__ChangeReq__NewLi__585CFA28");

            entity.HasOne(d => d.NewMachine).WithMany(p => p.ChangeRequestDetailNewMachines)
                .HasForeignKey(d => d.NewMachineId)
                .HasConstraintName("FK__ChangeReq__NewMa__5E15D37E");

            entity.HasOne(d => d.NewPart).WithMany(p => p.ChangeRequestDetailNewParts)
                .HasForeignKey(d => d.NewPartId)
                .HasConstraintName("FK__ChangeReq__NewPa__61E66462");

            entity.HasOne(d => d.NewStation).WithMany(p => p.ChangeRequestDetailNewStations)
                .HasForeignKey(d => d.NewStationId)
                .HasConstraintName("FK__ChangeReq__NewSt__5C2D8B0C");

            entity.HasOne(d => d.NewTurn).WithMany(p => p.ChangeRequestDetailNewTurns)
                .HasForeignKey(d => d.NewTurnId)
                .HasConstraintName("FK__ChangeReq__NewTu__5FFE1BF0");

            entity.HasOne(d => d.NewUser).WithMany(p => p.ChangeRequestDetailNewUsers)
                .HasForeignKey(d => d.NewUserId)
                .HasConstraintName("FK__ChangeReq__NewUs__5674B1B6");

            entity.HasOne(d => d.Request).WithMany(p => p.ChangeRequestDetails)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__ChangeReq__Reque__52A420D2");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971CAC6DF17B75");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CompanyAddress).HasMaxLength(255);
            entity.Property(e => e.CompanyColors).HasMaxLength(255);
            entity.Property(e => e.CompanyLogo).HasMaxLength(255);
            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.CompanyPhone).HasMaxLength(255);
            entity.Property(e => e.CompanyRuc).HasMaxLength(255);
            entity.Property(e => e.CompanyWeb).HasMaxLength(255);
            entity.Property(e => e.ContactEmail).HasMaxLength(255);
            entity.Property(e => e.ContactName).HasMaxLength(255);
            entity.Property(e => e.ContactPhone).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Companies__Creat__66AB197F");
        });

        modelBuilder.Entity<DailyPlanning>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("PK__DailyPla__BF3DD8C5F4E05915");

            entity.ToTable("DailyPlanning");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DayType).HasMaxLength(255);
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Month).WithMany(p => p.DailyPlannings)
                .HasForeignKey(d => d.MonthId)
                .HasConstraintName("FK__DailyPlan__Month__3CB4DFB3");
        });

        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__DailyTas__7C6949B169740CD5");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MachineStatus).HasMaxLength(255);
            entity.Property(e => e.OperatorStatus).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Day).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("FK__DailyTask__DayId__3DA903EC");

            entity.HasOne(d => d.Machine).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__DailyTask__Machi__4456017B");

            entity.HasOne(d => d.Station).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__DailyTask__Stati__4361DD42");

            entity.HasOne(d => d.Turn).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.TurnId)
                .HasConstraintName("FK__DailyTask__TurnI__3BC0BB7A");

            entity.HasOne(d => d.User).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DailyTask__UserI__454A25B4");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36A7483DFD8");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Groups__CreatedB__4CEB477C");

            entity.HasOne(d => d.Line).WithMany(p => p.Groups)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__Groups__LineId__3513BDEB");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6B386160D7E");

            entity.ToTable("Inventory");

            entity.HasIndex(e => e.PartId, "UQ__Inventor__7C3F0D51A7F7BA95").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.PartId)
                .HasConstraintName("FK__Inventory__PartI__4A0EDAD1");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__logs__5E5486483631C66E");

            entity.ToTable("logs");

            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.Area).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Desc0)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Desc1)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Desc2)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Desc3)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Rol).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__Machines__44EE5B3875078F0F");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EnergyConsumption).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Machines)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Machines__Create__4FC7B427");
        });

        modelBuilder.Entity<MachineReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__MachineR__D5BD4805F73AE1BC");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Machine).WithMany(p => p.MachineReports)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__MachineRe__Machi__37F02A96");

            entity.HasOne(d => d.Operator).WithMany(p => p.MachineReportOperators)
                .HasForeignKey(d => d.OperatorId)
                .HasConstraintName("FK__MachineRe__Opera__4826925F");

            entity.HasOne(d => d.Station).WithMany(p => p.MachineReports)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__MachineRe__Stati__38E44ECF");

            entity.HasOne(d => d.Technical).WithMany(p => p.MachineReportTechnicals)
                .HasForeignKey(d => d.TechnicalId)
                .HasConstraintName("FK__MachineRe__Techn__491AB698");
        });

        modelBuilder.Entity<MonthlyPlanning>(entity =>
        {
            entity.HasKey(e => e.MonthId).HasName("PK__MonthlyP__9FA83FA611F7A9E2");

            entity.ToTable("MonthlyPlanning");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Line).WithMany(p => p.MonthlyPlannings)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__MonthlyPl__LineI__3ACC9741");

            entity.HasOne(d => d.PlannedByNavigation).WithMany(p => p.MonthlyPlannings)
                .HasForeignKey(d => d.PlannedBy)
                .HasConstraintName("FK__MonthlyPl__Plann__50BBD860");
        });

        modelBuilder.Entity<NonCompliantPartsRecord>(entity =>
        {
            entity.HasKey(e => e.NcPartId).HasName("PK__NonCompl__A881183A4C9A1B9D");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithMany(p => p.NonCompliantPartsRecords)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__NonCompli__PartI__426DB909");

            entity.HasOne(d => d.Task).WithMany(p => p.NonCompliantPartsRecords)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__NonCompli__TaskI__40857097");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__Parts__7C3F0D50940A2FC7");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Parts)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Parts__CreatedBy__4ED38FEE");

            entity.HasOne(d => d.Station).WithMany(p => p.Parts)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__Parts__StationId__36FC065D");
        });

        modelBuilder.Entity<ProducedPartsRecord>(entity =>
        {
            entity.HasKey(e => e.ProducedPartId).HasName("PK__Produced__06E2CB4BEB709F2B");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithMany(p => p.ProducedPartsRecords)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__ProducedP__PartI__417994D0");

            entity.HasOne(d => d.Task).WithMany(p => p.ProducedPartsRecords)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__ProducedP__TaskI__3F914C5E");
        });

        modelBuilder.Entity<ProductionChangeRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Producti__33A8517A3C247BEE");

            entity.ToTable("ProductionChangeRequest");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OriginRequest).HasMaxLength(255);
            entity.Property(e => e.ProcessedAt).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Response).HasMaxLength(255);
            entity.Property(e => e.ResponseAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ApprovalUser).WithMany(p => p.ProductionChangeRequestApprovalUsers)
                .HasForeignKey(d => d.ApprovalUserId)
                .HasConstraintName("FK__Productio__Appro__63CEACD4");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Productio__Categ__51AFFC99");

            entity.HasOne(d => d.Day).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("FK__Productio__DayId__703483B9");

            entity.HasOne(d => d.Month).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.MonthId)
                .HasConstraintName("FK__Productio__Month__6F405F80");

            entity.HasOne(d => d.NcPart).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.NcPartId)
                .HasConstraintName("FK__Productio__NcPar__64C2D10D");

            entity.HasOne(d => d.RequestingUser).WithMany(p => p.ProductionChangeRequestRequestingUsers)
                .HasForeignKey(d => d.RequestingUserId)
                .HasConstraintName("FK__Productio__Reque__62DA889B");

            entity.HasOne(d => d.Task).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__Productio__TaskI__7128A7F2");

            entity.HasMany(d => d.Users).WithMany(p => p.Requests)
                .UsingEntity<Dictionary<string, object>>(
                    "OperatorChangeRequest",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OperatorC__UserI__548C6944"),
                    l => l.HasOne<ProductionChangeRequest>().WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OperatorC__Reque__5398450B"),
                    j =>
                    {
                        j.HasKey("RequestId", "UserId").HasName("PK__Operator__E2D0DDBEC1692191");
                        j.ToTable("OperatorChangeRequest");
                    });
        });

        modelBuilder.Entity<ProductionLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Producti__2EAE65291097646F");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.ProductionLines)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Productio__Compa__341F99B2");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionLines)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Productio__Creat__4B02FF0A");
        });

        modelBuilder.Entity<ProductionLineRecipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Producti__FDD988B0FB813079");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PreviousPartId).HasColumnName("previousPartId");
            entity.Property(e => e.RequiredQuantity).HasColumnType("decimal(10, 3)");

            entity.HasOne(d => d.Group).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Productio__Group__6A7BAA63");

            entity.HasOne(d => d.Line).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__Productio__LineI__6987862A");

            entity.HasOne(d => d.Machine).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__Productio__Machi__6C63F2D5");

            entity.HasOne(d => d.Part).WithMany(p => p.ProductionLineRecipeParts)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__Productio__PartI__6D58170E");

            entity.HasOne(d => d.PreviousPart).WithMany(p => p.ProductionLineRecipePreviousParts)
                .HasForeignKey(d => d.PreviousPartId)
                .HasConstraintName("FK__Productio__previ__6E4C3B47");

            entity.HasOne(d => d.Station).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__Productio__Stati__6B6FCE9C");
        });

        modelBuilder.Entity<ProductionLineRecipeMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Producti__C50610F79245AC12");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequiredQuantity).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.PreviousPart).WithMany(p => p.ProductionLineRecipeMaterials)
                .HasForeignKey(d => d.PreviousPartId)
                .HasConstraintName("FK__Productio__Previ__0347582D");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ProductionLineRecipeMaterials)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Productio__Recip__025333F4");
        });

        modelBuilder.Entity<ProductionRecord>(entity =>
        {
            entity.HasKey(e => e.ProductionId).HasName("PK__Producti__D5D9A2D5CA8C063E");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsCut).HasDefaultValue(1);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionRecords)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Productio__Creat__65B6F546");

            entity.HasOne(d => d.Task).WithMany(p => p.ProductionRecords)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__Productio__TaskI__3E9D2825");
        });

        modelBuilder.Entity<QualityClasification>(entity =>
        {
            entity.HasKey(e => e.ClasificationId).HasName("PK__QualityC__6E7506C0A97AAE21");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Quality).WithMany(p => p.QualityClasifications)
                .HasForeignKey(d => d.QualityId)
                .HasConstraintName("FK__QualityCl__Quali__47326E26");
        });

        modelBuilder.Entity<QualityRecord>(entity =>
        {
            entity.HasKey(e => e.QualityId).HasName("PK__QualityR__0B22BB4E3A98F586");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.NcPart).WithMany(p => p.QualityRecords)
                .HasForeignKey(d => d.NcPartId)
                .HasConstraintName("FK__QualityRe__NcPar__463E49ED");
        });

        modelBuilder.Entity<RequestCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__RequestC__19093A0B58D046E5");

            entity.ToTable("RequestCategory");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skills__DFA09187BCBBD1A8");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__E0D8A6BD4E067A21");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Stations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Stations__Create__4DDF6BB5");

            entity.HasOne(d => d.Group).WithMany(p => p.Stations)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Stations__GroupI__3607E224");

            entity.HasMany(d => d.Machines).WithMany(p => p.Stations)
                .UsingEntity<Dictionary<string, object>>(
                    "StationMachine",
                    r => r.HasOne<Machine>().WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StationMa__Machi__679F3DB8"),
                    l => l.HasOne<Station>().WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StationMa__Stati__689361F1"),
                    j =>
                    {
                        j.HasKey("StationId", "MachineId").HasName("PK__StationM__7496430E349A675D");
                        j.ToTable("StationMachine");
                    });
        });

        modelBuilder.Entity<Turn>(entity =>
        {
            entity.HasKey(e => e.TurnId).HasName("PK__Turns__F74C230385DA25E7");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Turns)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Turns__CreatedBy__4BF72343");
        });

        modelBuilder.Entity<TurnDetail>(entity =>
        {
            entity.HasKey(e => e.TurnDetailId).HasName("PK__TurnDeta__FF34B622E670FEF8");

            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PeriodType).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Turn).WithMany(p => p.TurnDetails)
                .HasForeignKey(d => d.TurnId)
                .HasConstraintName("FK__TurnDetai__TurnI__39D87308");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C80CE772D");

            entity.HasIndex(e => e.DocumentId, "UQ__Users__1ABEEF0E55F4D7EE").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105349125E23C").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F284569458BA0F").IsUnique();

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentId).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmployeeNumber)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.Rol).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Users__CompanyId__31432D07");
        });

        modelBuilder.Entity<UsersSkill>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SkillId }).HasName("PK__UsersSki__7A72C554425EF942");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SkillLevel).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Skill).WithMany(p => p.UsersSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersSkil__Skill__332B7579");

            entity.HasOne(d => d.User).WithMany(p => p.UsersSkills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersSkil__UserI__32375140");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
