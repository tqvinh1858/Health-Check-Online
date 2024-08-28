using Microsoft.AspNetCore.Http;

namespace BHEP.Infrastructure.BlobStorage.Repository.IRepository;
public interface IBlobStorageService
{
    Task<string?> GetImageToBase64(string imageUrl);
    Task<string> UploadFormFormFile(IFormFile image, string name, string type);
    Task<List<string>> UploadFormMultiFormFile(List<IFormFile> listImages, string name, string type, string? containerName = null);
    Task DeleteBlobAsync(string imageUrl, string? containerName = null);
    Task DeleteMultiBlobAsync(List<string> imageUrl);
    Task DeleteBlobSnapshotsAsync(string imageUrl, string? containerName = null);
    Task RestoreBlobsAsync(string imageUrl, string? containerName = null);
    Task RestoreSnapshotsAsync(string imageUrl, string? containerName = null);
}
