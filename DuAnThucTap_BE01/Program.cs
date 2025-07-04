// Program.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Cấu hình để bỏ qua lỗi lặp vô hạn khi serialize JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


// 1. Thêm DbContext
builder.Services.AddDbContext<ISCDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Đăng ký các Services cho Dependency Injection
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ITeacherTrainingHistoryService, TeacherTrainingHistoryService>();
builder.Services.AddScoped<ITeacherWorkHistoryService, TeacherWorkHistoryService>();
builder.Services.AddScoped<ITeacherWorkStatusHistoryService, TeacherWorkStatusHistoryService>();
builder.Services.AddScoped<ITeacherConcurrentSubjectService, TeacherConcurrentSubjectService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();