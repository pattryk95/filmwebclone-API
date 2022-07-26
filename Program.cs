using filmwebclone_API.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policyBuilder =>

        policyBuilder.AllowAnyMethod().AllowAnyHeader().WithOrigins(builder.Configuration["AllowedOrigins"])
    );
});

builder.Services.AddDbContext<FilmwebCloneContext>(
        option => option.UseSqlServer(builder.Configuration.GetConnectionString("FilmwebCloneConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("FrontEndClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
