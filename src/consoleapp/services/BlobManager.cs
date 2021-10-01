using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace consoleapp
{
    public class BlobManager
    {
        public static void UploadBlob()
        {
            var filePath = @"C:\Users\Jonas\Desktop\img\pipeline.png";
            var connectionString = Environment.GetEnvironmentVariable("mykey");

            BlobClient blobClient = new BlobClient(connectionString: connectionString, blobContainerName: "images", blobName: $"test-{Guid.NewGuid().ToString()}.png");

            blobClient.Upload(filePath);

            var blobUri = blobClient.Uri.AbsoluteUri;
            Console.WriteLine(blobUri);
        }
    }
}