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
        public List<Steps> Steps { get; set; } = [];
    }

    public class Steps
    {
        public string GroupId { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
        public string? PrevMachineId { get; set; } = null;
        public string PartId { get; set; } = string.Empty;
        public string PrevPartId { get; set; } = string.Empty;
        public decimal RequiredQuantity { get; set; } = 0;
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