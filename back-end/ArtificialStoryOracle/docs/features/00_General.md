# 00_General

## Visão Geral do Projeto

### Artificial Story Oracle (ASO)

O Artificial Story Oracle (ASO) é uma plataforma inovadora projetada para auxiliar mestres e
jogadores de RPG durante suas campanhas e no processo de criação de personagens. A plataforma
utiliza tecnologias modernas, incluindo inteligência artificial, para enriquecer a experiência de
jogo de RPG.

### Objetivos Principais

- **Criação de Personagens**: Sistema completo para criação e gerenciamento de personagens de RPG
- **Geração de Backstory**: Utilização de IA para gerar histórias de background para personagens
- **Criação de NPCs**: Ferramentas para mestres criarem NPCs com personalidades e histórias
  complexas
- **Planejamento de Campanhas**: Assistência ao mestre para idealizar e desenvolver campanhas
- **Integração com Jogadores**: Facilitar a comunicação e compartilhamento entre mestres e jogadores
- **Gerenciamento de Sessões**: Ferramentas para acompanhamento de sessões e evolução da história

## Arquitetura

### Clean Architecture

O projeto segue os princípios da Clean Architecture, organizando o código em camadas concêntricas
com dependências apontando para dentro:

```
ASO/
├── ASO.Domain/          # Entidades, regras de negócio, interfaces
├── ASO.Application/     # Casos de uso, serviços de aplicação
├── ASO.Infrastructure/  # Implementações de interfaces, acesso a dados
└── ASO.Api/             # Controllers, DTOs, configuração da API
```

### Princípios de Design

- **Domain-Driven Design (DDD)**: Modelagem baseada no domínio de RPG
- **CQRS Pattern**: Separação de responsabilidades entre comandos e consultas
- **Repository Pattern**: Abstração da camada de persistência
- **Dependency Injection**: Inversão de controle e baixo acoplamento
- **Domain Events**: Comunicação entre agregados através de eventos

## Tecnologias Utilizadas

### Backend

- **.NET 9.0**: Framework principal
- **ASP.NET Core**: Para API RESTful
- **Entity Framework Core**: ORM para acesso a dados
- **PostgreSQL**: Banco de dados relacional
- **FluentValidation**: Validação de inputs (planejado)
- **Keycloak**: Autenticação e autorização

### Frontend (Planejado)

- **Angular**: Framework para SPA
- **PrimeNG**: Componentes de UI
- **NgRx**: Gerenciamento de estado
- **RxJS**: Programação reativa

## Estrutura do Domínio

### Principais Entidades

- **Character**: Personagens jogáveis dos usuários
- **Ancestry**: Raças/Ancestralidades disponíveis
- **Class**: Classes de personagem (Guerreiro, Mago, etc.)
- **Game**: Campanhas/Jogos criados pelos mestres
- **Player**: Usuários jogadores na plataforma

### Regras de Negócio Principais

- Personagens pertencem a um jogador específico
- Personagens são definidos por classe, ancestralidade e atributos
- Jogos são criados por mestres e podem ter múltiplos jogadores
- Sistema baseado em regras inspiradas em RPGs de mesa tradicionais

## Padrões de Desenvolvimento

### Código

- **C# 13.0**: Utilização de recursos modernos da linguagem
- **Primary Constructors**: Simplificação de classes
- **Records**: Para DTOs e objetos imutáveis
- **Nullable Reference Types**: Prevenção de null references

### API

- **RESTful**: Princípios REST para endpoints
- **JSON**: Formato padrão para comunicação
- **Swagger/OpenAPI**: Documentação automática
- **JWT**: Autenticação baseada em tokens

## Próximos Passos do Projeto

### Melhorias Arquiteturais

- Implementação de sistema robusto de tratamento de exceções
- Configuração de validações de entrada com FluentValidation
- Implementação de logging estruturado
- Finalização das features básicas (CRUD de personagens)

### Features Futuras

