using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{
    public class OperatorSelectList
    {
        public Guid UserId { get; set; }
        public string Operator { get; set; } = string.Empty;
    }

    public class OperatorProfile : User
    {
        public List<OperatorSkills> OperatorSkills { get; set; } = [];
    }

    public class OperatorSkills
    {
        public int? SkillId { get; set; }
        public string? Name { get; set; }
        public string? SkillLevel { get; set; }
    }
}