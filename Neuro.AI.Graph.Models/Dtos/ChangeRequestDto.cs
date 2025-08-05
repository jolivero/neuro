namespace Neuro.AI.Graph.Models.Dtos
{
    public class MonthlyChangeRequestDto
    {
        public string MonthId { get; set; } = string.Empty;
        public string RequestingUserId { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public int CurrentValue { get; set; }
        public int NewValue { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

    public class UpdateStatusRequestDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string ApprovalUserId { get; set; } = string.Empty;
        public string? Response { get; set; }
        public bool Status { get; set; }
    }
}