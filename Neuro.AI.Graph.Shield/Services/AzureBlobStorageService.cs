using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Neuro.AI.Graph
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _config;

        public AzureBlobStorageService(BlobServiceClient blobServiceClient, IConfiguration config)
        {
            _blobServiceClient = blobServiceClient;
            _config = config;
        }

        public async Task<string> UploadFile(Stream fileStream, string fileName, string? contentType)
        {
            
            var containerClient = _blobServiceClient.GetBlobContainerClient(_config["AzureConnections:AzureContainerName"]);
            await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            });

            return blobClient.Uri.ToString();
        }
    }
}