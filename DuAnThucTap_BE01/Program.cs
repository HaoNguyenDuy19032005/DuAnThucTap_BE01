using DuAnThucTap.Configs;              
using DuAnThucTap.Data;
using DuAnThucTap.Irepository;
using DuAnThucTap.Service;
using DuAnThucTap.Services;
using Google.Apis.Auth.OAuth2;          
using Google.Cloud.Storage.V1;          
using Microsoft.AspNetCore.Http.Features; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;      

var builder = WebApplication.CreateBuilder(args);

// 1. Đọc cấu hình Firebase từ appsettings.json
builder.Services.Configure<FirebaseConfig>(
    builder.Configuration.GetSection("Firebase"));

// 2. Tạo singleton StorageClient dùng để upload
builder.Services.AddSingleton(sp =>
{
    var cfg = sp.GetRequiredService<IOptions<FirebaseConfig>>().Value;
    var cred = GoogleCredential
        .FromFile(cfg.CredentialPath)
        .CreateScoped("https://www.googleapis.com/auth/devstorage.full_control");
    return StorageClient.Create(cred);
});

// 3. Đăng ký service upload của bạn
builder.Services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();

// (Tuỳ chọn) 4. Tăng giới hạn kích thước multipart/form-data nếu cần
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100 MB
});

// ---- Các service cũ của bạn ----
builder.Services.AddScoped<ISchoolinformationService, SchoolinformationService>();
builder.Services.AddScoped<ISchoolyearService, SchoolyearService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISubjecttypeService, SubjecttypeService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IGradetypeService, GradetypeService>();
builder.Services.AddScoped<ICampusService, CampusService>();
builder.Services.AddScoped<IDepartmentleadersService, DepartmentleaderService>();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.ToDictionary(
            kv => kv.Key,
            kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
        );
        return new BadRequestObjectResult(new
        {
            success = false,
            message = "Dữ liệu không hợp lệ!",
            errors
        });
    };
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DuAnThucTap API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
