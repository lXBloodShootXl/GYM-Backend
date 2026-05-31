using GYM.Core.Interfaces;
using GYM.Infraestructura.Data;
using GYM.Infraestructura.Repositorio;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositorios;
using RRHH.Infraestructura.Repositorio;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Intentar obtener DATABASE_URL (Railway)
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

string connectionString;

if (!string.IsNullOrEmpty(databaseUrl))
{
    // Convertir de formato postgres:// a formato Npgsql
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    connectionString = $"Host={uri.Host};" +
                       $"Port={uri.Port};" +
                       $"Database={uri.AbsolutePath.TrimStart('/')};" +
                       $"Username={userInfo[0]};" +
                       $"Password={userInfo[1]};" +
                       $"SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    // Usar configuración local (appsettings.json)
    connectionString = builder.Configuration.GetConnectionString("GYMContext");
}

// Configurar DbContext
builder.Services.AddDbContext<GYM_DBContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure();
    }));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyApp", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    });
});

// Añadir controladores, Swagger y la API de endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar repositorios
builder.Services.AddScoped<ITelefonoRepositorio, TelefonoRepositorio>();
builder.Services.AddScoped<ICorreoRepositorio, CorreoRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IPersonaTelefonoRepositorio, PersonaTelefonoRepositorio>();
builder.Services.AddScoped<IPersonaCorreoRepositorio, PersonaCorreoRepositorio>();
builder.Services.AddScoped<IPersonaAsistenciaRepositorio, PersonaAsistenciaRepositorio>();
builder.Services.AddScoped<IAsistenciaRepositorio, AsistenciaRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IMembresiaRepositorio, MembresiaRepositorio>();
builder.Services.AddScoped<ISuscripcionRepositorio, SuscripcionRepositorio>();
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();
builder.Services.AddScoped<IEmpleadoRepositorio, EmpleadoRepositorio>();
builder.Services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
builder.Services.AddScoped<IEmpleadoTurnoRepositorio, EmpleadoTurnoRepositorio>();
builder.Services.AddScoped<ICargoRepositorio, CargoRepositorio>();
builder.Services.AddScoped<ISalarioRepositorio, SalarioRepositorio>();
builder.Services.AddScoped<ICargoSalarioRepositorio,CargoSalarioRepositorio>();
builder.Services.AddScoped<IEmpleadoCargoRepositorio,EmpleadoCargoRepositorio>();
builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>();
builder.Services.AddScoped<IDetalleVentaRepositorio, DetalleVentaRepositorio>();
builder.Services.AddScoped<IStockRepositorio, StockRepositorio>();
builder.Services.AddScoped<IAuditoriaRepositorio, AuditoriaRepositorio>();
var app = builder.Build();

// Aplicar migraciones al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GYM_DBContext>();
    try
    {
        dbContext.Database.Migrate(); // Aplica migraciones si no existen
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error aplicando migraciones: " + ex.Message);
    }

    // Ejecutar creación de vistas en la base de datos
    //await CrearVistas(dbContext);
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    //c.RoutePrefix = string.Empty; // Esto hace que Swagger esté en la raíz (puedes ajustarlo si necesitas otro lugar)
});

// Middleware
app.UseCors("MyApp");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
