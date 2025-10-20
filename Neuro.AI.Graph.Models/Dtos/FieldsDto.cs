namespace Neuro.AI.Graph.Models.Dtos
{
    public class Field
    {
        public string Name { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
    }

    public class GroupDto : Field { }

    public class StationDto : Field { }

    public class PartDto : Field
    {
        public string Code { get; set; } = string.Empty;
        public int IsPreviousPart { get; set; }
        public InventoryDto? Inventory { get; set; } = new();
    }

    public class InventoryDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
