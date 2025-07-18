namespace DuAnThucTap_BE01.Iterface
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string destinationPath);
    }
}
