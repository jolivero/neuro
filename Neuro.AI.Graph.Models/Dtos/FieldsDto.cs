namespace Neuro.AI.Graph.Models.Dtos
{
    public class Field
    {
        public string Name { get; set; } = string.Empty;
        public int? CreatedBy { get; set; }
    }

    public class GroupDto : Field
    {
        public int? LineId { get; set; }
    }

    public class StationDto : Field
    {
        public int? GroupId { get; set; }
    }

    public class PartDto : Field
    {
        public string Code { get; set; } = string.Empty;
        public int IsPreviousPart { get; set; }
        public InventoryDto Inventory { get; set; } = new();
        public int? StationId { get; set; }
    }

    public class InventoryDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
    }
}
