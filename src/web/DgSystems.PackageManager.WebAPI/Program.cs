using DgSystems.Downloader;
using DgSystems.PackageManager;
using DgSystems.PackageManager.WebAPI.Install;
using DgSystems.PowerShell;
using DgSystems.Scoop;
using System.IO.Abstractions;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<Process>();
builder.Services.AddTransient<ScoopFactory>();
builder.Services.AddTransient<Notifier, LoggerNotifier>();
builder.Services.AddTransient<CommandLineShellFactory, PowerShellFactory>();
builder.Services.AddTransient<IFileSystem, FileSystem>();
builder.Services.AddHttpClient<Downloader, DownloadManager>();
builder.Services.AddSingleton<ExtractToDirectory>((ExtractToDirectory)((source, destination) => ZipFile.ExtractToDirectory(source, destination, true)));

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
