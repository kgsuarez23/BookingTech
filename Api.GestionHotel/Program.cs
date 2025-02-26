using Api.GestionHotel.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configura controladores y suprime la validación automática de ModelState
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddEndpointsApiExplorer();

// Registra configuraciones de la aplicación
var configuration = builder.Configuration;
builder.Services.AddSingleton(configuration);
builder.Services.AddApplicationServices(configuration);
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddHttpContextAccessor();

// Configuración de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("Usuario"));
});

var app = builder.Build();

// Configuración del pipeline de solicitudes
app.UseRouting();
app.UseErrorHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
});

app.MapControllers();
app.Run();
