using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Server.Services;
using Server.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library API",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Client", policy =>
    {
        policy.WithOrigins("http://localhost:5180", "https://localhost:7124")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Client");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();


app.Run();
