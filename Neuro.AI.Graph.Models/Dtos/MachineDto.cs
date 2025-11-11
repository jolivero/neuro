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
        public int? StationId { get; set; }
        public Guid? CreatedBy { get; set; }
    }

    public class MachineReportDto
    {
        public string? Type { get; set; }
        public string? Desc0 { get; set; }
        public string? Desc1 { get; set; }
        public string? Status { get; set; }
        public int StationId { get; set; }
        public int MachineId { get; set; }
        public Guid OperatorId { get; set; }
        public Guid? TechnicalId { get; set; }
    }
}
