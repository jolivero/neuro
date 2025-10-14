namespace Neuro.AI.Graph.Models.Dtos
{
    public class ProductionLineDto
    {
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class ProductionLineConfigDto
    {
        public int LineId { get; set; }
        public Steps Steps { get; set; } = new();
    }

    public class Steps
    {
        public int GroupId { get; set; }
        public int StationId { get; set; }
        public int MachineId { get; set; }
        public int PartId { get; set; }
        public int PrevPartId { get; set; }
        public decimal RequiredQuantity { get; set; } = 0;
        public int StepOrder { get; set; }
    }

    public class ProductionLineHandleStepDto
    {
        public int RecipeId { get; set; }
        public int LineId { get; set; }
        public Steps Steps { get; set; } = new();
    }
}