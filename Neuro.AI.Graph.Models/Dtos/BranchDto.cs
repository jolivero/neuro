namespace Neuro.AI.Graph.Models.Dtos
{
    public class BranchDto
    {
        public int? BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string BranchAddress { get; set; } = string.Empty;
        public int CompanyId { get; set; }
    }
}