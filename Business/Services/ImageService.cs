using PicturePilot.Data;
using PicturePilot.Data.Entities;
using Azure.Storage.Blobs;

namespace PicturePilot.Services
{
    public class ImageService( IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<string> Upload(IFormFile file)
        {
            var connectionString = _configuration.GetValue<string>("Azure:BlobStorage:ConnectionString");

            BlobServiceClient blobServiceClient = new(connectionString);

            string containerName = "images";
            BlobContainerClient containerClient;

            if (blobServiceClient.GetBlobContainers().Any(x => x.Name == containerName))
            {
                containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            }
            else
            {
                containerClient = blobServiceClient.CreateBlobContainer(containerName);
            }
            var blobFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var blobClient = containerClient.GetBlobClient(blobFileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            return blobClient.Uri.ToString();
        }
    }
}