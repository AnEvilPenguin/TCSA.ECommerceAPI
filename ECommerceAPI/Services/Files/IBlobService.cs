namespace ECommerceAPI.Services.Files;

public interface IBlobService
{
    public Task UploadFile(string fileName, Stream report);
}