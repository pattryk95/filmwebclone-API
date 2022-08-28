using AutoMapper;
using filmwebclone_API.APIBehavior;
using filmwebclone_API.Entities;
using filmwebclone_API.Filters;
using filmwebclone_API.Helpers;
using filmwebclone_API.Services;
using filmwebclone_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IAccountService, AccountService> ();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(provider => new MapperConfiguration(config =>
{
    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
    config.AddProfile(new AutoMapperProfiles(geometryFactory));
}).CreateMapper());
builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

builder.Services.AddScoped<IFileStorageService, LocalStorageService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<FilmwebCloneContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["keyjwt"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
});

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