- Integração com IA para geração de histórias
- Sistema de compartilhamento de personagens
- Dashboard para mestres acompanharem campanhas
- Integração com APIs externas para regras de jogos

---

### --------------------------------Planos----------------------------------
### Plano de Implementação de Middleware de Exceções Centralizado

#### 1. Definição das Exceções de Domínio

**Objetivo**: Criar estrutura base para exceções específicas do domínio

**Tarefas**:
- [ ] Criar classe abstrata `DomainException` com propriedades:
  - `string ErrorCode` (código de erro padronizado)
  - Construtor que aceita mensagem e código de erro

- [ ] Criar exceções específicas derivadas de `DomainException`:
  - `EntityNotFoundException`: Para recursos não encontrados
  - `ValidationException`: Para erros de validação (com lista de erros detalhados)
  - `BusinessRuleException`: Para violação de regras de negócio
  - `UnauthorizedException`: Para erros de autenticação
  - `ForbiddenException`: Para erros de autorização
  - `ConflictException`: Para conflitos em operações (ex: duplicação)

  #### 2. Implementação do Middleware de Exceções

  **Objetivo**: Criar middleware para capturar e tratar exceções de forma centralizada

  **Tarefas**:
  - [ ] Criar classe `ExceptionHandlingMiddleware`:
  - Injetar `ILogger` para logging de exceções
  - Implementar método `InvokeAsync` para capturar exceções
  - Criar método `HandleExceptionAsync` para processar exceções e gerar resposta padronizada
  - Implementar método `GetStatusCode` para mapear tipos de exceção para códigos HTTP
  - Implementar método `CreateResponse` para formatar resposta JSON de erro

  - [ ] Criar extensão `UseExceptionHandling` para configuração do middleware:
  ```csharp
  public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
  {
      return builder.UseMiddleware<ExceptionHandlingMiddleware>();
  }
  ```

  - [ ] Criar modelo `ErrorResponse` para padronizar respostas de erro:
  - `bool Success` (sempre false para erros)
  - `ErrorDetails Error` com:
    - `string Code` (código de erro padronizado)
    - `string Message` (mensagem para o usuário)
    - `IEnumerable<string>? Details` (detalhes adicionais, opcional)
    - `string TraceId` (para rastreamento e troubleshooting)

  #### 3. Integração com a Aplicação

  **Objetivo**: Configurar o middleware na aplicação e integrar com o sistema existente

  **Tarefas**:
  - [ ] Registrar middleware no pipeline de requisição:
  ```csharp
  // Em Program.cs ou Startup.cs
  app.UseExceptionHandling();
  ```

  - [ ] Atualizar handlers para lançar exceções ao invés de retornar Result:
  ```csharp
  // Antes (com Result Pattern)
  return Result<TResponse>.Failure(Error.Validation("VALIDATION_ERROR", "Dados inválidos", errors));

  // Depois (com exceções)
  throw new ValidationException("Dados inválidos", errors);
  ```

  - [ ] Simplificar controllers para trabalhar diretamente com os objetos de resposta:
  ```csharp
  // Antes (com Result Pattern)
  var result = await _mediator.Send(command);
  return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

  // Depois (com exceções)
  var response = await _mediator.Send(command); // Exceções serão capturadas pelo middleware
  return Ok(response);
  ```

  #### 4. Integração com Sistema de Validação

  **Objetivo**: Integrar FluentValidation com o sistema de exceções

  **Tarefas**:
  - [ ] Criar MediatR Behavior para validação:
  ```csharp
  public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
      private readonly IEnumerable<IValidator<TRequest>> _validators;

      public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
      {
          _validators = validators;
      }

      public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
      {
          if (_validators.Any())
          {
              var context = new ValidationContext<TRequest>(request);
              var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
              var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

              if (failures.Count != 0)
              {
                  var errors = failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}").ToArray();
                  throw new ValidationException("Um ou mais erros de validação ocorreram", errors);
              }
          }

          return await next();
      }
  }
  ```

  - [ ] Registrar ValidationBehavior no contêiner de DI:
  ```csharp
  services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
  ```

  #### 5. Configuração do Swagger

  **Objetivo**: Documentar o formato de resposta padronizada e exceções

  **Tarefas**:
  - [ ] Configurar esquemas de resposta de erro no Swagger:
  ```csharp
  services.AddSwaggerGen(options =>
  {
      // Configuração geral
      options.SwaggerDoc("v1", new OpenApiInfo { Title = "ASO API", Version = "v1" });

      // Adicionar esquema de erro padronizado
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
          // Configuração de autenticação
      });

      // Configurar exemplos de resposta para erros comuns
      options.AddOperation400ResponseSchema();
      options.AddOperation404ResponseSchema();
      options.AddOperation500ResponseSchema();
  });
  ```

  - [ ] Criar método de extensão para adicionar exemplos de respostas de erro:
  ```csharp
  public static void AddOperation400ResponseSchema(this SwaggerGenOptions options)
  {
      options.OperationFilter<BadRequestResponseOperationFilter>();
  }
  ```

  - [ ] Implementar filtros de operação para configurar exemplos de resposta

  #### 6. Testes

  **Objetivo**: Garantir o funcionamento correto do middleware de exceções

  **Tarefas**:
  - [ ] Implementar testes unitários para exceções de domínio
  - [ ] Criar testes para o middleware de exceções:
    - Verificar se diferentes tipos de exceção geram o código HTTP correto
    - Verificar se o formato da resposta está correto
    - Testar cenários com e sem detalhes de erro

  - [ ] Criar testes de integração para endpoints com tratamento de erro:
    - Verificar comportamento com inputs inválidos
    - Verificar comportamento com recursos inexistentes
    - Verificar comportamento com erros de regras de negócio

  #### 7. Documentação

  **Objetivo**: Documentar o sistema de tratamento de exceções

  **Tarefas**:
  - [ ] Criar guia de uso do sistema de exceções para desenvolvedores
  - [ ] Documentar todas as exceções personalizadas e quando utilizá-las
  - [ ] Criar tabela de mapeamento entre exceções e códigos HTTP
  - [ ] Documentar formato padrão de resposta de erro da API
  - [ ] Criar exemplos de uso para diferentes cenários de erro

  #### 8. Migração de Endpoints Existentes

  **Objetivo**: Atualizar endpoints existentes para usar o novo padrão baseado em exceções

  **Tarefas**:
  - [ ] Identificar todos os endpoints existentes
  - [ ] Atualizar handlers para lançar exceções ao invés de retornar Result
  - [ ] Simplificar controllers removendo código de tratamento de erro
  - [ ] Verificar comportamento dos endpoints após migração

  #### 9. Exemplo de Implementação Final

  **Middleware**:
  ```csharp
  public class ExceptionHandlingMiddleware
  {
      private readonly RequestDelegate _next;
      private readonly ILogger<ExceptionHandlingMiddleware> _logger;

      public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
      {
          _next = next;
          _logger = logger;
      }

      public async Task InvokeAsync(HttpContext context)
      {
          try
          {
              await _next(context);
          }
          catch (Exception ex)
          {
              _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
              await HandleExceptionAsync(context, ex);
          }
      }

      private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
      {
          var statusCode = GetStatusCode(exception);
          var response = CreateResponse(exception, context);

          context.Response.ContentType = "application/json";
          context.Response.StatusCode = statusCode;

          await context.Response.WriteAsJsonAsync(response);
      }

      private static int GetStatusCode(Exception exception) => exception switch
      {
          ValidationException => StatusCodes.Status400BadRequest,
          EntityNotFoundException => StatusCodes.Status404NotFound,
          BusinessRuleException => StatusCodes.Status400BadRequest,
          UnauthorizedException => StatusCodes.Status401Unauthorized,
          ForbiddenException => StatusCodes.Status403Forbidden,
          ConflictException => StatusCodes.Status409Conflict,
          _ => StatusCodes.Status500InternalServerError
      };

      private static object CreateResponse(Exception exception, HttpContext context)
      {
          return new
          {
              success = false,
              error = new
              {
                  code = exception is DomainException domainException 
                      ? domainException.ErrorCode 
                      : "INTERNAL_SERVER_ERROR",
                  message = exception.Message,
                  details = exception is ValidationException validationException 
                      ? validationException.Errors 
                      : null,
                  traceId = context.TraceIdentifier
              }
          };
      }
  }
  ```

  **Controller**:
  ```csharp
  [ApiController]
  [Route("api/[controller]")]
  public class CharacterController : ControllerBase
  {
      private readonly IMediator _mediator;

      public CharacterController(IMediator mediator)
      {
          _mediator = mediator;
      }

      [HttpPost]
      [ProducesResponseType(typeof(CreateCharacterResponse), StatusCodes.Status201Created)]
      [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> Create([FromBody] CreateCharacterInput input)
      {
          // Sem try-catch aqui - o middleware irá tratar exceções
          var result = await _mediator.Send(new CreateCharacterCommand(input));
          return Created($"/api/character/{result.Id}", result);
      }
  }
  ```

  **Handler**:
  ```csharp
  public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, CreateCharacterResponse>
  {
      public async Task<CreateCharacterResponse> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
      {
          // Validação (já tratada pelo ValidationBehavior, mas poderia ser feita aqui também)
          if (string.IsNullOrEmpty(request.Name))
          {
              throw new ValidationException("Dados do personagem inválidos", new[] { "O nome é obrigatório" });
          }

          // Regra de negócio
          if (/* regra de negócio falhou */)
          {
              throw new BusinessRuleException("O personagem não pode ser criado por esta razão", "CHARACTER_CREATION_FAILED");
          }

          // Processo de criação...
          // var newCharacter = ...

          // Retorno direto
          return new CreateCharacterResponse { Id = newCharacter.Id, Name = newCharacter.Name };
      }
  }
  ```

  **Equipe**: Projeto de TCC  
  **Repositório**: ArtificialStoryOracle  
  **Última Atualização**: Julho de 2025

