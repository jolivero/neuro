namespace Neuro.AI.Graph.Models.Dtos
{
    public class UsersDto
    {
        public int? UserId { get; set; }
        public Guid UserIdRef { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocumentId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string Rol { get; set; } = string.Empty;
    }
    public class SkillsDto
    {
        public int SkillId { get; set; }
        public string Level { get; set; } = string.Empty;
    }

    public class UserSkillsDto
    {
        public int UserId { get; set; }
        public List<SkillsDto> Skills { get; set; } = [];
    }

}