namespace Neuro.AI.Graph.Models.Dtos
{
    public class UserIpcDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocumentId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;
    }
    public class SkillsDto
    {
        public string SkillId { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
    }

    public class UserSkillsDto
    {
        public string UserId { get; set; } = string.Empty;
        public List<SkillsDto> Skills { get; set; } = [];
    }

}