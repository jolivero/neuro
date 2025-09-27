namespace Neuro.AI.Graph.QL.Mutations
{
    public class Mutations
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService; 

        public Mutations(IAzureBlobStorageService azureBlobStorageService)
        {
            _azureBlobStorageService = azureBlobStorageService;
        }
        
        public ManufacturingMutations ManufacturingMutations => new(_azureBlobStorageService);
    }
}
