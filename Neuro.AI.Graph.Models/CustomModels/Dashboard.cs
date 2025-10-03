namespace Neuro.AI.Graph.Models.CustomModels
{
    public class ProductionLineSummary
    {
        public Guid LineId { get; set; }
        public string ProductionLine { get; set; } = string.Empty;
        public Guid MonthId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int MonthlyGoal { get; set; }
        public int Total { get; set; }
    }

    public class ProductionLineOperatorSummary
    {
        public Guid OperatorId { get; set; }
        public string Operator { get; set; } = string.Empty;
        public DateOnly ProductionDate { get; set; }
        public int DailyGoal { get; set; }
        public int Total { get; set; }
        public decimal Progress { get; set; }
    }
}