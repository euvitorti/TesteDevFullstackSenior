using GuiaMotel.Data;
using Microsoft.EntityFrameworkCore;
using Services.Authentication;
using Repository.Authentication;
using Services.Motels;
using Services.Motel;
using Services.SuiteType; // Certifique-se de ajustar para o namespace onde estão os serviços de Motel e SuiteType

var builder = WebApplication.CreateBuilder(args);

// Configurar o EF Core para PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registrar serviços de autenticação
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>(); // Adicionar TokenService

// Registrar os novos serviços para Motel e SuiteType
builder.Services.AddScoped<IMotelService, MotelService>();
builder.Services.AddScoped<ISuiteTypeService, SuiteTypeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar o CORS (se necessário)
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend", policy =>
//     {
//         policy.WithOrigins("http://localhost:3000") // Ajuste a URL de acordo com seu frontend
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//     });
// });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseCors("AllowFrontend"); // Ativando o CORS
// app.UseHttpsRedirection();
app.UseAuthentication(); // Importante para JWT
app.UseAuthorization();

app.MapControllers();
app.Run();
