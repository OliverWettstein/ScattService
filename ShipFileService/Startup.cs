using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Perigon.ToolsWebApiService.DefaultEndpoints;
using Serilog;
using ScattService.Services;
using System.Reflection;

namespace ScattService {
  public class Startup {

    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) {
      _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {

      services.AddControllers();
      services.AddEndpointsApiExplorer();
      
      services.AddScoped<IGetScattFileService, GetScattFileService>();
      services.AddScoped<ISaveScattFileService, SaveScattFileService>();
      services.AddScoped<IFileSystemAccessRepository, FileSystemAccessRepository>();

      var ScattServiceSettings = _configuration.GetSection(nameof(ScattService.ScattServiceSettings));
      services.Configure<ScattServiceSettings>(ScattServiceSettings);

      services.AddHealthChecks();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env) {
      if (app.Environment.IsDevelopment()) {
        //app.UseSwagger();
        //app.UseSwaggerUI();
      }

      app.UseSerilogRequestLogging(options => {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) => {
          diagnosticContext.Set("HttpRequestClientIP", httpContext.Connection.RemoteIpAddress);
          diagnosticContext.Set("ClientAgent", httpContext.Request.Headers["User-Agent"].ToString() ?? "(unknown)");
        };
      });

      app.UseRouting();

      app.UseHttpsRedirection();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
        endpoints.MapPerigonDefaults(new ServiceInformation() {
          Identifier = "ScattService",
          Version = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty,
        }, false);
      });

      app.MapControllers();
    }

  
  }
}
