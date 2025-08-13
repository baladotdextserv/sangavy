using Serilog;
using Sangavy.API.Repository;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() 
    .WriteTo.Console()
    .WriteTo.File("Logs/app-log-.txt",
                  rollingInterval: RollingInterval.Day,
                  retainedFileCountLimit: 7, 
                  fileSizeLimitBytes: 10_000_000, 
                  rollOnFileSizeLimit: true)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddScoped<MailRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok("Sangavy API"));

app.Run();
