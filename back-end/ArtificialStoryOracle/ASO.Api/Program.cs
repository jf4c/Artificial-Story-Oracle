using ASO.Api.Middleware;
using ASO.Application.Abstractions.UseCase.Ancestry;
using ASO.Application.Abstractions.UseCase.Characters;
using ASO.Application.Abstractions.UseCase.Classes;
using ASO.Application.Abstractions.UseCase.Images;
using ASO.Application.Abstractions.UseCase.Oracle;
using ASO.Application.Abstractions.UseCase.Players;
using ASO.Application.Abstractions.UseCase.Skills;
using ASO.Application.UseCases.Ancestry.GetAllAncestry;
using ASO.Application.UseCases.Campaigns.Complete;
using ASO.Application.UseCases.Campaigns.Create;
using ASO.Application.UseCases.Campaigns.Delete;
using ASO.Application.UseCases.Campaigns.GetById;
using ASO.Application.UseCases.Campaigns.GetMyCampaigns;
using ASO.Application.UseCases.Campaigns.Pause;
using ASO.Application.UseCases.Campaigns.Resume;
using ASO.Application.UseCases.Campaigns.SetStory;
using ASO.Application.UseCases.Campaigns.Start;
using ASO.Application.UseCases.Campaigns.Update;
using ASO.Application.UseCases.Characters.Create;
using ASO.Application.UseCases.Characters.GetAll;
using ASO.Application.UseCases.Classes.GetAll;
using ASO.Application.UseCases.Friendships.AcceptRequest;
using ASO.Application.UseCases.Friendships.GetCounts;
using ASO.Application.UseCases.Friendships.GetFriends;
using ASO.Application.UseCases.Friendships.GetReceivedRequests;
using ASO.Application.UseCases.Friendships.GetSentRequests;
using ASO.Application.UseCases.Friendships.Remove;
using ASO.Application.UseCases.Friendships.RejectRequest;
using ASO.Application.UseCases.Friendships.SearchPlayers;
using ASO.Application.UseCases.Friendships.SendRequest;
using ASO.Application.UseCases.Images.GetAll;
using ASO.Application.UseCases.Oracle;
using ASO.Application.UseCases.Oracle.GenerateCampaignStory;
using ASO.Application.UseCases.Players.Create;
using ASO.Application.UseCases.Players.GetByUserId;
using ASO.Application.UseCases.Skills.GetAllSkills;
using ASO.Domain.AI.Abstractions.Repositories;
using ASO.Domain.Game.Abstractions.ExternalServices;
using ASO.Domain.Game.Abstractions.QueriesServices;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Identity.Repositories.Abstractions;
using ASO.Domain.Shared.Abstractions;
using ASO.Infra;
using ASO.Infra.Database;
using ASO.Infra.Database.Seeds;
using ASO.Infra.ExternalServices;
using ASO.Infra.QueriesServices;
using ASO.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:8080/realms/artificial-story-oracle";
        options.Audience = "account";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"❌ Auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnMessageReceived = context => Task.CompletedTask
        };
    });

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => { x.UseNpgsql(cnnStr); });

// Registrar MediatR para eventos de domínio
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateCharacterHandler).Assembly);
});

// Registrar UnitOfWork para dispatch de eventos
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true);
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
builder.Services.AddScoped<IImageQueryService, ImageQueryService>();

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IGeneratedAIContentRepository, GeneratedAIContentRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerUserRepository, PlayerUserRepository>();
builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<ICampaignParticipantRepository, CampaignParticipantRepository>();

builder.Services.AddScoped<ICreateCharacterHandler, CreateCharacterHandler>();
builder.Services.AddScoped<IGetAllCharactersHandler, GetAllCharactersHandler>();
builder.Services.AddScoped<IGetAllClassesHandler, GetAllClassesHandler>();
builder.Services.AddScoped<IGetAllSkillsHandler, GetAllSkillsHandler>();
builder.Services.AddScoped<IGetAllImagesHandler, GetAllImagesHandler>();
builder.Services.AddScoped<ICreatePlayerHandler, CreatePlayerHandler>();
builder.Services.AddScoped<IGetPlayerByUserIdHandler, GetPlayerByUserIdHandler>();

builder.Services.AddScoped<SendFriendRequestHandler>();
builder.Services.AddScoped<AcceptFriendRequestHandler>();
builder.Services.AddScoped<RejectFriendRequestHandler>();
builder.Services.AddScoped<RemoveFriendshipHandler>();
builder.Services.AddScoped<GetReceivedRequestsHandler>();
builder.Services.AddScoped<GetSentRequestsHandler>();
builder.Services.AddScoped<GetFriendsHandler>();
builder.Services.AddScoped<SearchPlayersHandler>();
builder.Services.AddScoped<GetFriendshipCountsHandler>();

builder.Services.AddScoped<CreateCampaignHandler>();
builder.Services.AddScoped<GetCampaignByIdHandler>();
builder.Services.AddScoped<GetMyCampaignsHandler>();
builder.Services.AddScoped<UpdateCampaignHandler>();
builder.Services.AddScoped<DeleteCampaignHandler>();
builder.Services.AddScoped<StartCampaignHandler>();
builder.Services.AddScoped<PauseCampaignHandler>();
builder.Services.AddScoped<ResumeCampaignHandler>();
builder.Services.AddScoped<CompleteCampaignHandler>();
builder.Services.AddScoped<SetCampaignStoryHandler>();

builder.Services.AddScoped<IGenerateCharacterBackstory, GenerateCharacterBackstory>();
builder.Services.AddScoped<IGenerateCampaignBackstory, GenerateCampaignBackstory>();
builder.Services.AddScoped<IGenerateCharactersNames, GenerateCharactersNames>();
builder.Services.AddScoped<GenerateCampaignStoryHandler>();
builder.Services.AddScoped<GenerateCampaignStoryFromCharactersHandler>();


builder.Services.AddHttpClient<IGeminiApiService, GeminiApiService>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config["ExternalServices:Gemini_API:BaseUrl"]
                  ?? throw new ArgumentNullException("Base URL for Gemini API is not configured.");
    var apiKey = config["ExternalServices:Gemini_API:Key"]
                 ?? throw new ArgumentNullException("API Key for Gemini API is not configured.");

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("X-goog-api-key", apiKey);
});

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
    ImageSeed.Seed(context);
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
app.UseCors("AllowAngular");

// Configurar arquivos estáticos para servir imagens
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});

app.UseAuthentication();
app.UseAuthorization();

// TODO: Adicionar middleware de tratamento de exceções quando for implementado
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();