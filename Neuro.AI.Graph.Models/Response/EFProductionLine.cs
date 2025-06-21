using Neuro.AI.Graph.Models.Dtos;

namespace Neuro.AI.Graph.Models.Response
{
    
    public class EFProductionLineList : ProductionLineDto
    {
        public string LineId { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}