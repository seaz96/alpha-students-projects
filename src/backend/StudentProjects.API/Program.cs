using Scalar.AspNetCore;
using Serilog;
using StudentProjects.API.Configuration;
using StudentProjects.API.Utility;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, lc) => lc.GetConfiguration());
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddLoggerServices();
builder.Services.ConfigureDataServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();