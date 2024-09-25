var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la aplicación
builder.Services.AddControllers();  // Habilitar los controladores de la API

// Configurar Swagger si quieres documentación API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configuración del pipeline HTTP

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();              // Habilitar Swagger en entorno de desarrollo
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();          // Redirección a HTTPS (opcional)
app.UseAuthorization();             // Configuración de autorización si la necesitas
app.MapControllers();               // Mapeo de controladores para la API

app.Run();                          // Ejecutar la aplicación