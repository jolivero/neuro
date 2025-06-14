namespace Neuro.AI.Graph.Models.Dtos
{
    public class ProductionLineDto
    {
        public string Name { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
    }
}