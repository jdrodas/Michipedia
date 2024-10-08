using MICHIPEDIA_CS_REST_NoSQL_API.DbContexts;
using MICHIPEDIA_CS_REST_NoSQL_API.Interfaces;
using MICHIPEDIA_CS_REST_NoSQL_API.Repositories;
using MICHIPEDIA_CS_REST_NoSQL_API.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

//Aqui agregamos los servicios requeridos

//El DBContext a utilizar
builder.Services.AddSingleton<MongoDbContext>();

//Los repositorios
//builder.Services.AddScoped<IResumenRepository, ResumenRepository>();
builder.Services.AddScoped<IPaisRepository, PaisRepository>();
//builder.Services.AddScoped<IRazaRepository, RazaRepository>();
//builder.Services.AddScoped<ICaracteristicaRepository, CaracteristicaRepository>();

//Aqui agregamos los servicios asociados para cada ruta
//builder.Services.AddScoped<ResumenService>();
builder.Services.AddScoped<PaisService>();
//builder.Services.AddScoped<RazaService>();
//builder.Services.AddScoped<CaracteristicaService>();


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MICHIPEDIA - Enciclopedia de Gatos - Versión en MongoDB",
        Description = "API para la gestión de Información sobre razas de gatos"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Modificamos el encabezado de las peticiones para ocultar el web server utilizado
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Server", "MichiServer");
    await next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();