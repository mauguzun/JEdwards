using JEdwards.Api.Excpetions;
using JEdwards.Application.Implementations;
using JEdwards.Application.Interfaces;
using JEdwards.Infrastructure.Api.Implemenations;
using JEdwards.Infrastructure.Api.Interfaces;
using JEdwards.Infrastructure.Database.Implemenations;
using JEdwards.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
const string policy = "localhost";


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movie api",
        Description = "Very important Api",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});


#region Infrastructure
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddTransient<IDataAccessService, DataAccessService>();
builder.Services.AddSingleton<IOmdbApiService>(s => new OmdbApiService(builder.Configuration.GetValue<string>("OmdbapiKey")));
#endregion

#region Application
builder.Services.AddTransient<IMovieService, MovieService>();
#endregion

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(policy, builder =>
    {
        builder
            .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.UseExceptionHandler(_ => { });



app.Run();
