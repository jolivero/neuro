namespace Neuro.AI.Graph.Models.Dtos
{
    public class Field
    {
        public string Name { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
    }

    public class GroupDto : Field
    {
        public string? LineId { get; set; }
    }

    public class StationDto : Field
    {
        public string? GroupId { get; set; }
    }

    public class PartDto : Field
    {
        public string Code { get; set; } = string.Empty;
        public InventoryDto Inventory { get; set; } = new();
        public string? StationId { get; set; }
    }

    public class InventoryDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
    }
}
