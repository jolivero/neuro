namespace Neuro.AI.Graph.Models.Dtos
{
    public class CompanyDto
    {
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyRuc { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyPhone { get; set; } = string.Empty;
        public string? CompanyWeb { get; set; }
        public string? CompanyLogo { get; set; }
        public string? CompanyColors { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public int? CreatedBy { get; set; }
    }
}
