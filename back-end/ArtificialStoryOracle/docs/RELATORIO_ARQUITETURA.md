# Relatório de Análise de Arquitetura - Artificial Story Oracle (ASO)

**Data de Análise:** 25/10/2025  
**Projeto:** Artificial Story Oracle - Sistema de Gerenciamento de RPG com IA  
**Tecnologia:** .NET 9.0 / C#  

---

## 📋 Índice

1. [Visão Geral](#visão-geral)
2. [Arquitetura Geral](#arquitetura-geral)
3. [Padrões de Projeto](#padrões-de-projeto)
4. [Estrutura de Camadas](#estrutura-de-camadas)
5. [Convenções e Regras Internas](#convenções-e-regras-internas)
6. [Funcionalidades Padrão](#funcionalidades-padrão)
7. [Tecnologias e Dependências](#tecnologias-e-dependências)
8. [Diagrama de Arquitetura](#diagrama-de-arquitetura)

---

## 🎯 Visão Geral

O **Artificial Story Oracle (ASO)** é um sistema backend desenvolvido em .NET 9.0 que gerencia elementos de RPG (Role-Playing Game) e integra inteligência artificial (Gemini API) para geração de conteúdo narrativo. O sistema segue uma arquitetura em camadas bem definida com forte ênfase em princípios de Domain-Driven Design (DDD) e Clean Architecture.

### Propósito do Sistema
- Gerenciamento de personagens, classes, habilidades e ancestralidades para jogos de RPG
- Geração automática de narrativas e backstories usando IA
- Suporte a múltiplos jogadores e campanhas
- API RESTful com autenticação JWT via Keycloak

---

## 🏗️ Arquitetura Geral

### Tipo de Arquitetura
**Clean Architecture + Domain-Driven Design (DDD)**

### Estrutura de Projetos

```
ArtificialStoryOracle/
│
├── ASO.Api/                    # Camada de Apresentação (Controllers, Middleware)
├── ASO.Application/            # Camada de Aplicação (Use Cases, Handlers)
├── ASO.Domain/                 # Camada de Domínio (Entities, Value Objects, Rules)
├── ASO.Infra/                  # Camada de Infraestrutura (Database, External Services)
└── ASO.Domain.Tests/           # Testes Unitários do Domínio
```

### Fluxo de Dados (Data Flow)

```
[Client/Frontend]
      ↓
[ASO.Api - Controllers] → Recebe Input/DTOs
      ↓
[Input Mappers] → Converte para Commands/Queries
      ↓
[Application - Handlers] → Processa regras de aplicação
      ↓
[Domain - Entities/Services] → Aplica regras de negócio
      ↓
[Infrastructure - Repositories] → Persiste dados
      ↓
[Database - PostgreSQL]
```

---

## 🎨 Padrões de Projeto

### 1. **CQRS (Command Query Responsibility Segregation)**
   - **Commands:** Operações de escrita (Create, Update, Delete)
   - **Queries:** Operações de leitura (GetAll, GetById)
   - Handlers separados para cada responsabilidade

**Exemplo:**
```csharp
// Command
public interface ICommandHandler<in TRequest, out TResponse>
    where TRequest : ICommand
    where TResponse : IResponse
{
    TResponse Handle(TRequest command);
}

// Query
public interface IQueryHandler<in TRequest, TResponse>
    where TRequest : IQuery
    where TResponse : IResponse
{
    Task<TResponse> Handle(TRequest request);
}
```

### 2. **Repository Pattern**
   - Abstração do acesso a dados
   - Interface no Domain, implementação na Infrastructure
   - Uso de IQueryable para composição de queries

**Exemplo:**
```csharp
// Domain
public interface ICharacterRepository : IRepository<Character>
{
    Task<Character> Create(Character character);
    IQueryable<Character> GetAll();
}

// Infrastructure
public class CharacterRepository(AppDbContext context) : ICharacterRepository
{
    // Implementação...
}
```

### 3. **Factory Pattern**
   - Criação de entidades através de métodos estáticos `Create()`
   - Validação centralizada na criação
   - Construtor privado para forçar uso do Factory

**Exemplo:**
```csharp
public class Character : Entity
{
    private Character() { } // Construtor privado
    
    public static Character Create(CreateCharacterDto dto)
    {
        // Validações
        if (dto.Skills == null || dto.Skills.Count == 0)
            throw new ArgumentException("Expertises cannot be null or empty.");
            
        return new Character(dto.Name, dto.Ancestry, ...);
    }
}
```

### 4. **Builder Pattern**
   - Construção de queries complexas
   - Separação de responsabilidades na composição de filtros

**Exemplo:**
```csharp
public abstract class QueryBuilderBase<TBuilder, TEntity, TFilter>
{
    protected IQueryable<TEntity> Query { get; set; }
    
    public virtual TBuilder SetOrderBy()
    {
        Query = Query.OrderByDescending(i => i.Tracker.UpdatedAtUtc);
        return _instance;
    }
}
```

### 5. **Dependency Injection (DI)**
   - Injeção via construtor (Constructor Injection)
   - Configuração centralizada no `Program.cs`
   - Scoped lifetime para serviços transacionais

### 6. **Mapper Pattern**
   - Mappers estáticos para conversões entre camadas
   - Separação clara entre DTOs, Commands, Entities e Responses

**Exemplo:**
```csharp
public static class CharacterMapper
{
    public static CreateCharacterResponse ToCreateCharacterResponse(this Character entity)
    {
        return new CreateCharacterResponse { ... };
    }
}
```

### 7. **Middleware Pattern**
   - Tratamento centralizado de exceções
   - Pipeline de requests HTTP
   - Logging e error handling

### 8. **Value Object Pattern (DDD)**
   - Objetos imutáveis (records)
   - Validação interna
   - Sem identidade própria

---

## 📚 Estrutura de Camadas

### **ASO.Api (Presentation Layer)**

**Responsabilidades:**
- Receber requisições HTTP
- Validar inputs básicos
- Orquestrar chamadas aos handlers
- Retornar responses formatados
- Middleware de exceções

**Componentes:**
- **Controllers:** Endpoints REST
- **Inputs:** DTOs de entrada
- **Input Mappers:** Conversão Input → Command/Query
- **Middleware:** ExceptionHandlingMiddleware

**Regras:**
- Controllers devem ser finos, apenas delegando para handlers
- Uso de `[ApiController]` e `[Route("api/[controller]")]`
- Autenticação JWT configurada (Keycloak)
- CORS habilitado para Angular (http://localhost:4200)

**Exemplo de Controller:**
```csharp
[ApiController]
[Route("api/[controller]")]
public class CharacterController(
    ICreateCharacterHandler createCharacterHandler,
    IGetAllCharactersHandler getAllCharactersHandler) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterInput input)
    {
        var command = input.ToCommand();
        var player = await _createCharacterHandler.HandleAsync(command);
        return Ok(player);
    }
}
```

---

### **ASO.Application (Application Layer)**

**Responsabilidades:**
- Implementar casos de uso (Use Cases)
- Orquestrar fluxos de negócio
- Coordenar chamadas entre Domain e Infrastructure
- Paginação e ordenação
- Mapeamento entre camadas

**Componentes:**
- **UseCases:** Handlers para Commands e Queries
- **Abstractions:** Interfaces dos handlers
- **Mappers:** Conversões Entity → Response
- **Builders:** Construção de queries complexas
- **Pagination:** Suporte a resultados paginados
- **Extensions:** Extensões para IQueryable

**Estrutura de Use Case:**
```
UseCases/
└── Characters/
    ├── Create/
    │   ├── CreateCharacterCommand.cs
    │   ├── CreateCharacterHandler.cs
    │   └── CreateCharacterResponse.cs
    └── GetAll/
        ├── GetAllCharactersFilter.cs
        ├── GetAllCharactersHandler.cs
        └── GetAllCharactersResponse.cs
```

**Regras:**
- Um handler por caso de uso
- Handlers assíncronos sempre que possível
- Validação de regras de aplicação (não de domínio)
- Uso de Query Services para leitura
- Uso de Repositories para escrita

---

### **ASO.Domain (Domain Layer)**

**Responsabilidades:**
- Definir entidades e agregados
- Implementar regras de negócio
- Value Objects imutáveis
- Eventos de domínio
- Exceções de domínio

**Componentes:**
- **Entities:** Entidades com identidade
- **Value Objects:** Objetos sem identidade (records)
- **Aggregates:** Raízes de agregados (IAggregateRoot)
- **Events:** Eventos de domínio (IDomainEvent)
- **Exceptions:** Exceções customizadas
- **Abstractions:** Interfaces de repositórios e serviços

**Subdomínios:**
```
ASO.Domain/
├── Game/           # Domínio de jogo (Character, Player, Class, etc.)
├── AI/             # Domínio de IA (GeneratedAIContent)
├── Identity/       # Domínio de identidade (PlayerUser)
└── Shared/         # Compartilhado entre domínios
```

**Padrões da Camada de Domínio:**

#### Entidades Base
```csharp
public abstract class Entity : IEquatable<Guid>
{
    public Guid Id { get; } = Guid.NewGuid();
    public Tracker Tracker { get; } = Tracker.Create();
    
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyCollection<IDomainEvent> Events => _events;
    
    public void RaiseEvent(IDomainEvent @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
}
```

#### Value Objects
```csharp
public abstract record ValueObject;

// Exemplo concreto
public sealed record Email : ValueObject
{
    private const string Pattern = @"^\w+([\-+.'']\w+)*@\w+([\-\.]\w+)*\.\w+([\-\.]\w+)*$";
    
    private Email(string address) { Address = address; }
    
    public static Email Create(string address)
    {
        // Validações
        if (!EmailRegex().IsMatch(address))
            throw new InvalidEmailException("invalid email address");
        
        return new Email(address);
    }
    
    public string Address { get; }
}
```

#### Tracker (Auditoria)
```csharp
public sealed record Tracker : ValueObject
{
    public DateTime CreatedAtUtc { get; }
    public DateTime UpdatedAtUtc { get; }
    
    public static Tracker Create() 
        => new(DateTime.UtcNow, DateTime.UtcNow);
}
```

**Regras da Camada de Domínio:**
1. Entidades sempre herdam de `Entity`
2. Agregados implementam `IAggregateRoot`
3. Value Objects são `record` e herdam de `ValueObject`
4. Construtor privado + Factory Method `Create()`
5. Validações dentro do Factory Method
6. Lançar exceções de domínio customizadas
7. Toda entidade tem `Tracker` para auditoria
8. Eventos de domínio implementam `IDomainEvent` (MediatR)

---

### **ASO.Infra (Infrastructure Layer)**

**Responsabilidades:**
- Persistência de dados (Entity Framework Core)
- Integração com serviços externos (Gemini API)
- Implementação de repositórios
- Query Services (leitura otimizada)
- Migrations e Seeds

**Componentes:**
- **Database:** DbContext, Mappings, Seeds
- **Repositories:** Implementações de IRepository
- **QueriesServices:** Serviços de consulta
- **ExternalServices:** Integrações externas (API Gemini)
- **Migrations:** Versionamento do banco

**Configuração do DbContext:**
```csharp
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Ancestry> Ancestries { get; set; }
    // ...
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```

**Entity Type Configurations:**
```csharp
public class CharactersMap : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("characters");
        builder.HasKey(c => c.Id);
        
        // Value Objects como OwnsOne
        builder.OwnsOne(a => a.Modifiers, modifiers =>
        {
            modifiers.Property(m => m.ModStrength).HasColumnName("mod_strength");
            // ...
        });
        
        // Tracker automático
        builder.ConfigureTracker();
    }
}
```

**Query Services vs Repositories:**
- **Repositories:** Escrita (Create, Update, Delete) - retornam entidades
- **Query Services:** Leitura otimizada - podem retornar DTOs ou projeções

---

## 📐 Convenções e Regras Internas

### **Nomenclatura**

#### Projetos
- Padrão: `ASO.{Camada}`
- Exemplos: `ASO.Api`, `ASO.Domain`, `ASO.Application`, `ASO.Infra`

#### Namespaces
- Seguem estrutura de pastas
- Exemplo: `ASO.Application.UseCases.Characters.Create`

#### Classes e Interfaces
- **Interfaces:** Prefixo `I` + nome descritivo
- **Handlers:** Sufixo `Handler` (ex: `CreateCharacterHandler`)
- **Commands:** Sufixo `Command` (ex: `CreateCharacterCommand`)
- **Queries:** Sufixo `Query` ou `Filter`
- **Responses:** Sufixo `Response`
- **Mappers:** Sufixo `Mapper` (classe estática)
- **Repositories:** Sufixo `Repository`
- **Services:** Sufixo `Service`

#### Métodos
- **Factory Methods:** `Create()`
- **Handlers:** `Handle()` ou `HandleAsync()`
- **Mappers:** `To{Destino}()` (ex: `ToCommand()`, `ToResponse()`)

### **Estrutura de Use Cases**

Cada use case deve ter sua própria pasta com:
```
{UseCase}/
├── {UseCase}Command.cs      # ou Query/Filter
├── {UseCase}Handler.cs
└── {UseCase}Response.cs
```

### **Exceções**

#### Hierarquia
```
Exception
└── DomainException (abstract)
    ├── ValidationException
    ├── EntityNotFoundException
    ├── BusinessRuleException
    ├── ConflictException
    ├── UnauthorizedException
    └── ForbiddenException
```

#### Uso
- Exceções de domínio herdam de `DomainException`
- Contém `HttpStatusCode` para mapeamento HTTP
- Tratadas centralizadamente no `ExceptionHandlingMiddleware`

```csharp
public abstract class DomainException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    
    protected DomainException(HttpStatusCode statusCode, string? message = null) 
        : base(message)
    {
        StatusCode = statusCode;
    }
}
```

### **Paginação**

Padrão de paginação consistente:
```csharp
public record PaginatedQueryBase
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class PaginatedResult<T>
{
    public IList<T> Results { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
    public bool HasNextPage => CurrentPage < PageCount;
    public bool HasPreviousPage => CurrentPage > 1;
}
```

### **Ordenação**

Extension method padrão:
```csharp
public static IOrderedQueryable<T> OrderByAscDesc<T, TKey>(
    this IQueryable<T> queryable,
    Expression<Func<T, TKey>> expression,
    string orderByType = OrderType.Asc)
{
    return orderByType.Equals(OrderType.Asc) 
        ? queryable.OrderBy(expression)
        : queryable.OrderByDescending(expression);
}
```

Ordenação padrão: `UpdatedAtUtc DESC, Id ASC`

---

## ⚙️ Funcionalidades Padrão

### **1. Gerenciamento de Personagens**

**Endpoints:**
- `POST /api/character` - Criar personagem
- `GET /api/character` - Listar personagens (paginado)

**Entidades Relacionadas:**
- Character (Agregado Principal)
- Ancestry (Ancestralidade)
- Class (Classe)
- Skill (Habilidade)
- Image (Imagem)
- AttributeModifiers (Value Object)

**Fluxo de Criação:**
1. Controller recebe `CreateCharacterInput`
2. Mapper converte para `CreateCharacterCommand`
3. Handler valida e busca entidades relacionadas (Ancestry, Class, Skills, Image)
4. Domain cria Character via Factory Method
5. Repository persiste
6. Retorna `CreateCharacterResponse`

### **2. Sistema de Ancestralidades, Classes e Habilidades**

**Características:**
- Dados seedados no startup
- Consultas via Query Services (otimizado para leitura)
- Relacionamentos many-to-many (Character ↔ Skills, Character ↔ Classes)

**Seeds:**
- `AncestrySeed.Seed(context)`
- `ClassSeed.Seed(context)`
- `SkillSeed.Seed(context)`
- `ImageSeed.Seed(context)`

### **3. Geração de Conteúdo com IA (Oracle)**

**Endpoints:**
- `POST /api/oracle/character-backstory` - Gerar backstory
- `GET /api/oracle/character-names` - Gerar nomes
- `GET /api/oracle/campaign-backstory` - Gerar história de campanha

**Integração:**
- Serviço: `GeminiApiService`
- API: Google Gemini 2.0 Flash
- Persistência: `GeneratedAIContent` (histórico de geração)

**Fluxo:**
1. Recebe dados do personagem
2. Monta prompt contextualizado
3. Chama API Gemini
4. Persiste conteúdo gerado
5. Retorna resposta

### **4. Autenticação e Autorização**

**Configuração:**
- Provider: Keycloak
- Tipo: JWT Bearer Token
- Authority: `http://localhost:8080/realms/teste`
- Audience: `meu-client`

**Uso:**
```csharp
[Authorize] // Requer autenticação
[AllowAnonymous] // Permite acesso sem autenticação
```

### **5. Sistema de Paginação e Filtros**

**Query Builders:**
- Composição fluente de queries
- Filtros dinâmicos
- Ordenação configurável
- Paginação automática

**Exemplo:**
```csharp
var query = GetPaginatedCharactersQueryBuilder
    .CreateBuilder(_characterRepository)
    .SetFilter(filter)
    .FilterByName()
    .SetOrderBy()
    .BuildQuery();

return await query.GetPaginatedAsync(filter.Page, filter.PageSize);
```

### **6. Auditoria Automática**

**Tracker em todas as entidades:**
- `CreatedAtUtc` - Data de criação (UTC)
- `UpdatedAtUtc` - Data de atualização (UTC)
- Configurado via `EntityTypeBuilderExtensions.ConfigureTracker()`

### **7. Migrations Automáticas**

No startup do `Program.cs`:
```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Aplica migrations pendentes
    AncestrySeed.Seed(context); // Seeds
}
```

---

## 🔧 Tecnologias e Dependências

### **Framework e Linguagem**
- **.NET 9.0** - Framework principal
- **C# 13** - Linguagem
- **ImplicitUsings** - Habilitado
- **Nullable** - Habilitado

### **Pacotes Principais**

#### API e Web
- `Microsoft.AspNetCore` (built-in .NET 9)
- `Swashbuckle.AspNetCore` (8.1.1) - Swagger/OpenAPI

#### Autenticação
- `Microsoft.AspNetCore.Authentication.JwtBearer` (8.0.15)
- `Microsoft.IdentityModel.JsonWebTokens` (8.9.0)
- `Microsoft.IdentityModel.Tokens` (8.9.0)

#### Banco de Dados
- `Microsoft.EntityFrameworkCore` (9.0.5)
- `Microsoft.EntityFrameworkCore.Design` (9.0.5)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.5)
- `Npgsql.EntityFrameworkCore.PostgreSQL` (9.0.4) - PostgreSQL

#### Validação e Mediação
- `FluentValidation` (12.0.0) - Validações fluentes
- `MediatR.Contracts` (2.0.1) - Eventos de domínio

#### Testes
- `xunit` (2.9.2)
- `xunit.runner.visualstudio` (2.8.2)
- `coverlet.collector` (6.0.2)
- `Microsoft.NET.Test.Sdk` (17.11.1)

### **Gerenciamento Centralizado de Pacotes**

`Directory.Packages.props`:
```xml
<PropertyGroup>
  <ManagePackageVersionsCentrally>True</ManagePackageVersionsCentrally>
</PropertyGroup>
```

Versões definidas centralmente, referenciadas sem versão nos `.csproj`.

### **Banco de Dados**

**PostgreSQL**
- Host: localhost
- Port: 5433
- Database: aso
- Connection String em `appsettings.json`

**Convenções de Nomes:**
- Tabelas: snake_case (ex: `characters`, `characters_expertises`)
- Colunas: snake_case (ex: `created_at_utc`, `mod_strength`)

---

## 📊 Diagrama de Arquitetura

### **Arquitetura em Camadas**

```
┌─────────────────────────────────────────────────────────────┐
│                     CLIENT / FRONTEND                        │
│                   (Angular - Port 4200)                      │
└──────────────────────────┬──────────────────────────────────┘
                           │ HTTP/REST + JWT
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                      ASO.API LAYER                           │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │ Controllers  │  │   Inputs     │  │  Middleware  │      │
│  │              │  │   Mappers    │  │              │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
└──────────────────────────┬──────────────────────────────────┘
                           │
                           ↓
┌─────────────────────────────────────────────────────────────┐
│                 ASO.APPLICATION LAYER                        │
│  ┌──────────────────────────────────────────────────────┐   │
│  │              USE CASES (Handlers)                    │   │
│  │  • Create Character  • Get All Characters            │   │
│  │  • Generate Backstory • Get Classes/Skills           │   │
│  └──────────────────────────────────────────────────────┘   │
│  ┌────────────┐  ┌─────────────┐  ┌─────────────────┐     │
│  │  Mappers   │  │   Builders  │  │   Pagination    │     │
│  └────────────┘  └─────────────┘  └─────────────────┘     │
└──────────────┬──────────────────────────┬──────────────────┘
               │                          │
               ↓                          ↓
┌──────────────────────────┐  ┌────────────────────────────┐
│   ASO.DOMAIN LAYER       │  │  ASO.INFRA LAYER           │
│  ┌────────────────────┐  │  │  ┌──────────────────────┐  │
│  │    Entities        │  │  │  │   Repositories       │  │
│  │  • Character       │←─┼──┼──┤   • Character Repo   │  │
│  │  • Player          │  │  │  │   • AI Content Repo  │  │
│  │  • Ancestry        │  │  │  └──────────────────────┘  │
│  │  • Class/Skill     │  │  │  ┌──────────────────────┐  │
│  └────────────────────┘  │  │  │   Query Services     │  │
│  ┌────────────────────┐  │  │  │   • Ancestry Query   │  │
│  │  Value Objects     │  │  │  │   • Class Query      │  │
│  │  • Email           │  │  │  │   • Skill Query      │  │
│  │  • Name            │  │  │  └──────────────────────┘  │
│  │  • Tracker         │  │  │  ┌──────────────────────┐  │
│  │  • Modifiers       │  │  │  │  External Services   │  │
│  └────────────────────┘  │  │  │  • GeminiApiService  │  │
│  ┌────────────────────┐  │  │  └──────────┬───────────┘  │
│  │    Exceptions      │  │  │             │              │
│  │  • DomainException │  │  │  ┌──────────▼───────────┐  │
│  │  • Validation      │  │  │  │    DbContext         │  │
│  └────────────────────┘  │  │  │  • AppDbContext      │  │
│  ┌────────────────────┐  │  │  │  • Mappings          │  │
│  │  Events            │  │  │  │  • Seeds             │  │
│  │  • IDomainEvent    │  │  │  └──────────┬───────────┘  │
│  └────────────────────┘  │  │             │              │
│  ┌────────────────────┐  │  └─────────────┼──────────────┘
│  │  Abstractions      │  │                │
│  │  • IRepository     │──┼────────────────┘
│  │  • IQueryService   │  │                ↓
│  └────────────────────┘  │    ┌────────────────────────┐
└──────────────────────────┘    │  PostgreSQL Database    │
                                │  • characters           │
                                │  • ancestries           │
              ┌─────────────────┤  • classes              │
              │                 │  • skills               │
              │                 │  • images               │
              ↓                 │  • generated_ai_content │
  ┌───────────────────────┐    └────────────────────────┘
  │  External APIs        │
  │  • Gemini AI API      │
  │  • Keycloak (Auth)    │
  └───────────────────────┘
```

### **Fluxo de Uma Request (Exemplo: Criar Personagem)**

```
1. [Cliente] POST /api/character
   Body: CreateCharacterInput
   ↓
2. [Controller] CharacterController.CreateCharacter()
   ↓
3. [Mapper] CreateCharacterInput.ToCommand()
   → CreateCharacterCommand
   ↓
4. [Handler] CreateCharacterHandler.HandleAsync()
   ├─→ [Query Service] ancestryQueryService.GetById()
   ├─→ [Query Service] classQueryService.GetById()
   ├─→ [Query Service] skillQueryService.GetByIds()
   ├─→ [Query Service] imageQueryService.GetById()
   ↓
5. [Mapper] command.ToCreateCharacterDto()
   → CreateCharacterDto
   ↓
6. [Domain] Character.Create(dto)
   ├─→ Validações de negócio
   ├─→ Construção da entidade
   └─→ Eventos de domínio (se houver)
   ↓
7. [Repository] characterRepository.Create()
   ↓
8. [DbContext] SaveChangesAsync()
   ↓
9. [Database] INSERT INTO characters
   ↓
10. [Mapper] character.ToCreateCharacterResponse()
    → CreateCharacterResponse
    ↓
11. [Controller] Ok(response)
    ↓
12. [Cliente] Status 200 + JSON Response
```

---

## 🎯 Regras de Desenvolvimento (Guidelines)

### **Para Adicionar um Novo Use Case**

1. **Domain Layer:**
   - Criar/atualizar entidade se necessário
   - Definir interfaces de repositório/serviços em `Abstractions`
   - Criar DTOs em `Dtos` se necessário

2. **Application Layer:**
   - Criar pasta do use case: `UseCases/{Entity}/{Action}/`
   - Criar `{Action}Command.cs` ou `{Action}Query.cs`
   - Criar `{Action}Handler.cs` implementando interface
   - Criar `{Action}Response.cs`
   - Criar interface do handler em `Abstractions/UseCase/{Entity}/`
   - Adicionar mappers em `Mappers/`

3. **Infrastructure Layer:**
   - Implementar repositório/query service se necessário
   - Adicionar mapeamento EF Core em `Database/Mapping/`

4. **API Layer:**
   - Criar `{Entity}Input.cs` em `Inputs/`
   - Criar mapper em `Inputs/Mappers/`
   - Adicionar action no controller correspondente
   - Registrar handler no `Program.cs` (DI)

5. **Testes:**
   - Adicionar testes unitários em `ASO.Domain.Tests/`

### **Para Adicionar uma Nova Entidade**

1. Criar classe herdando de `Entity`
2. Implementar `IAggregateRoot` se for raiz
3. Construtor privado + Factory Method `Create()`
4. Adicionar validações no Factory
5. Definir propriedades como `get` only
6. Criar Value Objects para conceitos complexos
7. Adicionar `Tracker` (automático via Entity base)
8. Criar configuração EF Core em `Mapping/`
9. Adicionar `DbSet` no `AppDbContext`
10. Criar migration: `Add-Migration {Nome}`

### **Para Adicionar um Novo Value Object**

1. Criar `record` herdando de `ValueObject`
2. Construtor privado
3. Factory Method `Create()` com validações
4. Propriedades imutáveis (`init` ou sem setter)
5. Usar `OwnsOne` no mapeamento EF Core

### **Para Integrar um Serviço Externo**

1. Criar interface em `Domain/{Subdomain}/Abstractions/ExternalServices/`
2. Criar DTOs de request/response em `Domain/*/Dtos/ExternalServices/`
3. Implementar serviço em `Infra/ExternalServices/`
4. Configurar HttpClient no `Program.cs` se necessário
5. Registrar serviço no DI container

---

## 🔐 Segurança

### **Autenticação**
- JWT Bearer Tokens via Keycloak
- Validação de audience e issuer
- HTTPS desabilitado em dev (`RequireHttpsMetadata = false`)

### **CORS**
- Habilitado para `http://localhost:4200`
- Permite qualquer header e método

### **Secrets**
- User Secrets habilitado nos projetos API e Infra
- API Key do Gemini em appsettings ou secrets

### **Validações**
- Input validation na API layer
- Business rules validation na Domain layer
- Exceções customizadas com status codes apropriados

---

## 📝 Padrões de Código

### **Estilo C#**
- `ImplicitUsings` habilitado
- `Nullable` habilitado
- Primary constructors para DI (C# 12)
- Records para DTOs e Value Objects
- Expression body para membros simples
- File-scoped namespaces

### **Exemplo Completo de Um Use Case**

```csharp
// 1. Command (Application/UseCases/Characters/Create/)
public sealed record CreateCharacterCommand : ICommand
{
    public string Name { get; init; } = string.Empty;
    public Guid AncestryId { get; init; }
    public Guid ClasseId { get; init; }
    public List<Guid> SkillsIds { get; init; } = [];
    public AttributeModifiers Modifiers { get; init; } = null!;
    public string? Backstory { get; init; }
    public Guid ImageId { get; init; }
}

// 2. Handler Interface (Application/Abstractions/UseCase/Characters/)
public interface ICreateCharacterHandler 
    : ICommandHandlerAsync<CreateCharacterCommand, CreateCharacterResponse>;

// 3. Handler Implementation (Application/UseCases/Characters/Create/)
public sealed class CreateCharacterHandler(
    ICharacterRepository repository,
    IAncestryQueryService ancestryQueryService,
    IClassQueryService classQueryService,
    ISkillQueryService skillQueryService,
    IImageQueryService imageQueryService) : ICreateCharacterHandler
{
    public async Task<CreateCharacterResponse> HandleAsync(CreateCharacterCommand command)
    {
        var ancestry = await ancestryQueryService.GetById(command.AncestryId);
        var classes = await classQueryService.GetById(command.ClasseId);
        var expertises = await skillQueryService.GetByIds(command.SkillsIds);
        var image = await imageQueryService.GetById(command.ImageId);
        
        var dto = command.ToCreateCharacterDto(ancestry, classes, expertises, image);
        var character = Character.Create(dto);
        
        await repository.Create(character);
        
        return character.ToCreateCharacterResponse();
    }
}

// 4. Response (Application/UseCases/Characters/Create/)
public sealed record CreateCharacterResponse : IResponse
{
    public string Name { get; init; } = string.Empty;
    public string TypeCharacter { get; init; } = string.Empty;
    public int Level { get; init; }
}

// 5. Controller (Api/Controllers/)
[ApiController]
[Route("api/[controller]")]
public class CharacterController(
    ICreateCharacterHandler createCharacterHandler) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterInput input)
    {
        var command = input.ToCommand();
        var result = await createCharacterHandler.HandleAsync(command);
        return Ok(result);
    }
}

// 6. DI Registration (Api/Program.cs)
builder.Services.AddScoped<ICreateCharacterHandler, CreateCharacterHandler>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
```

---

## 🧪 Testes

### **Estrutura**
- Projeto: `ASO.Domain.Tests`
- Framework: xUnit
- Cobertura: Coverlet

### **Estratégia**
- Foco em testes unitários do domínio
- Testar regras de negócio
- Testar validações de Value Objects
- Testar Factory Methods

### **Exemplo de Teste**
```csharp
public class EmailTests
{
    [Fact]
    public void Create_WithValidEmail_ShouldReturnEmail()
    {
        // Arrange
        var validEmail = "test@example.com";
        
        // Act
        var email = Email.Create(validEmail);
        
        // Assert
        Assert.Equal(validEmail, email.Address);
    }
    
    [Fact]
    public void Create_WithInvalidEmail_ShouldThrowException()
    {
        // Arrange
        var invalidEmail = "invalid-email";
        
        // Act & Assert
        Assert.Throws<InvalidEmailException>(() => Email.Create(invalidEmail));
    }
}
```

---

## 📈 Métricas e Observabilidade

### **Logging**
- Configurado via `appsettings.json`
- Níveis: Information (default), Warning (AspNetCore)
- Logs de exceções no middleware

### **Error Handling**
- Middleware centralizado: `ExceptionHandlingMiddleware`
- Response padronizado:
```json
{
  "traceId": "...",
  "success": false,
  "error": {
    "message": "Error message",
    "statusCode": 400,
    "details": ["detail1", "detail2"],
    "stackTrace": "..." // apenas em Development
  }
}
```

### **Swagger/OpenAPI**
- Documentação automática da API
- Suporte a JWT authentication
- Endpoint: `/swagger` (em desenvolvimento)

---

## 🚀 Deployment e Configuração

### **Ambientes**
- Development: `appsettings.Development.json`
- Production: `appsettings.json`

### **Configurações Importantes**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5433;Database=aso;..."
  },
  "ExternalServices": {
    "Gemini_API": {
      "BaseUrl": "https://generativelanguage.googleapis.com/",
      "Key": "..." // Em user secrets ou variável de ambiente
    }
  }
}
```

### **Startup**
1. Aplica migrations pendentes
2. Executa seeds (Ancestry, Skill, Class, Image)
3. Inicia servidor

---

## 📚 Conceitos DDD Aplicados

### **Ubiquitous Language**
- Character, Player, Ancestry, Class, Skill
- Backstory, Campaign, Oracle
- Modifiers (Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma)

### **Bounded Contexts**
- Game Context (Character, Player, Class, Ancestry, Skill)
- AI Context (GeneratedAIContent, AI queries)
- Identity Context (PlayerUser, authentication)

### **Aggregates**
- Character (root)
- Player (root)
- GeneratedAIContent (root)

### **Value Objects**
- Email, Name, Nickname
- Tracker
- AttributeModifiers
- Statistics

### **Domain Events**
- Infraestrutura pronta (`IDomainEvent`, `MediatR.Contracts`)
- Suporte a eventos em Entity base
- Não implementados ainda (TODO no código)

### **Repositories**
- Interface no Domain
- Implementação na Infrastructure
- Trabalham com Aggregate Roots

---

## 🎓 Boas Práticas Observadas

✅ Separação clara de responsabilidades (SoC)  
✅ Inversão de dependência (DIP)  
✅ Princípio aberto/fechado (OCP)  
✅ Substituição de Liskov (LSP)  
✅ Interface segregation (ISP)  
✅ Imutabilidade de Value Objects  
✅ Factory Methods para criação de entidades  
✅ Validação centralizada  
✅ Exceções customizadas tipadas  
✅ Auditoria automática (Tracker)  
✅ Paginação consistente  
✅ Mapeamento EF Core separado  
✅ Seeds automáticos  
✅ Migrations versionadas  
✅ HttpClient com DI e configuração  
✅ CORS e JWT configurados  
✅ Swagger com autenticação  

---

## ⚠️ Pontos de Atenção / TODOs

1. **Domain Events:** Infraestrutura preparada mas não utilizada
2. **Validações:** FluentValidation instalado mas não implementado
3. **Testes:** Cobertura limitada ao domínio
4. **Player Entity:** Comentada no DbContext
5. **Autenticação:** Alguns endpoints com `[AllowAnonymous]` para desenvolvimento
6. **HTTPS:** Desabilitado em desenvolvimento
7. **Comentários:** Alguns TODOs espalhados no código

---

## 📖 Conclusão

O projeto **Artificial Story Oracle** demonstra uma arquitetura sólida e bem estruturada, seguindo princípios modernos de desenvolvimento:

- **Clean Architecture** com separação clara de camadas
- **Domain-Driven Design** com entidades ricas e value objects
- **CQRS** para separação de leitura/escrita
- **Repository e Query Service patterns** para acesso a dados
- **Factory Pattern** para criação consistente
- **Dependency Injection** para baixo acoplamento
- **Exception Handling centralizado** para respostas consistentes

O código é limpo, bem organizado e segue convenções consistentes, facilitando manutenção e evolução. A integração com IA (Gemini) e autenticação (Keycloak) demonstra preparação para um sistema completo de produção.

---

**Gerado em:** 25 de Outubro de 2025  
**Versão do Relatório:** 1.0  
**Framework:** .NET 9.0 / C# 13  

