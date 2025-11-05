# Instruções para GitHub Copilot - Artificial Story Oracle (ASO)

## 1. Estilo de Comunicação

- **NÃO COMEÇAR AS RESPOSTAS COM "Você está absolutamente certo!" OU AFIRMAÇÕES SIMILARES.**
- **Seja Honesto e Direto:** Forneça percepções genuínas sobre qualidade de código, decisões arquiteturais e direção do projeto.
- **Ofereça Análise Crítica:** Em conversas de planejamento, não hesite em apontar possíveis problemas, sugerir alternativas ou desafiar abordagens.
- **Foque no Valor:** Em vez de elogios, ofereça observações concretas sobre o que funciona bem e o que pode ser melhorado.
- **Esclareça Ambiguidades:** Se meu pedido for confuso ou ambíguo, faça perguntas para garantir entendimento.
- **Evite Elogios Vazios:** Nada de “boa pergunta!” ou “você está certo!” — responda diretamente.

### Comportamentos Esperados

- **Vá Direto ao Ponto:** Em vez de “Esse é um ponto fascinante!” → já responda direto.
- **Corrija Mal-entendidos:** “Na verdade, isso não está certo porque…”
- **Esclareça Intenções:** “Você está perguntando sobre X ou Y especificamente?”
- **Aponte Erros:** “Acredito que há um erro nessa lógica, pois 2+2 não é 5…”

---

## 🎯 Visão Geral do Projeto

Este é um sistema backend em **.NET 9.0** para gerenciamento de RPG com IA, seguindo **Clean Architecture** e **Domain-Driven Design (DDD)**. O projeto utiliza PostgreSQL, Entity Framework Core e integração com Gemini API para geração de conteúdo narrativo.

**IMPORTANTE:** Sempre responda e documente em **Português Brasileiro (PT-BR)**.

---

## 🏗️ Arquitetura e Estrutura de Projetos

### Camadas do Projeto

```
ArtificialStoryOracle/
├── ASO.Api/                    # Controllers, Middleware, Inputs
├── ASO.Application/            # Use Cases, Handlers, Mappers
├── ASO.Domain/                 # Entities, Value Objects, Regras de Negócio
├── ASO.Infra/                  # Repositories, Database, External Services
└── ASO.Domain.Tests/           # Testes Unitários
```

### Fluxo de Dados

```
Client → Controller → Input Mapper → Handler → Domain Entity → Repository → Database
```

---

## 📁 Convenções de Estrutura de Pastas

### **ASO.Api/**
```
ASO.Api/
├── Controllers/              # Endpoints REST
├── Inputs/                   # DTOs de entrada
│   └── Mappers/             # Conversão Input → Command/Query
├── Middleware/              # Middleware customizado
└── Properties/              # launchSettings.json
```

### **ASO.Application/**
```
ASO.Application/
├── Abstractions/
│   ├── Shared/             # Interfaces base (ICommandHandler, IQueryHandler)
│   └── UseCase/            # Interfaces específicas por feature
│       └── [Feature]/      # Ex: Characters/, Oracle/, Classes/
├── UseCases/
│   └── [Feature]/          # Ex: Characters/, Classes/, Skills/
│       ├── Create/
│       │   ├── Create[Feature]Command.cs
│       │   ├── Create[Feature]Handler.cs
│       │   └── Create[Feature]Response.cs
│       └── GetAll/
│           ├── GetAll[Feature]Filter.cs
│           ├── GetAll[Feature]Handler.cs
│           └── GetAll[Feature]Response.cs
├── Mappers/                # Entity → Response
├── Builders/               # Query builders
├── Pagination/             # PaginatedResult, PaginatedQueryBase
└── Extensions/             # Extensions para IQueryable
```

### **ASO.Domain/**
```
ASO.Domain/
├── [Subdominio]/           # Ex: Game/, AI/, Identity/
│   ├── Entities/          # Entidades com identidade
│   ├── ValueObjects/      # Records imutáveis
│   ├── Dtos/              # DTOs internos do domínio
│   ├── Enums/             # Enumeradores
│   ├── Events/            # Domain Events
│   ├── Exceptions/        # Exceções customizadas
│   └── Abstractions/      # Interfaces (Repositories, Services)
│       ├── Repositories/
│       ├── QueriesServices/
│       └── ExternalServices/
└── Shared/                # Compartilhado entre subdomínios
    ├── Entities/          # Base Entity
    ├── ValueObjects/      # ValueObjects comuns
    ├── Aggregates/        # IAggregateRoot
    ├── Events/            # IDomainEvent
    └── Exceptions/        # Exceções base
```

