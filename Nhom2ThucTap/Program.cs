using DuAnThucTapNhom2.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Service;
using Nhom2ThucTap.Services;
//using Nhom2ThucTap.Services.Interfaces;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ✅ Sửa lỗi DateTime PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);

// ✅ Cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Đăng ký các service
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IClassTransferService, ClassTransferService>();
builder.Services.AddScoped<IStudentYearlyStatusService, StudentYearlyStatusService>();
builder.Services.AddScoped<ISchoolTransferHistoryService, SchoolTransferHistoryService>();
builder.Services.AddScoped<ICommendationtypeService, CommendationtypeService>();
builder.Services.AddScoped<IStudentcommendationService, StudentcommendationService>();
builder.Services.AddScoped<IDisciplinetypeService, DisciplinetypeService>();
builder.Services.AddScoped<IStudentDisciplineService, StudentDisciplineService>();
builder.Services.AddScoped<ISubjectsOfExemptionService, SubjectsOfExemptionService>();
builder.Services.AddScoped<IStudentExemptionService, StudentExemptionService>();
builder.Services.AddScoped<IStudentPreservationService, StudentPreservationService>();
builder.Services.AddScoped<IStudentsubjectsummaryService, StudentsubjectsummaryService>();
builder.Services.AddScoped<IStudentSemesterSummaryService, StudentSemesterSummaryService>();
builder.Services.AddScoped<IStudentTransferReceiptService, StudentTransferReceiptService>();
//builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IDisplayedTestListService, DisplayedTestListService>();
builder.Services.AddScoped<ITestHeaderService, TestHeaderService>();
builder.Services.AddScoped<ITestQuestionService, TestQuestionService>();
builder.Services.AddScoped<ITestStudentSubmissionService, TestStudentSubmissionService>();
builder.Services.AddScoped<ITestStudentAnswerService, TestStudentAnswerService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserNotificationService, UserNotificationService>();
//builder.Services.AddScoped<IUserThreadReadStatusService, UserThreadReadStatusService>();
//builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

// ✅ Cấu hình Controllers với JSON và tắt validation tự động
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // 👇 Quan trọng: Ngăn ASP.NET tự động trả lỗi model validation
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// ✅ Thêm CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
