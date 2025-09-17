using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{
    public class MonthlyPlanningProductionLines : MonthlyPlanning
    {
        public List<AssignedProductionLines> AssignedProductionLines { get; set; } = [];
    }

    public class AssignedProductionLines
    {
        public Guid LineId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    
}