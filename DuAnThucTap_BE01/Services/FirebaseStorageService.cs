using DuAnThucTap_BE01.Interface; // Sửa lỗi chính tả: Iterface -> Interface
using DuAnThucTap_BE01.Iterface;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public FirebaseStorageService(IConfiguration configuration)
        {
            // Lấy thông tin từ file appsettings.json
            var googleCredentialsJsonPath = configuration["Firebase:GoogleCredentialsJson"];
            _bucketName = configuration["Firebase:BucketName"];

            // Thêm kiểm tra để đảm bảo cấu hình tồn tại, tránh lỗi runtime
            if (string.IsNullOrEmpty(googleCredentialsJsonPath) || string.IsNullOrEmpty(_bucketName))
            {
                throw new ArgumentNullException(nameof(configuration), "Cấu hình Firebase (BucketName, GoogleCredentialsJson) không được để trống trong appsettings.json.");
            }

            var credentials = GoogleCredential.FromFile(googleCredentialsJsonPath);
            _storageClient = StorageClient.Create(credentials);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string destinationPath)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File không được để trống.", nameof(file));
            }

            // Tạo tên file duy nhất để tránh trùng lặp
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Đảm bảo đường dẫn thư mục kết thúc bằng dấu "/"
            if (!destinationPath.EndsWith("/"))
            {
                destinationPath += "/";
            }
            var objectName = $"{destinationPath}{uniqueFileName}";

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0; // Reset vị trí stream

                // ✅ SỬA LỖI: Thêm tùy chọn để file được public và có thể xem được qua URL
                await _storageClient.UploadObjectAsync(
                    bucket: _bucketName,
                    objectName: objectName,
                    contentType: file.ContentType,
                    source: stream,
                    options: new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead }
                );
            }

            // Trả về URL công khai, dễ truy cập
            return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
        }
    }
}
