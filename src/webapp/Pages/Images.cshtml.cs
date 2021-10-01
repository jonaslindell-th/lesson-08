using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace webapp.Pages
{
    public class ImagesModel : PageModel
    {
        public IConfiguration _config { get; set; }
        public ImagesModel(IConfiguration config)
        {
            _config = config;
        }
        public List<Uri> allBlobs = new List<Uri>();
        public async Task<IActionResult> OnGet()
        {
            string connectionString = _config.GetConnectionString("AzureKey");

            // Analyserar min connection string och returnerar en cloud storage account instans som skapats från min connectionstring.
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            // Skapar min blob service client.
            var _blobClient = storageAccount.CreateCloudBlobClient();
            // Hämtar ett cloud blob container objekt baserat på min parameter, i mitt fall min images container.
            var _blobContainer = _blobClient.GetContainerReference("images");

            // ListBlobsSegmentedAsync hämtar in en viss mängd åt gången och kräver en continuationtoken, så för det så skapar jag upp en med värdet null. Finns det mer blobs efter jag hämtat in, så returneras en not null continuation token.
            BlobContinuationToken blobContinuationToken = null;
            
            // Så länge continuationtoken är skiljt från null så hämta in och lägg till blob uri's till min lista.
            do
            {
                var response = await _blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                        allBlobs.Add(blob.Uri);
                }
                blobContinuationToken = response.ContinuationToken;
            } while (blobContinuationToken != null);

            return Page();
        }
    }
}
