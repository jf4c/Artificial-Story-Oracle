using ASO.Api.Middleware;
using ASO.Application.Abstractions.UseCase.Ancestry;
using ASO.Application.Abstractions.UseCase.Characters;
using ASO.Application.Abstractions.UseCase.Classes;
using ASO.Application.Abstractions.UseCase.Skills;
using ASO.Application.UseCases.Ancestry.GetAllAncestry;
using ASO.Application.UseCases.Characters.Create;
using ASO.Application.UseCases.Characters.GetAll;
using ASO.Application.UseCases.Classes.GetAll;
using ASO.Application.UseCases.Skills.GetAllSkills;
using ASO.Domain.Game.QueriesServices;
using ASO.Domain.Game.Repositories.Abstractions;
using ASO.Infra.Database;
using ASO.Infra.Database.Seeds;
using ASO.Infra.QueriesServices;
using ASO.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:8080/realms/teste";
        options.Audience = "meu-client";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context => Task.CompletedTask,
            OnMessageReceived = context => Task.CompletedTask
        };
    });

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => { x.UseNpgsql(cnnStr); });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // permite Angular
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Minha API", Version = "v1" });

    // Configuração de segurança
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IGetAllAncestryHandler, GetAllAncestryHandler>();
builder.Services.AddScoped<IAncestryQueryService, AncestryQueryService>();
builder.Services.AddScoped<IClassQueryService, ClassQueryService>();
builder.Services.AddScoped<ISkillQueryService, SkillQueryService>();
builder.Services.AddScoped<ICreateCharacterHandler, CreateCharacterHandler>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IGetAllCharactersHandler, GetAllCharactersHandler>();
builder.Services.AddScoped<IGetAllClassesHandler, GetAllClassesHandler>();
builder.Services.AddScoped<IGetAllSkillsHandler, GetAllSkillsHandler>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

IdentityModelEventSource.ShowPII = true;


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    AncestrySeed.Seed(context);
    SkillSeed.Seed(context);
    ClassSeed.Seed(context);
}

// Adicionar middleware de tratamento de exceções no início do pipeline
app.UseExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASO.Api v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAngular");

// TODO: Adicionar middleware de tratamento de exceções quando for implementado
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();