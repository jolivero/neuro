namespace Neuro.AI.Graph.Models.Dtos
{
    public class StationConfigInfo
    {
        public string LineName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public MachineDto Machine { get; set; } = new();
        public PartDto Part { get; set; } = new();

    }
}