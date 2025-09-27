namespace Neuro.AI.Graph
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadFile(Stream fileStream, string fileName);
    }
}