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

            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var _blobClient = storageAccount.CreateCloudBlobClient();

            var _blobContainer = _blobClient.GetContainerReference("images");


            BlobContinuationToken blobContinuationToken = null;

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
