using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Helpers;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// --- Database Context ---
builder.Services.AddDbContext<ISCDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DuAnThucTap_BE01.Interface.IContactService, DuAnThucTap_BE01.Services.ContactService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.IExamGraderService, DuAnThucTap_BE01.Services.ExamGraderService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.IExamService, DuAnThucTap_BE01.Services.ExamService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITeacherConcurrentSubjectService, DuAnThucTap_BE01.Services.TeacherConcurrentSubjectService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITeacherService, DuAnThucTap_BE01.Services.TeacherService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITeacherTrainingHistoryService, DuAnThucTap_BE01.Services.TeacherTrainingHistoryService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITeacherWorkHistoryService, DuAnThucTap_BE01.Services.TeacherWorkHistoryService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITeacherWorkStatusHistoryService, DuAnThucTap_BE01.Services.TeacherWorkStatusHistoryService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITeachingAssignmentService, DuAnThucTap_BE01.Services.TeachingAssignmentService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITestAssignment, DuAnThucTap_BE01.Services.TestAssignmentService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITests, DuAnThucTap_BE01.Services.TestsService>(); 
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.ITopicListService, DuAnThucTap_BE01.Services.TopicListService>();
builder.Services.AddScoped<DuAnThucTap_BE01.Interface.IExamScheduleService, DuAnThucTap_BE01.Services.ExamScheduleService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
