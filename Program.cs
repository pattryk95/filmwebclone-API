var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policyBuilder =>

        policyBuilder.AllowAnyMethod().AllowAnyHeader().WithOrigins(builder.Configuration["AllowedOrigins"])
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("FrontEndClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
