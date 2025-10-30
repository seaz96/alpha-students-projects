using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using StudentProjects.API.Configuration;
using StudentProjects.API.Middleware;
using StudentProjects.API.Utility;

var builder = WebApplication.CreateBuilder(args);
AuthOptions.Initialize(builder.Configuration);
builder.Host.UseSerilog((_, lc) => lc.GetConfiguration());
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddLoggerServices();
builder.Services.ConfigureDataServices(builder.Configuration);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = AuthOptions.TokenIssuer,
            ValidAudience = AuthOptions.TokenAudience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.SecurityKey,
            RoleClaimType = ClaimTypes.Role
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .WithOrigins("http://localhost:5173", "https://localhost:5173", "http://student-projects.ru") //todo: to config
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowCredentials();
        });
});

var app = builder.Build();
app.MigrateDatabase();
app.UsePathBase(new PathString("/api"));
app.MapOpenApi();
//todo: я хочу мидлвару с приведением всех ошибок в свой собственный формат
app.MapScalarApiReference(options => options.AddServer(app.Configuration.GetValue<string>("DOCUMENTATION_SERVER")!));
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseMiddleware<RequestHeadersComplementaryMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();