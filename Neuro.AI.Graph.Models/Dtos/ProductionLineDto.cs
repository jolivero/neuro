namespace Neuro.AI.Graph.Models.Dtos
{
    public class ProductionLineDto
    {
        public string Name { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
    }

    public class ProductionLineConfigDto
    {
        public string LineId { get; set; } = string.Empty;
        public Steps Steps { get; set; } = new();
    }

    public class Steps
    {
        public string GroupId { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
        public string PartId { get; set; } = string.Empty;
        public string PrevPartId { get; set; } = string.Empty;
        public decimal RequiredQuantity { get; set; } = 0;
        public int StepOrder { get; set; }
    }

    public class ProductionLineHandleStepDto
    {
        public string RecipeId { get; set; } = string.Empty;
        public string LineId { get; set; } = string.Empty;
        public Steps Steps { get; set; } = new();
    }
}