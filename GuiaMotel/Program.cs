using GuiaMotel.Data;
using Microsoft.EntityFrameworkCore;
using Services.Authentication;
using Repository.Authentication;
using Services.Motels;
using Services.SuiteTypes;
using Services.Reservations; // Adicionado para o IReservationService e ReservationService
using Infra.Config; // Importar para acessar o JwtConfig

var builder = WebApplication.CreateBuilder(args);

// Configurar o EF Core para PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registrar serviços de autenticação
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Adicionar configuração do JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization(); // Garantir que o Authorization está registrado

// Registrar os novos serviços para Motel e SuiteType
builder.Services.AddScoped<IMotelService, MotelService>();
builder.Services.AddScoped<ISuiteTypeService, SuiteTypeService>();

// Registrar o serviço de Reservas
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // Middleware de autenticação para validar o JWT
app.UseAuthorization();  // Middleware de autorização

app.MapControllers();
app.Run();
