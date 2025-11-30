using Azure.Storage.Blobs;

namespace ECommerceAPI.Services.Files;

public sealed class BlobService(string? connectionString) : IBlobService
{
    private readonly BlobServiceClient _client = new(connectionString);

    public async Task UploadFile(string fileName, Stream report)
    {
        var blobClient = _client.GetBlobContainerClient("epiecommerce");
        await blobClient.CreateIfNotExistsAsync();
        
        await blobClient.UploadBlobAsync(fileName, report, CancellationToken.None);
    }
}