#### 2. Implementação na Camada de Aplicação

**Objetivo**: Integrar o Result Pattern nos handlers e serviços

**Tarefas**:
- [ ] Atualizar interfaces de handlers/services para retornar `Result<T>`:
  ```csharp
  // Antes
  Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

  // Depois
  Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
  ```

- [ ] Implementar retornos padronizados nos handlers:
  ```csharp
  // Sucesso
  return Result<TResponse>.Success(mappedResult);

  // Falha
  return Result<TResponse>.Failure(
      Error.Validation("VALIDATION_ERROR", "Dados inválidos fornecidos.", validationErrors));
  ```

- [ ] Criar catálogo de códigos de erro padronizados (documentação)

#### 3. Implementação na Camada de API

**Objetivo**: Converter Results internos em respostas HTTP padronizadas

**Tarefas**:
- [ ] Criar classe `ApiResponse<T>` para padronizar todas as respostas HTTP:
  - `bool Success`
  - `T Data` (apenas quando Success = true)
  - `ApiError Error` (apenas quando Success = false)
  - `object Metadata` (opcional, para paginação, etc.)

- [ ] Criar classe `ApiError` para representar erros na API:
  - `string Code`
  - `string Message`
  - `IEnumerable<string> Details`
  - `string TraceId` (para rastreamento)

