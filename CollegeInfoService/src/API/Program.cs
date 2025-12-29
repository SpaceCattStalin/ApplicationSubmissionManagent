using Application.Interfaces;
using Application.UseCases.CollegeInfoService.GetCampusById;
using Infrastructure;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// 1. Add Services
// -------------------------

builder.AddInfrastructureServices();
builder.AddApplicationServices();
// builder.AddWebServices();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Nếu không dùng base path "/collegeinfo", hãy bỏ dòng này
    // c.AddServer(new OpenApiServer { Url = "/collegeinfo" });
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "College API", Version = "v1" });
});

//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssembly(typeof(GetCampusByIdQueryHandler).Assembly));

// CORS: cấu hình một chính sách duy nhất dùng được cho Swagger và Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalClients", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5182", // Swagger UI
                "https://localhost:7149", // Gateway dev
                "http://localhost:8080"   // Gateway prod
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// -------------------------
// 2. Build app
// -------------------------

var app = builder.Build();

// Áp dụng migration và khởi tạo database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    await app.InitialiseDatabaseAsync();
}

// -------------------------
// 3. Configure Middleware
// -------------------------

app.UseCors("AllowLocalClients");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "College API V1");
    // Nếu có base path như "/collegeinfo", hãy dùng c.RoutePrefix = "collegeinfo";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();