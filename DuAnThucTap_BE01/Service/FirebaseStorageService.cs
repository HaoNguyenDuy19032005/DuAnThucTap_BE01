using DuAnThucTap.Configs;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace DuAnThucTap.Service
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly StorageClient _client;
        private readonly FirebaseConfig _cfg;

        public FirebaseStorageService(StorageClient client,
                                      IOptions<FirebaseConfig> opt)
        {
            _client = client;
            _cfg = opt.Value;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            // Tạo object name duy nhất
            var objectName = $"images/{Guid.NewGuid()}_{fileName}";

            // Upload lên bucket
            await _client.UploadObjectAsync(
                bucket: _cfg.BucketName,
                objectName: objectName,
                contentType: contentType,
                source: fileStream

            );

            // Trả về URL public
            return $"https://storage.googleapis.com/{_cfg.BucketName}/{objectName}";
        }
    }
}
