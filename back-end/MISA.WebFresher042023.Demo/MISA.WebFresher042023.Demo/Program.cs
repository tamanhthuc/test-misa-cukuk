using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Repository;
using MISA.WebFresher042023.Demo.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions((options) =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .ToDictionary(x => x.Key, x => x.Value.Errors[0].ErrorMessage);

        var baseException = new BaseException()
        {
            ErrorCode = 400,
            UserMessage = "Lỗi nhập liệu người dùng",
            DevMessage = "Lỗi nhập liệu người dùng",
            TraceId = "",
            MoreInfo = "",
            Errors = errors
        };
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(baseException, jsonSerializerOptions);

        return new BadRequestObjectResult(json);
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRespository>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IDepartmentRespository, DepartmentRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAllOrigins");

app.MapControllers();


app.Run();
