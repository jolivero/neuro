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
            entity.HasKey(e => e.DetailId).HasName("PK__ChangeRe__135C316D798F104E");

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
                .HasConstraintName("FK__ChangeReq__Curre__1A94D1D9");

            entity.HasOne(d => d.CurrentLine).WithMany(p => p.ChangeRequestDetailCurrentLines)
                .HasForeignKey(d => d.CurrentLineId)
                .HasConstraintName("FK__ChangeReq__Curre__18AC8967");

            entity.HasOne(d => d.CurrentMachine).WithMany(p => p.ChangeRequestDetailCurrentMachines)
                .HasForeignKey(d => d.CurrentMachineId)
                .HasConstraintName("FK__ChangeReq__Curre__1E6562BD");

            entity.HasOne(d => d.CurrentPart).WithMany(p => p.ChangeRequestDetailCurrentParts)
                .HasForeignKey(d => d.CurrentPartId)
                .HasConstraintName("FK__ChangeReq__Curre__2235F3A1");

            entity.HasOne(d => d.CurrentStation).WithMany(p => p.ChangeRequestDetailCurrentStations)
                .HasForeignKey(d => d.CurrentStationId)
                .HasConstraintName("FK__ChangeReq__Curre__1C7D1A4B");

            entity.HasOne(d => d.CurrentTurn).WithMany(p => p.ChangeRequestDetailCurrentTurns)
                .HasForeignKey(d => d.CurrentTurnId)
                .HasConstraintName("FK__ChangeReq__Curre__204DAB2F");

            entity.HasOne(d => d.CurrentUser).WithMany(p => p.ChangeRequestDetailCurrentUsers)
                .HasForeignKey(d => d.CurrentUserId)
                .HasConstraintName("FK__ChangeReq__Curre__16C440F5");

            entity.HasOne(d => d.NewGroup).WithMany(p => p.ChangeRequestDetailNewGroups)
                .HasForeignKey(d => d.NewGroupId)
                .HasConstraintName("FK__ChangeReq__NewGr__1B88F612");

            entity.HasOne(d => d.NewLine).WithMany(p => p.ChangeRequestDetailNewLines)
                .HasForeignKey(d => d.NewLineId)
                .HasConstraintName("FK__ChangeReq__NewLi__19A0ADA0");

            entity.HasOne(d => d.NewMachine).WithMany(p => p.ChangeRequestDetailNewMachines)
                .HasForeignKey(d => d.NewMachineId)
                .HasConstraintName("FK__ChangeReq__NewMa__1F5986F6");

            entity.HasOne(d => d.NewPart).WithMany(p => p.ChangeRequestDetailNewParts)
                .HasForeignKey(d => d.NewPartId)
                .HasConstraintName("FK__ChangeReq__NewPa__232A17DA");

            entity.HasOne(d => d.NewStation).WithMany(p => p.ChangeRequestDetailNewStations)
                .HasForeignKey(d => d.NewStationId)
                .HasConstraintName("FK__ChangeReq__NewSt__1D713E84");

            entity.HasOne(d => d.NewTurn).WithMany(p => p.ChangeRequestDetailNewTurns)
                .HasForeignKey(d => d.NewTurnId)
                .HasConstraintName("FK__ChangeReq__NewTu__2141CF68");

            entity.HasOne(d => d.NewUser).WithMany(p => p.ChangeRequestDetailNewUsers)
                .HasForeignKey(d => d.NewUserId)
                .HasConstraintName("FK__ChangeReq__NewUs__17B8652E");

            entity.HasOne(d => d.Request).WithMany(p => p.ChangeRequestDetails)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__ChangeReq__Reque__13E7D44A");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971CAC81B20DB1");

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
                .HasConstraintName("FK__Companies__Creat__27EECCF7");
        });

        modelBuilder.Entity<DailyPlanning>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("PK__DailyPla__BF3DD8C55B67F1FC");

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
                .HasConstraintName("FK__DailyPlan__Month__7DF8932B");
        });

        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__DailyTas__7C6949B1B4709513");

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
                .HasConstraintName("FK__DailyTask__DayId__7EECB764");

            entity.HasOne(d => d.Machine).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__DailyTask__Machi__0599B4F3");

            entity.HasOne(d => d.Station).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__DailyTask__Stati__04A590BA");

            entity.HasOne(d => d.Turn).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.TurnId)
                .HasConstraintName("FK__DailyTask__TurnI__7D046EF2");

            entity.HasOne(d => d.User).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DailyTask__UserI__068DD92C");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36AA9D9AEE0");

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
                .HasConstraintName("FK__Groups__CreatedB__0E2EFAF4");

            entity.HasOne(d => d.Line).WithMany(p => p.Groups)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__Groups__LineId__76577163");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6B3E7216C22");

            entity.ToTable("Inventory");

            entity.HasIndex(e => e.PartId, "UQ__Inventor__7C3F0D510CC3A748").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.PartId)
                .HasConstraintName("FK__Inventory__PartI__0B528E49");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__logs__5E548648FAEF1F86");

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
            entity.Property(e => e.Payload).HasMaxLength(255);
            entity.Property(e => e.Rol).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__Machines__44EE5B384F455286");

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
                .HasConstraintName("FK__Machines__Create__110B679F");
        });

        modelBuilder.Entity<MachineReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__MachineR__D5BD480510C7D1E5");

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
                .HasConstraintName("FK__MachineRe__Machi__7933DE0E");

            entity.HasOne(d => d.Operator).WithMany(p => p.MachineReportOperators)
                .HasForeignKey(d => d.OperatorId)
                .HasConstraintName("FK__MachineRe__Opera__096A45D7");

            entity.HasOne(d => d.Station).WithMany(p => p.MachineReports)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__MachineRe__Stati__7A280247");

            entity.HasOne(d => d.Technical).WithMany(p => p.MachineReportTechnicals)
                .HasForeignKey(d => d.TechnicalId)
                .HasConstraintName("FK__MachineRe__Techn__0A5E6A10");
        });

        modelBuilder.Entity<MonthlyPlanning>(entity =>
        {
            entity.HasKey(e => e.MonthId).HasName("PK__MonthlyP__9FA83FA620AACC92");

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
                .HasConstraintName("FK__MonthlyPl__LineI__7C104AB9");

            entity.HasOne(d => d.PlannedByNavigation).WithMany(p => p.MonthlyPlannings)
                .HasForeignKey(d => d.PlannedBy)
                .HasConstraintName("FK__MonthlyPl__Plann__11FF8BD8");
        });

        modelBuilder.Entity<NonCompliantPartsRecord>(entity =>
        {
            entity.HasKey(e => e.NcPartId).HasName("PK__NonCompl__A881183AC0527CCA");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithMany(p => p.NonCompliantPartsRecords)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__NonCompli__PartI__03B16C81");

            entity.HasOne(d => d.Task).WithMany(p => p.NonCompliantPartsRecords)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__NonCompli__TaskI__01C9240F");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__Parts__7C3F0D50BD9E3B5E");

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
                .HasConstraintName("FK__Parts__CreatedBy__10174366");

            entity.HasOne(d => d.Station).WithMany(p => p.Parts)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__Parts__StationId__783FB9D5");
        });

        modelBuilder.Entity<ProducedPartsRecord>(entity =>
        {
            entity.HasKey(e => e.ProducedPartId).HasName("PK__Produced__06E2CB4BD559CD48");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithMany(p => p.ProducedPartsRecords)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__ProducedP__PartI__02BD4848");

            entity.HasOne(d => d.Task).WithMany(p => p.ProducedPartsRecords)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__ProducedP__TaskI__00D4FFD6");
        });

        modelBuilder.Entity<ProductionChangeRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Producti__33A8517A0DE6E7B3");

            entity.ToTable("ProductionChangeRequest");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OriginRequest).HasMaxLength(255);
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
                .HasConstraintName("FK__Productio__Appro__2512604C");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Productio__Categ__12F3B011");

            entity.HasOne(d => d.Day).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("FK__Productio__DayId__31783731");

            entity.HasOne(d => d.Month).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.MonthId)
                .HasConstraintName("FK__Productio__Month__308412F8");

            entity.HasOne(d => d.NcPart).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.NcPartId)
                .HasConstraintName("FK__Productio__NcPar__26068485");

            entity.HasOne(d => d.RequestingUser).WithMany(p => p.ProductionChangeRequestRequestingUsers)
                .HasForeignKey(d => d.RequestingUserId)
                .HasConstraintName("FK__Productio__Reque__241E3C13");

            entity.HasOne(d => d.Task).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__Productio__TaskI__326C5B6A");

            entity.HasMany(d => d.Users).WithMany(p => p.Requests)
                .UsingEntity<Dictionary<string, object>>(
                    "OperatorChangeRequest",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OperatorC__UserI__15D01CBC"),
                    l => l.HasOne<ProductionChangeRequest>().WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OperatorC__Reque__14DBF883"),
                    j =>
                    {
                        j.HasKey("RequestId", "UserId").HasName("PK__Operator__E2D0DDBEE0FAB925");
                        j.ToTable("OperatorChangeRequest");
                    });
        });

        modelBuilder.Entity<ProductionLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Producti__2EAE6529E6EB5CDC");

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
                .HasConstraintName("FK__Productio__Compa__75634D2A");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionLines)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Productio__Creat__0C46B282");
        });

        modelBuilder.Entity<ProductionLineRecipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Producti__FDD988B01265E08B");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PreviousPartId).HasColumnName("previousPartId");
            entity.Property(e => e.RequiredQuantity).HasColumnType("decimal(10, 3)");

            entity.HasOne(d => d.Group).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Productio__Group__2BBF5DDB");

            entity.HasOne(d => d.Line).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__Productio__LineI__2ACB39A2");

            entity.HasOne(d => d.Machine).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__Productio__Machi__2DA7A64D");

            entity.HasOne(d => d.Part).WithMany(p => p.ProductionLineRecipeParts)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__Productio__PartI__2E9BCA86");

            entity.HasOne(d => d.PreviousPart).WithMany(p => p.ProductionLineRecipePreviousParts)
                .HasForeignKey(d => d.PreviousPartId)
                .HasConstraintName("FK__Productio__previ__2F8FEEBF");

            entity.HasOne(d => d.Station).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__Productio__Stati__2CB38214");
        });

        modelBuilder.Entity<ProductionRecord>(entity =>
        {
            entity.HasKey(e => e.ProductionId).HasName("PK__Producti__D5D9A2D55565E065");

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
                .HasConstraintName("FK__Productio__Creat__26FAA8BE");

            entity.HasOne(d => d.Task).WithMany(p => p.ProductionRecords)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__Productio__TaskI__7FE0DB9D");
        });

        modelBuilder.Entity<QualityClasification>(entity =>
        {
            entity.HasKey(e => e.ClasificationId).HasName("PK__QualityC__6E7506C042699967");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Quality).WithMany(p => p.QualityClasifications)
                .HasForeignKey(d => d.QualityId)
                .HasConstraintName("FK__QualityCl__Quali__0876219E");
        });

        modelBuilder.Entity<QualityRecord>(entity =>
        {
            entity.HasKey(e => e.QualityId).HasName("PK__QualityR__0B22BB4E410B3A1B");

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
                .HasConstraintName("FK__QualityRe__NcPar__0781FD65");
        });

        modelBuilder.Entity<RequestCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__RequestC__19093A0B50742FB1");

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
            entity.HasKey(e => e.SkillId).HasName("PK__Skills__DFA09187550FF2A5");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__E0D8A6BD6706820B");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Stations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Stations__Create__0F231F2D");

            entity.HasOne(d => d.Group).WithMany(p => p.Stations)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Stations__GroupI__774B959C");

            entity.HasMany(d => d.Machines).WithMany(p => p.Stations)
                .UsingEntity<Dictionary<string, object>>(
                    "StationMachine",
                    r => r.HasOne<Machine>().WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StationMa__Machi__28E2F130"),
                    l => l.HasOne<Station>().WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StationMa__Stati__29D71569"),
                    j =>
                    {
                        j.HasKey("StationId", "MachineId").HasName("PK__StationM__7496430EFF491DAE");
                        j.ToTable("StationMachine");
                    });
        });

        modelBuilder.Entity<Turn>(entity =>
        {
            entity.HasKey(e => e.TurnId).HasName("PK__Turns__F74C23031981CA66");

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
                .HasConstraintName("FK__Turns__CreatedBy__0D3AD6BB");
        });

        modelBuilder.Entity<TurnDetail>(entity =>
        {
            entity.HasKey(e => e.TurnDetailId).HasName("PK__TurnDeta__FF34B62277816A3B");

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
                .HasConstraintName("FK__TurnDetai__TurnI__7B1C2680");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CF1DC8FAF");

            entity.HasIndex(e => e.DocumentId, "UQ__Users__1ABEEF0E54925828").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343021230F").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F284568427C344").IsUnique();

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
                .HasConstraintName("FK__Users__CompanyId__7286E07F");
        });

        modelBuilder.Entity<UsersSkill>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SkillId }).HasName("PK__UsersSki__7A72C554CB7BBFB2");

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
                .HasConstraintName("FK__UsersSkil__Skill__746F28F1");

            entity.HasOne(d => d.User).WithMany(p => p.UsersSkills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersSkil__UserI__737B04B8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
