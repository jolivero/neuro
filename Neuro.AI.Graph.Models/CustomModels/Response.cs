namespace Neuro.AI.Graph.Models.CustomModels
{
    public class OptionsResponse
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }

    public class MessageResponse
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class ChangePlanningRequestMessageResponse : MessageResponse
    {
        public int RequestId { get; set; }
    }
}