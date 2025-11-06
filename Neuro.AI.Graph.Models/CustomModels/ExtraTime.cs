namespace Neuro.AI.Graph.Models.CustomModels
{
    public class ExtraTimeResponse
    {
        public Guid UserId { get; set; }
        public string Operator { get; set; } = string.Empty;
        public TimeOnly ExtraTime { get; set; }
    }
}