- [ ] Criar classe `PaginationMetadata` para respostas paginadas:
  - `int Page`
  - `int PageSize`
  - `int TotalCount`
  - `int TotalPages`

- [ ] Implementar `ApiResponseFactory` para converter Results em ApiResponses

#### 4. Middleware e ActionFilters

**Objetivo**: Garantir que todas as respostas sigam o padrão definido

**Tarefas**:
- [ ] Criar `ApiResponseActionFilter` para converter automaticamente resultados em ApiResponse:
  ```csharp
  public class ApiResponseActionFilter : IAsyncActionFilter
  {
      public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
      {
          var executedContext = await next();

          if (executedContext.Result is ObjectResult objectResult)
          {
              if (objectResult.Value is IResult resultValue)
              {
                  executedContext.Result = new ObjectResult(ApiResponseFactory.FromResult(resultValue));
              }
          }
      }
  }
  ```

- [ ] Criar `ExceptionHandlingMiddleware` para capturar exceções não tratadas e convertê-las em ApiResponse:
  - Mapear tipos de exceção para ErrorTypes apropriados
  - Incluir logging estruturado das exceções
  - Adicionar TraceId para rastreamento em ambientes de produção

#### 5. Integração com Validação

**Objetivo**: Integrar FluentValidation com o sistema de resposta padronizada

