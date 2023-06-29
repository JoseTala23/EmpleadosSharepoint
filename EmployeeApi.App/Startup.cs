using System.Text.Json.Serialization;
using EmpleadosSharepoint;
using EmpleadosSharepoint.Options;

namespace WebApplication1;

public class Startup
{
    public IConfiguration Configuration { get; set; }
    public Startup(IConfiguration builderConfiguration)
    {
        Configuration = builderConfiguration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddHealthChecks();
        services.Configure<SharepointOptions>(Configuration.GetSection("SharePoint"));
        services.AddSingleton<IEmployee,SharePointEmployee>();

       

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });

        
    }
}