namespace Neuro.AI.Graph.Models.Dtos
{
    public class MachineDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal? EnergyConsumption { get; set; }
        public int? MaintenancePeriod { get; set; }
        public int? Velocity { get; set; }
        public int? MinOperator { get; set; }
        public int? MaxOperator { get; set; }
        public int? HoursPerCut { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? StationId { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class MachineReportDto
    {
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string StationId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
        public string OperatorId { get; set; } = string.Empty;
        public string? TechnicalId { get; set; }
    }
}
