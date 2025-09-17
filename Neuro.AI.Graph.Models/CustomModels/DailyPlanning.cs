namespace Neuro.AI.Graph.Models.CustomModels
{
    public class DailyPlanningSummary
    {
        public Guid MonthId { get; set; }
        public int MonthlyGoal { get; set; }
        public int CurrentGoal { get; set; }
        public int ProductiveDays { get; set; }
        public int DailyGoal { get; set; }
    }
}