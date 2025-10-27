using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{
    public class ProductionLineBasicInfo
    {
        public int RecipeId { get; set; }
        public int StepOrder { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int StationId { get; set; }
        public string StationName { get; set; } = string.Empty;
        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int PreviousPartId { get; set; }
        public string PreviousPartName { get; set; } = string.Empty;
        public decimal RequiredQuantity { get; set; }
        public Guid CreatedById { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ProductionLineWithMaterial
    {
        public int RecipeId { get; set; }
        public int StepOrder { get; set; }
        public int AssemblyStep { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int StationId { get; set; }
        public string StationName { get; set; } = string.Empty;
        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public List<ProductionLineMaterials> ProductionLineMaterials { get; set; } = [];
        public Guid CreatedById { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ProductionLineMaterials
    {
        public int MaterialId { get; set; }
        public decimal RequiredQuantity { get; set; }
        public int MaterialOrder { get; set; }
        public Part MaterialInfo { get; set; } = new();
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