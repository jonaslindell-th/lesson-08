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
            var filePath = @"C:\Users\Jonas\Desktop\img\package.png";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=lesson08;AccountKey=XxMdJ+CD+nBMgcwlEolv0zCJ9xBLc4mNPz0/HQDxWdgNi5Oo3bMUwt5o411CCsvp38EFQfp78BCcyARpIylluQ==;EndpointSuffix=core.windows.net";

            BlobClient blobClient = new BlobClient(connectionString: connectionString, blobContainerName: "images", blobName: $"test-{Guid.NewGuid().ToString()}.png");

            blobClient.Upload(filePath);

            var blobUri = blobClient.Uri.AbsoluteUri;
            Console.WriteLine(blobUri);
        }
    }
}