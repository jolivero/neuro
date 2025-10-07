namespace Neuro.AI.Graph.Models.CustomModels
{
    public class ProductionLineSummary
    {
        public Guid LineId { get; set; }
        public string ProductionLine { get; set; } = string.Empty;
        public Guid MonthId { get; set; }
        public int CurrentMonth { get; set; }
        public int PreviousMonth { get; set; }
        public int Year { get; set; }
        public int CurrentGoal { get; set; }
        public int PreviousGoal { get; set; }
        public int CurrentTotal { get; set; }
        public int PreviousTotal { get; set; }
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