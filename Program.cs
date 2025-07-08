using DuAnThucTapNhom3.Data;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using DuAnThucTapNhom3.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Thêm dịch vụ DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISchoolYearService, SchoolYearService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ISubjectTypeService, SubjectTypeService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IGradeLevelService, GradeLevelService>();
builder.Services.AddScoped<IClassTypeService, ClassTypeService>();
builder.Services.AddScoped<IClassService, ClassService>();
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
