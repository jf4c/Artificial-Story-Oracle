# Instruções do Gemini para o repositório ArtificialStoryOracle

Este arquivo é a fonte de verdade para comportamentos esperados do assistente automático (Copilot) ao trabalhar neste repositório. Sempre responda e documente em Português Brasileiro (PT-BR).

## Prioridade
1. Siga as regras definidas neste arquivo e em `.github/GEMINI.md` (este arquivo).\
2. Siga as convenções descritas em `docs/RELATORIO_ARQUITETURA.md` quando houver conflito.

## Estilo de comunicação
- Seja direto, honesto e objetivo. Evite elogios vazios.\
- Ao apresentar mudanças: breve preâmbulo (uma linha), checklist e plano de ação.\
- Quando o usuário pedir um "plano de ação", descreva os passos pretendidos; só gere documentação em `docs/` quando solicitado.

## Regras gerais de implementação
- Linguagem: C# com .NET 9.0; use C# 12 (primary constructors) quando aplicável.
- Sempre usar DDD / Clean Architecture conforme estrutura do repositório.
- Todo I/O (banco, HTTP, disco) deve ser assíncrono (async/await) e usar sufixo `Async` em métodos públicos.
- Handlers de escrita devem implementar `ICommandHandlerAsync<TRequest, TResponse>` e leitura via `IQueryHandler<TRequest, TResponse>` ou variações já existentes.
- Repositórios para escrita (ex.: `I[Feature]Repository`) e `QueryService` para leitura.
- Usar métodos de fábrica `Create()` nas entidades para validação de domínio.
- Value Objects como `record` e imutáveis.
- Mappers como extension methods (Application layer) para conversão Entity ↔ Response e Command → Dto.
- Handlers: `sealed class` com primary constructor e campos privados somente leitura.
- Controllers finos: apenas mapeiam Input → Command/Query e delegam para handlers.

## Convenções de pastas (resumo rápido)
- ASO.Api/: Controllers, Inputs, Inputs/Mappers, Middleware
- ASO.Application/: Abstractions/, UseCases/, Mappers/, Builders/, Pagination/
- ASO.Domain/: [Subdominio]/Entities, ValueObjects, Dtos, Abstractions/Repositories
- ASO.Infra/: Database/, Repositories/, ExternalServices/

## Nomenclatura (resumo rápido)
- Controllers: `FeatureController`\
- Commands: `ActionFeatureCommand`\
- Handlers: `ActionFeatureHandler` (sealed, primary constructor)\
- Responses: `ActionFeatureResponse`\
- Inputs (API): `CreateFeatureInput`\

## Registro de dependências
- Registrar handlers, repositories e queryservices no `Program.cs` usando `AddScoped` e `AddHttpClient` para serviços externos.

## Segurança / Tokens
- Para endpoints que recebem JWT do Keycloak: extrair claims via `HttpContext.User.FindFirst(...)` e validar presença das claims necessárias. Guardar o `sub` (Keycloak user id) no `Player` como `Guid`.

## Integrações externas (ex.: Gemini API)
- Definir interface no Domain: `IGenerativeService` ou similar.\
- Implementar na Infra com `HttpClient` registrado via `AddHttpClient<IGenerativeService, GeminiService>((sp, client) => { ... })` e configurar base URL e header Authorization com chave do appsettings (não commitá-la).\
- Todos os adaptadores devem verificar `response.IsSuccessStatusCode()` e desserializar com segurança.

## Testing
- Criar testes unitários na camada `ASO.Domain.Tests` para regras de domínio (entidades, VOs).\
- Cobrir happy path + 1-2 casos de erro por entidade/VO.

## Boas práticas e notas específicas
- Não deixe `using` com caracteres invisíveis (ZWNBSP) — se aparecerem, remova-os regravando a linha `using` manualmente.\
- Não usar `var` em declarações de injeção de dependência (Program.cs) — declarar tipos explícitos.\
- Sempre rodar `dotnet build` e `dotnet test` localmente após mudanças significativas; corrigir erros antes de pedir revisão.
- Seeds e migrations: gerar migrations com nomes descritivos e aplicar no startup quando necessário para ambiente local.

## Procedimento ao receber tarefas
1. Ler o pedido completo.\
2. Se pedido envolver código: apresentar um plano curto (1-2 linhas) + checklist de mudanças (arquivos a criar/editar).\
3. Ao editar arquivos: agrupar alterações por arquivo e aplicar usando commit atômico.\
4. Rodar checagens de erros (build/tests) e reportar PASS/FAIL com logs relevantes.

## Quando esperar confirmação do usuário
- Para mudanças de alto impacto (ex.: criação de novas tabelas, alteração de contratos públicos, uso de novos serviços externos), apresentar um plano de ação e aguardar confirmação antes de implementar.

---

Última atualização: automático pelo Gemini local. Use este arquivo como referência durante sessões de pair programming.