**Tarefas**:
- [ ] Configurar `ValidationBehavior` para MediatR:
  - Capturar erros de validação
  - Retornar Result.Failure com erros formatados

- [ ] Criar extensão para converter ValidationFailure em Error:
  ```csharp
  public static Error ToError(this ValidationFailure failure)
  {
      return new Error
      {
          Code = $"VALIDATION_{failure.PropertyName.ToUpper()}",
          Message = failure.ErrorMessage,
          Type = ErrorType.Validation
      };
  }
  ```

#### 6. Configuração do Swagger

**Objetivo**: Documentar o formato de resposta padronizado

**Tarefas**:
- [ ] Configurar exemplos de resposta no Swagger
- [ ] Adicionar documentação dos códigos de erro
- [ ] Configurar esquemas para ApiResponse, ApiError e PaginationMetadata

#### 7. Testes

**Objetivo**: Garantir que o sistema de resposta padronizada funciona corretamente

**Tarefas**:
- [ ] Testes unitários para Result e Error
- [ ] Testes para conversão de Result para ApiResponse
- [ ] Testes de integração para garantir que todas as respostas seguem o padrão
- [ ] Testes para verificar códigos HTTP corretos baseados no tipo de erro

#### 8. Documentação

**Objetivo**: Documentar o sistema para desenvolvedores futuros

**Tarefas**:
- [ ] Criar guia de uso do Result Pattern para desenvolvedores
- [ ] Documentar todos os códigos de erro e seus significados
- [ ] Criar exemplos de uso para diferentes cenários
- [ ] Documentar mapeamento entre ErrorType e códigos HTTP

#### 9. Migração de Endpoints Existentes

**Objetivo**: Atualizar endpoints existentes para usar o novo padrão

**Tarefas**:
- [ ] Identificar todos os endpoints existentes
- [ ] Atualizar handlers para retornar Result<T>
- [ ] Atualizar controllers para usar ApiResponse
- [ ] Verificar tratamento de erros em cada endpoint

#### 10. Exemplo de Implementação Final

**Controller**:
```csharp
[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly IMediator _mediator;

    public CharacterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateCharacterResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCharacterInput input)
    {
        var result = await _mediator.Send(new CreateCharacterCommand(input));

        return result.IsSuccess
            ? Created($"/api/character/{result.Value.Id}", ApiResponse<CreateCharacterResponse>.Success(result.Value))
            : BadRequest(ApiResponse<CreateCharacterResponse>.Failure(result.Error));
    }
}
```

**Handler**:
```csharp
public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, Result<CreateCharacterResponse>>
{
    public async Task<Result<CreateCharacterResponse>> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        // Lógica do handler

        if (/* validação falhou */)
        {
            return Result<CreateCharacterResponse>.Failure(
                Error.Validation("INVALID_CHARACTER", "Dados do personagem inválidos", new[] { "O nome é obrigatório" }));
        }

        // Criação bem-sucedida
        return Result<CreateCharacterResponse>.Success(new CreateCharacterResponse { Id = newCharacter.Id, Name = newCharacter.Name });
    }
}
```

**Equipe**: Projeto de TCC  
**Repositório**: ArtificialStoryOracle  
**Última Atualização**: Julho de 2025


