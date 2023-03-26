using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MsEstado.MsContext;
using MsEstado.Rpositorys.Interfaces;
using MsEstado.Rpositorys.Repository;
using MsEstado.Services.Interfaces;
using MsEstado.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MsEstadoContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddScoped<IRepositoryEstado, RepositoryEstado>();
builder.Services.AddScoped<IEstadoService, EstadoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Mypolicy",
                      policy =>
                      {
                          policy.WithOrigins("")
                            .WithHeaders(
                                    HeaderNames.ContentType,
                                    HeaderNames.Authorization)
                            .AllowAnyMethod()
                            .AllowCredentials();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
