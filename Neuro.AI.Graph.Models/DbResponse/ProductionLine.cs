namespace Neuro.AI.Graph.Models.DbResponse
{
    public class ProductionLineBasicInfo
    {
        public Guid RecipeId { get; set; }
        public Guid LineId { get; set; }
        public string LineName { get; set; } = string.Empty;
        public Guid GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public Guid StationId { get; set; }
        public string StationName { get; set; } = string.Empty;
        public Guid MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public Guid PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public Guid PreviousPartId { get; set; }
        public string PreviousPartName { get; set; } = string.Empty;
        public decimal RequiredQuantity { get; set; }
        public Guid CreatedById { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ProductionLineMachineHoursPerCut
    {
        public string GroupName { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public int HoursPerCut { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}