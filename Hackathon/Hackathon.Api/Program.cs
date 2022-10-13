using System.Text.RegularExpressions;
using Hackathon.Api.Predictors;

var domainRegex =
    new Regex(
        @"^(http://localhost:\d{4,5}|https://lemon-wave-0a1dfce03(\.|-)(\d{1,}\.)?(westeurope(\.\d{1,})\.)?azurestaticapps\.net)/?",
        RegexOptions.Compiled);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(domain => domainRegex.IsMatch(domain));
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<KocarPredictor>();
builder.Services.AddSingleton<MiraclePredictor>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();