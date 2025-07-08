using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Helpers;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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
    // Thêm dòng này vào
    options.SerializerSettings.Converters.Add(new NewtonsoftDateOnlyConverter());
});

builder.Services.AddDbContext<ISCDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ITeacherTrainingHistoryService, TeacherTrainingHistoryService>();
builder.Services.AddScoped<ITeacherWorkHistoryService, TeacherWorkHistoryService>();
builder.Services.AddScoped<ITeacherWorkStatusHistoryService, TeacherWorkStatusHistoryService>();
builder.Services.AddScoped<ITeacherConcurrentSubjectService, TeacherConcurrentSubjectService>();
builder.Services.AddScoped<ITeachingAssignmentService, TeachingAssignmentService>();
builder.Services.AddScoped<ITopicListService, TopicListService>();
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