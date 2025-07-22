namespace DuAnThucTap.Service
{
    public interface IFirebaseStorageService
    {
        /// <summary>
        /// Upload file lên Firebase Storage, trả về URL công khai
        /// </summary>
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    }
}
