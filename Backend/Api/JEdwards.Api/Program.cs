using JEdwards.Application.Implementations;
using JEdwards.Application.Interfaces;
using JEdwards.Infrastructure.Api.Implemenations;
using JEdwards.Infrastructure.Api.Interfaces;
using JEdwards.Infrastructure.Database.Implemenations;
using JEdwards.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
const string policy = "localhost";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policy, builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Infrastructure
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddTransient<IDataAccessService, DataAccessService>();
builder.Services.AddSingleton<IOmdbApiService>(s => new OmdbApiService(builder.Configuration.GetValue<string>("OmdbapiKey")));
#endregion

#region Application
builder.Services.AddTransient<IMovieService, MovieService>();
#endregion


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("./index.html");

app.UseCors(policy);

app.Run();
