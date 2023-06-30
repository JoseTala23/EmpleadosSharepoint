using System.Text.Json.Serialization;
using EmpleadosSharepoint;
using EmpleadosSharepoint.Options;
using EmployeeApi.Infraestructure.Authenticate;
using Microsoft.AspNetCore.Authentication;

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

        services.AddSingleton<IUser, UserService>();
        services.AddAuthentication("BasicAuthentication").
            AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>
            ("BasicAuthentication", null);


    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });

        
    }
}