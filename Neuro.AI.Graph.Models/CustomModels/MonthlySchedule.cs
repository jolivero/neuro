using Neuro.AI.Graph.Models.CustomModels;

namespace Neuro.AI.Graph.Models.Dtos
{
    public class OperatorMonthlySchedule
    {
        public Guid OperatorId { get; set; }
        public string OperatorName { get; set; } = string.Empty;
        public List<AssignedDays> Days { get; set; } = [];
    }

    public class AssignedDays
    {
        public string DayId { get; set; } = string.Empty;
        public string ProductiveDate { get; set; } = string.Empty;
    }
}