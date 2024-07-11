using Azure.Storage.Blobs;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace PicturePilot.Business.Services;

public class ImageService(IConfiguration configuration, ComputerVisionClient computerVisionClient)
{
    private readonly IConfiguration _configuration = configuration;
    private ComputerVisionClient _computerVisionClient = computerVisionClient;

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


    public async Task<ImageAnalysis> AnalyzeImageAsync(string imageUrl)
    {
        _computerVisionClient = Authenticate(_configuration.GetValue<string>("Azure:CognitiveServices:Key"), _configuration.GetValue<string>("Azure:CognitiveServices:Endpoint"));
        var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Description, VisualFeatureTypes.Adult, VisualFeatureTypes.Faces, VisualFeatureTypes.Categories, VisualFeatureTypes.Tags, VisualFeatureTypes.Objects };
        var analysisResult = await _computerVisionClient.AnalyzeImageAsync(imageUrl, features);
        return analysisResult;
    }

    public static ComputerVisionClient Authenticate(string key, string endpoint)
    {
        ComputerVisionClient visionClient = new ComputerVisionClient(
        new ApiKeyServiceClientCredentials(key))
        { Endpoint = endpoint };
        return visionClient;
    }
}