using Huppy.API.Models;
using Huppy.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HuppyContext>();
builder.Services.AddTransient<AppService>();
builder.Services.AddTransient<PackageService>();
builder.Services.AddTransient<CategoryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: fix CORS
app.UseCors(config => config.AllowAnyMethod()
                          .AllowAnyHeader()
                          .SetIsOriginAllowed(origin => true)
                          .WithOrigins("https://localhost:5001/")
                          .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
