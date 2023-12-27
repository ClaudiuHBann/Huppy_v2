using Huppy.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HuppyContext>();

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