### **ASO.Infra/**
```
ASO.Infra/
├── Database/
│   ├── AppDbContext.cs
│   ├── Seeds/             # Seed data
│   └── Configurations/    # Entity configurations
├── Repositories/          # Implementação dos repositórios
├── QueriesServices/       # Implementação de serviços de leitura
├── ExternalServices/      # Integrações externas (Gemini API)
└── Migrations/            # EF Core migrations
```

---

## 🎨 Padrões de Projeto a Seguir

### 1. **CQRS (Command Query Responsibility Segregation)**

**Commands:** Operações de escrita
```csharp
// Interface
public interface ICreate[Feature]Handler 
    : ICommandHandlerAsync<Create[Feature]Command, Create[Feature]Response>;

// Handler
public sealed class Create[Feature]Handler(
    I[Feature]Repository repository) : ICreate[Feature]Handler
{
    public async Task<Create[Feature]Response> HandleAsync(Create[Feature]Command command)
    {
        var entity = [Feature].Create(command.ToDto());
        await _repository.Create(entity);
        return entity.ToResponse();
    }
}
```

**Queries:** Operações de leitura
```csharp
public interface IGetAll[Feature]Handler 
    : IQueryHandlerAsync<GetAll[Feature]Filter, PaginatedResult<GetAll[Feature]Response>>;
```

### 2. **Repository Pattern**

**Interface no Domain:**
```csharp
// ASO.Domain/Game/Abstractions/Repositories/
public interface I[Feature]Repository : IRepository<[Feature]>
{
    Task<[Feature]> Create([Feature] entity);
    IQueryable<[Feature]> GetAll();
}
```

**Implementação na Infrastructure:**
```csharp
// ASO.Infra/Repositories/
public class [Feature]Repository(AppDbContext context) : I[Feature]Repository
{
    private readonly AppDbContext _context = context;
    
    public async Task<[Feature]> Create([Feature] entity)
    {
        await _context.[Features].AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public IQueryable<[Feature]> GetAll()
    {
        return _context.[Features].AsQueryable();
    }
}
```

### 3. **Factory Pattern em Entidades**

**Sempre usar método estático `Create()` para instanciar entidades:**
```csharp
public class [Feature] : Entity, IAggregateRoot
{
    // Construtor privado
    private [Feature]() { }
    
    // Construtor privado completo
    private [Feature](string name, /* params */)
    {
        Name = name;
        // Atribuições...
    }
    
    // Factory method público
    public static [Feature] Create(Create[Feature]Dto dto)
    {
        // Validações
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Nome não pode ser vazio.");
            
        return new [Feature](dto.Name, /* params */);
    }
}
```

### 4. **Mapper Pattern**

**Mappers estáticos em extension methods:**
```csharp
// ASO.Application/Mappers/
public static class [Feature]Mapper
{
    // Entity → Response
    public static [Feature]Response ToResponse(this [Feature] entity)
    {
        return new [Feature]Response
        {
            Id = entity.Id,
            Name = entity.Name,
            // ...
        };
    }
    
    // Command → Dto
    public static Create[Feature]Dto ToDto(this Create[Feature]Command command)
    {
        return new Create[Feature]Dto(
            command.Name,
            // ...
        );
    }
}

// ASO.Api/Inputs/Mappers/
public static class [Feature]InputMapper
{
    // Input → Command
    public static Create[Feature]Command ToCommand(this Create[Feature]Input input)
    {
        return new Create[Feature]Command
        {
            Name = input.Name,
            // ...
        };
    }
}
```

### 5. **Value Objects (DDD)**

**Sempre usar `record` para Value Objects:**
```csharp
// ASO.Domain/[Subdominio]/ValueObjects/
public record [ValueObject]
{
    public string Value { get; }
    
    public [ValueObject](string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("[ValueObject] inválido.");
            
        Value = value;
    }
}
```

