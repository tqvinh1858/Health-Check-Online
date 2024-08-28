using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using BHEP.Infrastructure.BlobStorage.DependencyInjection.Options;
using BHEP.Infrastructure.BlobStorage.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BHEP.Infrastructure.BlobStorage.Repositories;
public class BlobStorageService : IBlobStorageService
{
    private readonly BlobStorageOptions blobStorageOptions;

    public BlobStorageService(IOptions<BlobStorageOptions> blobStorageOptions)
    {
        this.blobStorageOptions = blobStorageOptions.Value;
    }

    public async Task<string?> GetImageToBase64(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return null;

        var blobPath = GetBlobPath(imageUrl);
        var blobServiceClient = new BlobServiceClient(blobStorageOptions.BlobUrl);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageOptions.Container);

        var blobClient = blobContainerClient.GetBlobClient(blobPath);

        if (!await blobClient.ExistsAsync())
        {
            return null;
        }


        using var memoryStream = new MemoryStream();

        await blobClient.DownloadToAsync(memoryStream, new BlobDownloadToOptions
        {
            TransferOptions = new StorageTransferOptions
            {
                // Set the maximum number of parallel transfer workers
                MaximumConcurrency = 2,

                // Set the initial transfer length to 8 MiB
                InitialTransferSize = 8 * 1024 * 1024,

                // Set the maximum length of a transfer to 4 MiB
                MaximumTransferSize = 4 * 1024 * 1024
            }
        });
        return Convert.ToBase64String(memoryStream.ToArray());
    }
    public async Task<string> UploadFormFormFile(IFormFile image, string name, string type)
    {

        BlobContainerClient container = new BlobContainerClient(
            blobStorageOptions.BlobUrl,
            blobStorageOptions.Container);
        string path = $"{type}/{name}-{DateTimeOffset.Now.ToUnixTimeSeconds()}";
        BlobClient blob = container.GetBlobClient(path);

        // Open the file and upload its data
        using (Stream stream = image.OpenReadStream())
        {
            await blob.UploadAsync(stream);
        }

        var uri = blob.Uri.AbsoluteUri;
        return uri;
    }
    public async Task<List<string>> UploadFormMultiFormFile(List<IFormFile> listImages, string name, string type, string? containerName = null)
    {
        List<string> urlImages = new List<string>();

        Random random = new Random();
        BlobContainerClient container = new BlobContainerClient(
            blobStorageOptions.BlobUrl,
            containerName ?? blobStorageOptions.Container);

        foreach (IFormFile image in listImages)
        {
            string path = $"{type}/{name}-{random.Next(0, int.MaxValue)}-{DateTimeOffset.Now.ToUnixTimeSeconds()}";
            BlobClient blob = container.GetBlobClient(path);

            // Open the file and upload its data
            using (Stream stream = image.OpenReadStream())
            {
                await blob.UploadAsync(stream);
            }
            var uri = blob.Uri.AbsoluteUri;

            urlImages.Add(uri);
        }

        return urlImages;
    }
    public async Task DeleteBlobAsync(string imageUrl, string? containerName = null)
    {
        var blobPath = GetBlobPath(imageUrl);

        BlobContainerClient container = new BlobContainerClient(
            blobStorageOptions.BlobUrl,
            containerName ?? blobStorageOptions.Container);
        BlobClient blob = container.GetBlobClient(blobPath);

        // Check if the blob exists before attempting to delete
        if (await blob.ExistsAsync())
        {
            await blob.DeleteAsync();
        }
    }
    public async Task DeleteBlobSnapshotsAsync(string imageUrl, string? containerName = null)
    {
        var blobPath = GetBlobPath(imageUrl);

        BlobContainerClient container = new BlobContainerClient(
            blobStorageOptions.BlobUrl,
            containerName ?? blobStorageOptions.Container);
        BlobClient blob = container.GetBlobClient(blobPath);

        // Check if the blob exists before attempting to delete
        if (await blob.ExistsAsync())
        {
            // Delete a blob and all of its snapshots
            await blob.DeleteAsync(snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots);

            // Delete only the blob's snapshots
            //await blob.DeleteAsync(snapshotsOption: DeleteSnapshotsOption.OnlySnapshots);
        }
    }
    public async Task DeleteMultiBlobAsync(List<string> imageUrls)
    {
        foreach (string imageUrl in imageUrls)
        {
            await DeleteBlobAsync(imageUrl);
        }
    }
    public async Task RestoreBlobsAsync(string imageUrl, string? containerName = null)
    {
        var blobPath = GetBlobPath(imageUrl);
        BlobContainerClient container = new BlobContainerClient(
            blobStorageOptions.BlobUrl,
            containerName ?? blobStorageOptions.Container);
        BlobClient blob = container.GetBlobClient(blobPath);

        await container.GetBlockBlobClient(blob.Name).UndeleteAsync();

    }
    public async Task RestoreSnapshotsAsync(string imageUrl, string? containerName = null)
    {
        var blobPath = GetBlobPath(imageUrl);
        BlobContainerClient container = new BlobContainerClient(
                    blobStorageOptions.BlobUrl,
                    containerName ?? blobStorageOptions.Container);
        BlobClient blob = container.GetBlobClient(blobPath);
        // Restore the deleted blob
        await blob.UndeleteAsync();

        // List blobs in this container that match prefix
        // Include snapshots in listing
        Pageable<BlobItem> blobItems = container.GetBlobs(
            BlobTraits.None,
            BlobStates.Snapshots,
            prefix: blob.Name);

        // Get the URI for the most recent snapshot
        BlobUriBuilder blobSnapshotUri = new BlobUriBuilder(blob.Uri)
        {
            Snapshot = blobItems
                .OrderByDescending(snapshot => snapshot.Snapshot)
                .ElementAtOrDefault(0)?.Snapshot
        };

        // Restore the most recent snapshot by copying it to the blob
        await blob.StartCopyFromUriAsync(blobSnapshotUri.ToUri());
    }

    private string GetBlobPath(string imageUrl)
    {
        Uri uri = new Uri(imageUrl);
        string containerName = uri.Segments[1];  // Assuming the container name is the second segment in the URI

        // Get blobPath
        int startIndex = containerName.Length + 1;
        int length = uri.PathAndQuery.Length - containerName.Length - 1;
        string blobPath = uri.PathAndQuery.Substring(startIndex, length);
        return blobPath;
    }
    private BlobClient GetBlobClient(string blobPath)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageOptions.BlobUrl);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageOptions.Container);
        return blobContainerClient.GetBlobClient(blobPath);
    }
    private async Task<bool> BlobExists(BlobClient blobClient)
    {
        try
        {
            return await blobClient.ExistsAsync();
        }
        catch
        {
            return false;
        }
    }
}
