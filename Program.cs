using AutoMapper;
using filmwebclone_API.APIBehavior;
using filmwebclone_API.Entities;
using filmwebclone_API.Filters;
using filmwebclone_API.Helpers;
using filmwebclone_API.Services;
using filmwebclone_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ParseBadRequest));
}).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policyBuilder =>

        policyBuilder.AllowAnyMethod()
                     .AllowAnyHeader()
                     .WithOrigins(builder.Configuration["AllowedOrigins"])
                     .WithExposedHeaders(new string[] { "totalAmountOfRecords" })
    );
});

builder.Services.AddDbContext<FilmwebCloneContext>(
        option => option.UseSqlServer(builder.Configuration.GetConnectionString("FilmwebCloneConnectionString"), 
        sqlOptions => sqlOptions.UseNetTopologySuite())
    );


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IMovietheaterService, MovietheaterService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(provider => new MapperConfiguration(config =>
{
    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
    config.AddProfile(new AutoMapperProfiles(geometryFactory));
}).CreateMapper());
builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

builder.Services.AddScoped<IFileStorageService, LocalStorageService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();
app.UseCors("FrontEndClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<FilmwebCloneContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

app.Run();