### 6. **Dependency Injection**

**Registrar serviços no `Program.cs`:**
```csharp
// Handlers
builder.Services.AddScoped<I[Feature]Handler, [Feature]Handler>();

// Repositories
builder.Services.AddScoped<I[Feature]Repository, [Feature]Repository>();

// Query Services
builder.Services.AddScoped<I[Feature]QueryService, [Feature]QueryService>();

// External Services (com HttpClient)
builder.Services.AddHttpClient<IExternalService, ExternalService>();
```

---

## 💻 Convenções de Código

### Nomenclatura

- **Controllers:** `[Feature]Controller.cs`
- **Handlers:** `[Action][Feature]Handler.cs` (Ex: `CreateCharacterHandler.cs`)
- **Commands:** `[Action][Feature]Command.cs`
- **Responses:** `[Action][Feature]Response.cs`
- **Inputs:** `[Action][Feature]Input.cs`
- **Repositories:** `I[Feature]Repository` / `[Feature]Repository`
- **Entities:** `[Feature].cs` (singular)
- **Tables:** Plural do nome da entidade

### Injeção de Dependência

**Sempre usar Primary Constructor (C# 12):**
```csharp
public sealed class [Feature]Handler(
    I[Feature]Repository repository,
    IOtherService service) : I[Feature]Handler
{
    private readonly I[Feature]Repository _repository = repository;
    private readonly IOtherService _service = service;
}
```

### Modificadores de Acesso

- **Handlers:** `sealed class`
- **Entities:** `class` (podem ser herdadas se necessário)
- **Construtores de Entities:** `private`
- **Factory Methods:** `public static`

### Async/Await

- **Todos os métodos I/O devem ser assíncronos**
- **Sufixo `Async` em métodos:** `HandleAsync`, `CreateAsync`, `GetByIdAsync`
- **Usar `Task<T>` para retornos assíncronos**

### Validações

- **Validações de domínio:** Dentro das entidades (no método `Create()`)
- **Validações de aplicação:** Nos handlers
- **Validações de entrada:** Nos controllers (via DataAnnotations se necessário)

---

## 🎯 Controllers (ASO.Api)

### Template de Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class [Feature]Controller(
    ICreate[Feature]Handler createHandler,
    IGetAll[Feature]Handler getAllHandler) : ControllerBase
{
    private readonly ICreate[Feature]Handler _createHandler = createHandler;
    private readonly IGetAll[Feature]Handler _getAllHandler = getAllHandler;

    [HttpPost]
    [Authorize] // ou [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] Create[Feature]Input input)
    {
        var command = input.ToCommand();
        var response = await _createHandler.HandleAsync(command);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAll[Feature]Query query)
    {
        var filter = query.ToFilter();
        var response = await _getAllHandler.Handle(filter);
        return Ok(response);
    }
}
```

### Regras para Controllers

- Controllers devem ser **finos** - apenas delegar para handlers
- Usar `[ApiController]` e `[Route("api/[controller]")]`
- Métodos devem retornar `Task<IActionResult>`
- Usar `[FromBody]`, `[FromQuery]`, `[FromRoute]` explicitamente
- Autenticação: `[Authorize]` ou `[AllowAnonymous]`

---

## 🔧 Handlers (ASO.Application)

### Template de Handler de Criação

```csharp
public sealed class Create[Feature]Handler(
    I[Feature]Repository repository,
    IOtherQueryService otherService) : ICreate[Feature]Handler
{
    private readonly I[Feature]Repository _repository = repository;
    private readonly IOtherQueryService _otherService = otherService;
    
    public async Task<Create[Feature]Response> HandleAsync(Create[Feature]Command command)
    {
        // Buscar dependências se necessário
        var otherEntity = await _otherService.GetById(command.OtherId);
        
        // Criar DTO
        var dto = command.ToDto(otherEntity);
        
        // Criar entidade usando Factory
        var entity = [Feature].Create(dto);
        
        // Persistir
        await _repository.Create(entity);

        // Retornar response
        return entity.ToResponse();
    }
}
```

### Template de Handler de Listagem/Query

```csharp
public sealed class GetAll[Feature]Handler(
    I[Feature]QueryService queryService) : IGetAll[Feature]Handler
{
    private readonly I[Feature]QueryService _queryService = queryService;
    
    public async Task<PaginatedResult<GetAll[Feature]Response>> Handle(GetAll[Feature]Filter filter)
    {
        var query = _queryService.GetAll();
        
        var builder = new GetPaginated[Feature]QueryBuilder(query, filter);
        var result = await builder
            .SetFilter()
            .SetOrderBy()
            .SetPagination()
            .BuildAsync();
        
        return result;
    }
}
```

---

## 🏛️ Entidades (ASO.Domain)

### Template de Entidade

```csharp
public class [Feature] : Entity, IAggregateRoot
{
    #region Constructors

