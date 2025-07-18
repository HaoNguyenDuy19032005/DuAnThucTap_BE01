using DuAnThucTap.Data;
using DuAnThucTap.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ISchoolinformationService, SchoolinformationService>();
builder.Services.AddScoped<ISchoolyearService, SchoolyearService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISubjecttypeService, SubjecttypeService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IGradetypeService, GradetypeService>();

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
