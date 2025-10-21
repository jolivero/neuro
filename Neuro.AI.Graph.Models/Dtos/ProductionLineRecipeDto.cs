namespace Neuro.AI.Graph.Models.Dtos
{
    public class RecipeStepOrderDto
    {
        public int RecipeId { get; set; }
        public int StepOrder { get; set; }
    }

    public class MaterialStepOrderDto
    {
        public int MaterialId { get; set; }
        public int MaterialOrder { get; set; }
    }
}