    // Construtor para EF Core
    private [Feature]()
    {
        // Inicializar propriedades obrigatórias com valores padrão
        Name = null!;
        RelatedEntities = new();
    }

    // Construtor real
    private [Feature](string name, OtherEntity otherEntity)
    {
        Name = name;
        OtherEntity = otherEntity;
        OtherEntityId = otherEntity.Id;
        RelatedEntities = new();
    }

    #endregion

    #region Properties

    public string Name { get; private set; }
    public Guid OtherEntityId { get; private set; }
    
    // Navigation Properties
    public virtual OtherEntity OtherEntity { get; private set; }
    public virtual List<RelatedEntity> RelatedEntities { get; private set; }

    #endregion

    #region Factory Methods

    public static [Feature] Create(Create[Feature]Dto dto)
    {
        // Validações
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Nome é obrigatório.");
            
        if (dto.OtherEntity == null)
            throw new ArgumentNullException(nameof(dto.OtherEntity));
        
        return new [Feature](dto.Name, dto.OtherEntity);
    }

    #endregion

    #region Methods

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório.");
            
        Name = name;
        UpdateTracker();
    }

    #endregion
}
```

### Regras para Entidades

- Herdar de `Entity` (que contém Id, CreatedAt, UpdatedAt)
- Implementar `IAggregateRoot` se for raiz de agregado
- **Construtores privados**
- **Properties com `private set`**
- **Navigation properties `virtual`** (para lazy loading)
- **Factory method `Create()` estático e público**
- Validações no método `Create()` e em métodos de atualização

---

## 📦 DTOs e Records

### DTOs de Domínio

```csharp
// ASO.Domain/[Subdominio]/Dtos/
public record Create[Feature]Dto(
    string Name,
    OtherEntity OtherEntity,
    List<RelatedEntity> RelatedEntities
);
```

### Commands

```csharp
// ASO.Application/UseCases/[Feature]/Create/
public record Create[Feature]Command
{
    public required string Name { get; init; }
    public required Guid OtherEntityId { get; init; }
    public required List<Guid> RelatedEntityIds { get; init; }
}
```

### Responses

```csharp
// ASO.Application/UseCases/[Feature]/Create/
public record Create[Feature]Response
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required DateTime CreatedAt { get; init; }
}
```

### Inputs (API)

```csharp
// ASO.Api/Inputs/
public record Create[Feature]Input
{
    public required string Name { get; init; }
    public required Guid OtherEntityId { get; init; }
    public required List<Guid> RelatedEntityIds { get; init; }
}
```

---

## 🗄️ Database e EF Core

### DbContext

```csharp
public class AppDbContext : DbContext
{
    public DbSet<[Feature]> [Features] { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

### Entity Configuration

```csharp
// ASO.Infra/Database/Configurations/
public class [Feature]Configuration : IEntityTypeConfiguration<[Feature]>
{
    public void Configure(EntityTypeBuilder<[Feature]> builder)
    {
        builder.ToTable("[Features]"); // Plural
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.HasOne(x => x.OtherEntity)
            .WithMany()
            .HasForeignKey(x => x.OtherEntityId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasMany(x => x.RelatedEntities)
            .WithOne()
            .HasForeignKey(x => x.[Feature]Id);
    }
}
```

### Seeds

```csharp
// ASO.Infra/Database/Seeds/
public static class [Feature]Seed
{
    public static void Seed(AppDbContext context)
    {
        if (context.[Features].Any())
            return;
            
        var entities = new List<[Feature]>
        {
            // Dados iniciais
        };
        
        context.[Features].AddRange(entities);
        context.SaveChanges();
    }
}
```

**Registrar seed no `Program.cs`:**
```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    [Feature]Seed.Seed(context);
}
```

---

## 🔌 External Services

### Interface no Domain

```csharp
// ASO.Domain/[Subdominio]/Abstractions/ExternalServices/
public interface IExternalService
{
    Task<ExternalResponse> DoSomethingAsync(ExternalRequest request);
}
```

### Implementação na Infra

```csharp
// ASO.Infra/ExternalServices/
public class ExternalService(HttpClient httpClient) : IExternalService
{
    private readonly HttpClient _httpClient = httpClient;
    
    public async Task<ExternalResponse> DoSomethingAsync(ExternalRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("endpoint", request);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<ExternalResponse>();
        return result ?? throw new Exception("Resposta inválida.");
    }
}
```

### Registro com HttpClient

```csharp
// Program.cs
builder.Services.AddHttpClient<IExternalService, ExternalService>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config["ExternalServices:ServiceName:BaseUrl"]
                  ?? throw new ArgumentNullException("Base URL não configurada.");
    var apiKey = config["ExternalServices:ServiceName:Key"]
                 ?? throw new ArgumentNullException("API Key não configurada.");

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
});
```

---

## 🔐 Autenticação e Autorização

### Configuração JWT (Keycloak)

```csharp
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
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
```

### Uso em Controllers

```csharp
[HttpPost]
[Authorize] // Requer autenticação
public async Task<IActionResult> Create([FromBody] CreateInput input)
{
    // ...
}

[HttpGet]
[AllowAnonymous] // Permite acesso sem autenticação
public async Task<IActionResult> GetAll()
{
    // ...
}
```

---

## 🛡️ Middleware e Tratamento de Erros

### Middleware de Exceções

```csharp
// ASO.Api/Middleware/ExceptionHandlingMiddleware.cs
public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var response = new { message = exception.Message };
        return context.Response.WriteAsJsonAsync(response);
    }
}
```

### Registro do Middleware

```csharp
// Program.cs
app.UseExceptionHandling(); // Extension method customizado
// ou
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

---

## 📄 Paginação

### Filter Base

```csharp
// ASO.Application/Pagination/
public abstract record PaginatedQueryBase
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? OrderBy { get; init; }
    public bool Ascending { get; init; } = false;
}
```

### Resultado Paginado

```csharp
public record PaginatedResult<T>
{
    public required List<T> Items { get; init; }
    public required int TotalCount { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
```

### Query Builder para Paginação

```csharp
// ASO.Application/Builders/
public class GetPaginated[Feature]QueryBuilder 
    : QueryBuilderBase<GetPaginated[Feature]QueryBuilder, [Feature], GetAll[Feature]Filter>
{
    public GetPaginated[Feature]QueryBuilder(
        IQueryable<[Feature]> query, 
        GetAll[Feature]Filter filter) : base(query, filter)
    {
    }
    
    public override GetPaginated[Feature]QueryBuilder SetFilter()
    {
        if (!string.IsNullOrWhiteSpace(Filter.Name))
            Query = Query.Where(x => x.Name.Contains(Filter.Name));
            
        return this;
    }
    
    public override GetPaginated[Feature]QueryBuilder SetOrderBy()
    {
        Query = Filter.OrderBy?.ToLower() switch
        {
            "name" => Filter.Ascending 
                ? Query.OrderBy(x => x.Name) 
                : Query.OrderByDescending(x => x.Name),
            _ => Query.OrderByDescending(x => x.Tracker.UpdatedAtUtc)
        };
        
        return this;
    }
}
```

---

## 🧪 Testes

### Testes de Entidades (Domain Tests)

```csharp
// ASO.Domain.Tests/Entities/
public class [Feature]Tests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateEntity()
    {
        // Arrange
        var dto = new Create[Feature]Dto(
            Name: "Test",
            OtherEntity: MockOtherEntity()
        );
        
        // Act
        var entity = [Feature].Create(dto);
        
        // Assert
        Assert.NotNull(entity);
        Assert.Equal("Test", entity.Name);
    }
    
    [Fact]
    public void Create_WithInvalidName_ShouldThrowException()
    {
        // Arrange
        var dto = new Create[Feature]Dto(
            Name: "",
            OtherEntity: MockOtherEntity()
        );
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => [Feature].Create(dto));
    }
}
```

---

## 📝 Checklist para Nova Feature

Ao criar uma nova feature, seguir esta ordem:

### 1. **Domain (ASO.Domain)**
- [ ] Criar entidade em `ASO.Domain/[Subdominio]/Entities/`
- [ ] Criar DTOs em `ASO.Domain/[Subdominio]/Dtos/`
- [ ] Criar Value Objects se necessário
- [ ] Criar exceções customizadas se necessário
- [ ] Criar interface do repositório em `ASO.Domain/[Subdominio]/Abstractions/Repositories/`
- [ ] Criar interface do QueryService em `ASO.Domain/[Subdominio]/Abstractions/QueriesServices/`

### 2. **Infrastructure (ASO.Infra)**
- [ ] Implementar repositório em `ASO.Infra/Repositories/`
- [ ] Implementar QueryService em `ASO.Infra/QueriesServices/`
- [ ] Criar Entity Configuration em `ASO.Infra/Database/Configurations/`
- [ ] Adicionar DbSet no `AppDbContext`
- [ ] Criar seed se necessário em `ASO.Infra/Database/Seeds/`
- [ ] Gerar migration: `dotnet ef migrations add Add[Feature]`

### 3. **Application (ASO.Application)**
- [ ] Criar estrutura de pastas: `UseCases/[Feature]/Create/` e `GetAll/`
- [ ] Criar Command/Response para Create
- [ ] Criar Filter/Response para GetAll
- [ ] Criar Handler para Create
- [ ] Criar Handler para GetAll
- [ ] Criar interface do handler em `Abstractions/UseCase/[Feature]/`
- [ ] Criar mappers em `Mappers/[Feature]Mapper.cs`
- [ ] Criar Query Builder se necessário

### 4. **API (ASO.Api)**
- [ ] Criar Input DTOs em `Inputs/`
- [ ] Criar Input Mapper em `Inputs/Mappers/`
- [ ] Criar Controller em `Controllers/`
- [ ] Registrar dependências no `Program.cs`

### 5. **Tests (ASO.Domain.Tests)**
- [ ] Criar testes para entidade
- [ ] Criar testes para Value Objects

### 6. **Validação Final**
- [ ] Executar migrations: `dotnet ef database update`
- [ ] Testar endpoints via Swagger
- [ ] Verificar se seeds estão funcionando
- [ ] Validar tratamento de erros

---

## 🛠️ Comandos Úteis

### Entity Framework

```bash
# Criar migration
dotnet ef migrations add [NomeDaMigration] --project ASO.Infra --startup-project ASO.Api

# Aplicar migrations
dotnet ef database update --project ASO.Infra --startup-project ASO.Api

# Remover última migration (se não aplicada)
dotnet ef migrations remove --project ASO.Infra --startup-project ASO.Api

# Gerar script SQL
dotnet ef migrations script --project ASO.Infra --startup-project ASO.Api
```

### Build e Run

```bash
# Restore packages
dotnet restore

# Build
dotnet build

# Run API
dotnet run --project ASO.Api

# Run tests
dotnet test

# Watch mode (hot reload)
dotnet watch run --project ASO.Api
```

---

## 🌐 Configurações Importantes

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=aso_db;Username=postgres;Password=postgres"
  },
  "ExternalServices": {
    "Gemini_API": {
      "BaseUrl": "https://generativelanguage.googleapis.com/v1beta/models/",
      "Key": "SUA_API_KEY_AQUI"
    }
  }
}
```

### CORS

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ...

app.UseCors("AllowAngular");
```

---

## 📚 Tecnologias e Dependências

- **.NET 9.0**
- **C# 12** (Primary Constructors, Records)
- **Entity Framework Core** (PostgreSQL)
- **Npgsql** (Provider PostgreSQL)
- **Keycloak** (Autenticação JWT)
- **Swagger/OpenAPI**
- **Gemini API** (IA generativa)

---

## ⚠️ Regras Importantes

1. **SEMPRE responder em PT-BR**
2. **NÃO usar `var` em declarações de injeção de dependência**
3. **Primary Constructors obrigatório** (C# 12)
4. **Factory Pattern** para criação de entidades
5. **Construtores privados** em entidades
6. **Properties com `private set`**
7. **Validações dentro do domínio**
8. **Mappers estáticos** (extension methods)
9. **Async/Await** em operações I/O
10. **Repository para escrita, QueryService para leitura**
11. **Uma pasta por Use Case** com Command/Handler/Response
12. **Migrations com nomes descritivos**
13. **Seeds para dados iniciais**
14. **Middleware para tratamento de exceções**
15. **Controllers finos** - apenas delegação

---

## 🎯 Exemplo Completo: Criar Feature "Weapon"

<details>
<summary>Expandir exemplo completo</summary>

### 1. Domain - Entity

```csharp
// ASO.Domain/Game/Entities/Weapon.cs
public class Weapon : Entity, IAggregateRoot
{
    private Weapon()
    {
        Name = null!;
        Description = null!;
    }

