namespace Neuro.AI.Graph.Models.Dtos
{
    public class LogDto
    {
        public int? UserId { get; set; }
        public Guid UserIdRef { get; set; }
        public string Area { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public string? Desc0 { get; set; }
        public string? Desc1 { get; set; }
        public string? Desc2 { get; set; }
        public string? Desc3 { get; set; }
    }
}