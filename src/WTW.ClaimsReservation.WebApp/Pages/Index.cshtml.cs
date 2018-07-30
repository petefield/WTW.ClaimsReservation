using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClaimsReservation.Core;
using ClaimsReservation.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ClaimsReservation.WebApp.Pages
{


    public class IndexModel : PageModel
    {
        [BindProperty]
        public IFormFile PostedFile { get; set; }

        [BindProperty]
        public TriangleSet TriangleSet { get; set; }

        [BindProperty]
        public String DownloadPath { get; set; }

        public void OnGet()
        {

        }

        public async Task<CloudBlockBlob> Open(string filename)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=wtwclaimsreservation;AccountKey=o9F+Ile4h0poqdorK1mCNPH+UghJ2IFaw9uSjoaytoyHJ1SHJNeobtkGppRFqb0V1OaynDL4zNYv8NPvBsdEHw==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("output");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);
            return blockBlob;
        }

        public async Task OnPost()
        { 
            try
            {
                var source = new DataSources.StreamDataSource(PostedFile.OpenReadStream());
                this.TriangleSet = new TriangleSetFactory(source).Create();
            }
            catch (Exception ex)
            {
                this.TriangleSet = null;
                this.ModelState.AddModelError("PostedFile", ex.Message);
            }

            if (ModelState.IsValid)
            {
                var outputFormatter = new Data.StreamOutputFormatter();

                this.DownloadPath = DateTime.UtcNow.ToString("dd-MMM-yyyy-hh-mm-ss") + ".csv";
                var blob = await Open(this.DownloadPath);
                var stream = await blob.OpenWriteAsync();
                outputFormatter.WriteOutput(stream, this.TriangleSet);
                this.DownloadPath = blob.Uri.ToString();
            }
        }

    }
}
