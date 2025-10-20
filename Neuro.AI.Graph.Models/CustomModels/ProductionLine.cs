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

    public class ProductionLineMachineHoursPerCut
    {
        public string GroupName { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public int HoursPerCut { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class ProductionLineDetail
    {
        public int LineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductionLineGroups> Groups { get; set; } = [];
    }

    public class ProductionLineGroups
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProductionLineStation Station { get; set; } = new();
    }

    public class ProductionLineStation
    {
        public int StationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProductionLineMachine Machine { get; set; } = new();
        public ProductionLinePart Part { get; set; } = new();
        public List<ProductionLinePreviousPart> PreviousParts { get; set; } = [];
    }

    public class ProductionLineMachine
    {
        public int MachineId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductionLinePart
    {
        public int PartId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class ProductionLinePreviousPart
    {
        public int PreviousPartId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public ProductionLinePreviousPartInventory Inventory { get; set; } = new();
    }

    public class ProductionLinePreviousPartInventory
    {
        public int InventoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}