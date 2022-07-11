using Twitter_Sentiment_API.Models;
using Twitter_Sentiment_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<HttpServices>();
builder.Services.AddHealthChecks();
if (builder.Environment.EnvironmentName == "Production")
{
    builder.Services.AddDbContext<TweetContext>(option =>
        option.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlTwitterAPI")));
    
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHealthChecks("/twitter/1.1/healthz");
app.MapControllers();

app.Run();