using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Conferences.Domain.Interfaces;
using Conferences.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Conferences.Infrastructure.Storage
{
    internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

        public async Task<string> UploadToBlobAsync(Stream data, string filename)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

            var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);

            var blobClient = containerClient.GetBlobClient(filename);

            await blobClient.UploadAsync(data);

            var blobUrl = blobClient.Uri.ToString();

            return blobUrl;
        }

        public string? GetBlobSasUrl(string? blobUrl)
        {
            if (blobUrl == null) return null;

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _blobStorageSettings.LogosContainerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(7),
                BlobName = GetBlobNameFromUrl(blobUrl),
            };

            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);

            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

            var sasToken = sasBuilder
                .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName,
                _blobStorageSettings.AccountKey))
                .ToString();

            return $"{blobUrl}?{sasToken}";

        }

        private string GetBlobNameFromUrl(string blobUrl)
        {
            var uri = new Uri(blobUrl);

            return uri.Segments.Last();
        }
    }
}
