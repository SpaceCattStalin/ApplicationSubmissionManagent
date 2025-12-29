var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGateway", policy =>
    {
        policy.WithOrigins("*")
         .AllowAnyHeader()
         .AllowAnyMethod();
    });
});


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("http://localhost:5002/swagger/v1/swagger.json", "User API");
        c.SwaggerEndpoint("http://localhost:5012/swagger/v1/swagger.json", "Lead API");
        c.SwaggerEndpoint("http://localhost:5010/swagger/v1/swagger.json", "College Info API");
        c.SwaggerEndpoint("http://localhost:5014/swagger/v1/swagger.json", "Application API");
    });
}
else
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/user/v1/swagger.json", "User API");
        c.SwaggerEndpoint("/swagger/lead/v1/swagger.json", "Lead API");
        c.SwaggerEndpoint("/swagger/collegeinfo/v1/swagger.json", "College Info API");
    });
}
app.UseCors("AllowGateway");

app.MapReverseProxy();

app.Run();
