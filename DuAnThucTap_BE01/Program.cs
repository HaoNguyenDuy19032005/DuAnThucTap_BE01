using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Helpers;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new NewtonsoftDateOnlyConverter());
});


builder.Services.AddDbContext<ISCDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IExamGraderService, ExamGraderService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<ITeacherConcurrentSubjectService, TeacherConcurrentSubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITeacherTrainingHistoryService, TeacherTrainingHistoryService>();
builder.Services.AddScoped<ITeacherWorkHistoryService, TeacherWorkHistoryService>();
builder.Services.AddScoped<ITeacherWorkStatusHistoryService, TeacherWorkStatusHistoryService>();
builder.Services.AddScoped<ITeachingAssignmentService, TeachingAssignmentService>();
builder.Services.AddScoped<ITestAssignment, TestAssignmentService>();
builder.Services.AddScoped<ITests, TestsService>();
builder.Services.AddScoped<ITopicListService, TopicListService>();
builder.Services.AddScoped<IExamScheduleService, ExamScheduleService>();
builder.Services.AddSingleton<IFirebaseStorageService, FirebaseStorageService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.UseDateOnlyTimeOnlyStringConverters();
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();