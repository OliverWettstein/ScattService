using ScattService;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .AddEnvironmentVariables()
    .Build())
  .Enrich
    .WithExceptionDetails(new DestructuringOptionsBuilder()
    .WithDefaultDestructurers())
  .CreateBootstrapLogger();

AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

var builder = WebApplication.CreateBuilder(args);
builder.Host
  .UseSerilog((context, services, configuration) =>
    configuration
      .ReadFrom.Configuration(context.Configuration)
      .ReadFrom.Services(services)
      .Enrich
        .WithExceptionDetails(new DestructuringOptionsBuilder()
        .WithDefaultDestructurers()));

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
