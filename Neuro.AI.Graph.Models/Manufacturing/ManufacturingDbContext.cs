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

    public virtual DbSet<ChangesRequest> ChangesRequests { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<DailySchedule> DailySchedules { get; set; }

    public virtual DbSet<DailyTask> DailyTasks { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<MachineReport> MachineReports { get; set; }

    public virtual DbSet<MonthlySchedule> MonthlySchedules { get; set; }

    public virtual DbSet<NonCompliantPartsRecord> NonCompliantPartsRecords { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<ProducedPartsRecord> ProducedPartsRecords { get; set; }

    public virtual DbSet<ProductionChangeRequest> ProductionChangeRequests { get; set; }

    public virtual DbSet<ProductionLine> ProductionLines { get; set; }

    public virtual DbSet<ProductionLineRecipe> ProductionLineRecipes { get; set; }

    public virtual DbSet<ProductionRecord> ProductionRecords { get; set; }

    public virtual DbSet<QualityClasification> QualityClasifications { get; set; }

    public virtual DbSet<QualityRecord> QualityRecords { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

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
            entity.HasKey(e => e.DetailId).HasName("PK__ChangeRe__135C316D16AAD0CF");

            entity.ToTable("ChangeRequestDetail");

            entity.Property(e => e.DetailId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RequestType).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Machine).WithMany(p => p.ChangeRequestDetails)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__ChangeReq__Machi__47127295");

            entity.HasOne(d => d.Part).WithMany(p => p.ChangeRequestDetails)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__ChangeReq__PartI__48FABB07");

            entity.HasOne(d => d.Request).WithMany(p => p.ChangeRequestDetails)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__ChangeReq__Reque__443605EA");

            entity.HasOne(d => d.Station).WithMany(p => p.ChangeRequestDetails)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__ChangeReq__Stati__480696CE");
        });

        modelBuilder.Entity<ChangesRequest>(entity =>
        {
            entity.HasKey(e => e.ChangesRequestId).HasName("PK__ChangesR__EB099F68094386A3");

            entity.ToTable("ChangesRequest");

            entity.Property(e => e.ChangesRequestId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971CAC2FEDFF9F");

            entity.Property(e => e.CompanyId).HasDefaultValueSql("(newid())");
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
                .HasConstraintName("FK__Companies__Creat__4DBF7024");
        });

        modelBuilder.Entity<DailySchedule>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("PK__DailySch__BF3DD8C52D2776A1");

            entity.ToTable("DailySchedule");

            entity.Property(e => e.DayId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DayType)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ProductionDate)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Month).WithMany(p => p.DailySchedules)
                .HasForeignKey(d => d.MonthId)
                .HasConstraintName("FK__DailySche__Month__2E46C4CB");
        });

        modelBuilder.Entity<DailyTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__DailyTas__7C6949B1D48DD733");

            entity.Property(e => e.TaskId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomeBeginAt).HasMaxLength(255);
            entity.Property(e => e.CustomeEndAt).HasMaxLength(255);
            entity.Property(e => e.MachineStatus).HasMaxLength(255);
            entity.Property(e => e.OperatorStatus).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Day).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("FK__DailyTask__DayId__2F3AE904");

            entity.HasOne(d => d.Machine).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__DailyTask__Machi__35E7E693");

            entity.HasOne(d => d.Station).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__DailyTask__Stati__34F3C25A");

            entity.HasOne(d => d.User).WithMany(p => p.DailyTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__DailyTask__UserI__36DC0ACC");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36A9082168F");

            entity.Property(e => e.GroupId).HasDefaultValueSql("(newid())");
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
                .HasConstraintName("FK__Groups__CreatedB__3E7D2C94");

            entity.HasOne(d => d.Line).WithMany(p => p.Groups)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__Groups__LineId__2799C73C");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6B3F1C18028");

            entity.ToTable("Inventory");

            entity.HasIndex(e => e.PartId, "UQ__Inventor__7C3F0D51C83247EB").IsUnique();

            entity.Property(e => e.InventoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.PartId)
                .HasConstraintName("FK__Inventory__PartI__3BA0BFE9");
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__Machines__44EE5B38FA2E3F44");

            entity.Property(e => e.MachineId).HasDefaultValueSql("(newid())");
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
                .HasConstraintName("FK__Machines__Create__4159993F");
        });

        modelBuilder.Entity<MachineReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__MachineR__D5BD4805CD3531A6");

            entity.Property(e => e.ReportId).HasDefaultValueSql("(newid())");
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
                .HasConstraintName("FK__MachineRe__Machi__2A7633E7");

            entity.HasOne(d => d.Operator).WithMany(p => p.MachineReportOperators)
                .HasForeignKey(d => d.OperatorId)
                .HasConstraintName("FK__MachineRe__Opera__39B87777");

            entity.HasOne(d => d.Station).WithMany(p => p.MachineReports)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__MachineRe__Stati__2B6A5820");

            entity.HasOne(d => d.Technical).WithMany(p => p.MachineReportTechnicals)
                .HasForeignKey(d => d.TechnicalId)
                .HasConstraintName("FK__MachineRe__Techn__3AAC9BB0");
        });

        modelBuilder.Entity<MonthlySchedule>(entity =>
        {
            entity.HasKey(e => e.MonthId).HasName("PK__MonthlyS__9FA83FA6639DF960");

            entity.ToTable("MonthlySchedule");

            entity.Property(e => e.MonthId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Line).WithMany(p => p.MonthlySchedules)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__MonthlySc__LineI__2D52A092");

            entity.HasOne(d => d.PlannedByNavigation).WithMany(p => p.MonthlySchedules)
                .HasForeignKey(d => d.PlannedBy)
                .HasConstraintName("FK__MonthlySc__Plann__424DBD78");
        });

        modelBuilder.Entity<NonCompliantPartsRecord>(entity =>
        {
            entity.HasKey(e => e.NcPartId).HasName("PK__NonCompl__A881183A651B2336");

            entity.Property(e => e.NcPartId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithMany(p => p.NonCompliantPartsRecords)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__NonCompli__PartI__33FF9E21");

            entity.HasOne(d => d.Production).WithMany(p => p.NonCompliantPartsRecords)
                .HasForeignKey(d => d.ProductionId)
                .HasConstraintName("FK__NonCompli__Produ__321755AF");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__Parts__7C3F0D507BA902FD");

            entity.Property(e => e.PartId).HasDefaultValueSql("(newid())");
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
                .HasConstraintName("FK__Parts__CreatedBy__40657506");

            entity.HasOne(d => d.Station).WithMany(p => p.Parts)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__Parts__StationId__29820FAE");
        });

        modelBuilder.Entity<ProducedPartsRecord>(entity =>
        {
            entity.HasKey(e => e.ProducedPartId).HasName("PK__Produced__06E2CB4B7027D216");

            entity.Property(e => e.ProducedPartId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Part).WithMany(p => p.ProducedPartsRecords)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__ProducedP__PartI__330B79E8");

            entity.HasOne(d => d.Production).WithMany(p => p.ProducedPartsRecords)
                .HasForeignKey(d => d.ProductionId)
                .HasConstraintName("FK__ProducedP__Produ__31233176");
        });

        modelBuilder.Entity<ProductionChangeRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Producti__33A8517A0808EA21");

            entity.ToTable("ProductionChangeRequest");

            entity.Property(e => e.RequestId).ValueGeneratedNever();
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
                .HasConstraintName("FK__Productio__Appro__4AE30379");

            entity.HasOne(d => d.ChangesRequest).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.ChangesRequestId)
                .HasConstraintName("FK__Productio__Chang__4341E1B1");

            entity.HasOne(d => d.NcPart).WithMany(p => p.ProductionChangeRequests)
                .HasForeignKey(d => d.NcPartId)
                .HasConstraintName("FK__Productio__NcPar__4BD727B2");

            entity.HasOne(d => d.RequestingUser).WithMany(p => p.ProductionChangeRequestRequestingUsers)
                .HasForeignKey(d => d.RequestingUserId)
                .HasConstraintName("FK__Productio__Reque__49EEDF40");

            entity.HasMany(d => d.Users).WithMany(p => p.Requests)
                .UsingEntity<Dictionary<string, object>>(
                    "OperatorChangeRequest",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OperatorC__UserI__461E4E5C"),
                    l => l.HasOne<ProductionChangeRequest>().WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OperatorC__Reque__452A2A23"),
                    j =>
                    {
                        j.HasKey("RequestId", "UserId").HasName("PK__Operator__E2D0DDBE6D291605");
                        j.ToTable("OperatorChangeRequest");
                    });
        });

        modelBuilder.Entity<ProductionLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Producti__2EAE6529DF7A867C");

            entity.Property(e => e.LineId).HasDefaultValueSql("(newid())");
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
                .HasConstraintName("FK__Productio__Compa__26A5A303");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionLines)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Productio__Creat__3C94E422");
        });

        modelBuilder.Entity<ProductionLineRecipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Producti__FDD988B0620C6831");

            entity.Property(e => e.RecipeId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.RequiredQuantity).HasColumnType("decimal(10, 3)");

            entity.HasOne(d => d.Group).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Productio__Group__5748DA5E");

            entity.HasOne(d => d.Line).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__Productio__LineI__5654B625");

            entity.HasOne(d => d.Machine).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__Productio__Machi__593122D0");

            entity.HasOne(d => d.Part).WithMany(p => p.ProductionLineRecipeParts)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__Productio__PartI__5A254709");

            entity.HasOne(d => d.PreviousPart).WithMany(p => p.ProductionLineRecipePreviousParts)
                .HasForeignKey(d => d.PreviousPartId)
                .HasConstraintName("FK__Productio__previ__5B196B42");

            entity.HasOne(d => d.Station).WithMany(p => p.ProductionLineRecipes)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK__Productio__Stati__583CFE97");
        });

        modelBuilder.Entity<ProductionRecord>(entity =>
        {
            entity.HasKey(e => e.ProductionId).HasName("PK__Producti__D5D9A2D58D6FD574");

            entity.Property(e => e.ProductionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BeginTime).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Duration).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EndTime).HasMaxLength(255);
            entity.Property(e => e.IsCut).HasDefaultValue(1);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionRecords)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Productio__Creat__4CCB4BEB");

            entity.HasOne(d => d.Task).WithMany(p => p.ProductionRecords)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__Productio__TaskI__302F0D3D");
        });

        modelBuilder.Entity<QualityClasification>(entity =>
        {
            entity.HasKey(e => e.ClasificationId).HasName("PK__QualityC__6E7506C00259C344");

            entity.Property(e => e.ClasificationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Quality).WithMany(p => p.QualityClasifications)
                .HasForeignKey(d => d.QualityId)
                .HasConstraintName("FK__QualityCl__Quali__38C4533E");
        });

        modelBuilder.Entity<QualityRecord>(entity =>
        {
            entity.HasKey(e => e.QualityId).HasName("PK__QualityR__0B22BB4E35B804A9");

            entity.Property(e => e.QualityId).HasDefaultValueSql("(newid())");
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
                .HasConstraintName("FK__QualityRe__NcPar__37D02F05");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302F1BB73CBFE");

            entity.Property(e => e.RolId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skills__DFA09187F0FD6860");

            entity.Property(e => e.SkillId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__E0D8A6BD33F6A050");

            entity.Property(e => e.StationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Stations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Stations__Create__3F7150CD");

            entity.HasOne(d => d.Group).WithMany(p => p.Stations)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Stations__GroupI__288DEB75");

            entity.HasMany(d => d.Machines).WithMany(p => p.Stations)
                .UsingEntity<Dictionary<string, object>>(
                    "StationMachine",
                    r => r.HasOne<Machine>().WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StationMa__Machi__4EB3945D"),
                    l => l.HasOne<Station>().WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StationMa__Stati__4FA7B896"),
                    j =>
                    {
                        j.HasKey("StationId", "MachineId").HasName("PK__StationM__7496430EFB11556D");
                        j.ToTable("StationMachine");
                    });
        });

        modelBuilder.Entity<Turn>(entity =>
        {
            entity.HasKey(e => e.TurnId).HasName("PK__Turns__F74C2303D5D592D9");

            entity.Property(e => e.TurnId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Duration).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PauseTime).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductiveTime).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Turns)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Turns__CreatedBy__3D89085B");
        });

        modelBuilder.Entity<TurnDetail>(entity =>
        {
            entity.HasKey(e => e.TurnDetailId).HasName("PK__TurnDeta__FF34B6229F7FA8A3");

            entity.Property(e => e.TurnDetailId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Available).HasDefaultValue(1);
            entity.Property(e => e.BeginAt).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndAt).HasMaxLength(255);
            entity.Property(e => e.PeriodType).HasMaxLength(255);
            entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Turn).WithMany(p => p.TurnDetails)
                .HasForeignKey(d => d.TurnId)
                .HasConstraintName("FK__TurnDetai__TurnI__2C5E7C59");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD6457BD2");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534EC2E7E89").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F2845634A5B1CF").IsUnique();

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
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
            entity.Property(e => e.Rol)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Users__CompanyId__22D5121F");
        });

        modelBuilder.Entity<UsersSkill>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SkillId }).HasName("PK__UsersSki__7A72C5542FA50E7F");

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
                .HasConstraintName("FK__UsersSkil__Skill__25B17ECA");

            entity.HasOne(d => d.User).WithMany(p => p.UsersSkills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersSkil__UserI__24BD5A91");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
