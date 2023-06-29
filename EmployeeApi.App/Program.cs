

using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new(builder.Configuration);
startup.ConfigureServices(builder.Services);



builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));



var app = builder.Build();

app.UseCors("corsapp");

startup.Configure(app, builder.Environment, app.Services);

app.Run();