    private Weapon(string name, string description, int damage, WeaponType type)
    {
        Name = name;
        Description = description;
        Damage = damage;
        Type = type;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Damage { get; private set; }
    public WeaponType Type { get; private set; }

    public static Weapon Create(CreateWeaponDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Nome é obrigatório.");
            
        if (dto.Damage <= 0)
            throw new ArgumentException("Dano deve ser maior que zero.");
        
        return new Weapon(dto.Name, dto.Description, dto.Damage, dto.Type);
    }
}
```

### 2. Domain - DTO

```csharp
// ASO.Domain/Game/Dtos/Weapon/CreateWeaponDto.cs
public record CreateWeaponDto(
    string Name,
    string Description,
    int Damage,
    WeaponType Type
);
```

### 3. Domain - Repository Interface

```csharp
// ASO.Domain/Game/Abstractions/Repositories/IWeaponRepository.cs
public interface IWeaponRepository : IRepository<Weapon>
{
    Task<Weapon> Create(Weapon weapon);
    IQueryable<Weapon> GetAll();
}
```

### 4. Infrastructure - Repository

```csharp
// ASO.Infra/Repositories/WeaponRepository.cs
public class WeaponRepository(AppDbContext context) : IWeaponRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<Weapon> Create(Weapon weapon)
    {
        await _context.Weapons.AddAsync(weapon);
        await _context.SaveChangesAsync();
        return weapon;
    }
    
