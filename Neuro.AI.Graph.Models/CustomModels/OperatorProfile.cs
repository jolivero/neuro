using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{
    public class OperatorProfile : User
    {
        public List<OperatorSkills> OperatorSkills { get; set; } = [];
    }

    public class OperatorSkills
    {
        public Guid? SkillId { get; set; }
        public string? Name { get; set; }
        public string? SkillLevel { get; set; }
    }
}