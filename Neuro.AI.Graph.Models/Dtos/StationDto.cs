using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.Dtos
{
    public class StationConfigInfo
    {
        public string LineName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public Machine Machine { get; set; } = new();
        public Part Part { get; set; } = new();
        public List<Part> PreviousPart { get; set; } = [];
    }
}