    public IQueryable<Weapon> GetAll()
    {
        return _context.Weapons.AsQueryable();
    }
}
```

### 5. Application - Command

```csharp
// ASO.Application/UseCases/Weapons/Create/CreateWeaponCommand.cs
public record CreateWeaponCommand
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Damage { get; init; }
    public required WeaponType Type { get; init; }
}
```

### 6. Application - Handler

```csharp
// ASO.Application/UseCases/Weapons/Create/CreateWeaponHandler.cs
public sealed class CreateWeaponHandler(
    IWeaponRepository repository) : ICreateWeaponHandler
{
    private readonly IWeaponRepository _repository = repository;
    
    public async Task<CreateWeaponResponse> HandleAsync(CreateWeaponCommand command)
    {
        var dto = command.ToDto();
        var weapon = Weapon.Create(dto);
        await _repository.Create(weapon);
        return weapon.ToResponse();
    }
}
```

### 7. API - Controller

```csharp
// ASO.Api/Controllers/WeaponController.cs
[ApiController]
[Route("api/[controller]")]
public class WeaponController(
    ICreateWeaponHandler createWeaponHandler) : ControllerBase
{
    private readonly ICreateWeaponHandler _createWeaponHandler = createWeaponHandler;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateWeaponInput input)
    {
        var command = input.ToCommand();
        var response = await _createWeaponHandler.HandleAsync(command);
        return Ok(response);
    }
}
```

### 8. Program.cs - Registro

```csharp
// ASO.Api/Program.cs
builder.Services.AddScoped<IWeaponRepository, WeaponRepository>();
builder.Services.AddScoped<ICreateWeaponHandler, CreateWeaponHandler>();
```

</details>

---

## 📞 Contato e Documentação

Para mais detalhes sobre a arquitetura, consulte:
- `docs/RELATORIO_ARQUITETURA.md`
- `docs/SUGESTOES_MELHORIA.md`

---

**Última atualização:** 05/11/2025

