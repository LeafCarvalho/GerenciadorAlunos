# Gerenciador de Alunos

Projeto desenvolvido com foco em **boas práticas de arquitetura, TDD e Clean Architecture**, utilizando **.NET 9, EF Core e PostgreSQL**.

---

## Tecnologias
- **.NET 9** (C#)
- **Entity Framework Core** + **PostgreSQL**
- **xUnit** + **FluentAssertions** (TDD e testes unitários)
- **Swagger / OpenAPI** (NSwag + Microsoft.AspNetCore.OpenApi)
- **BCrypt.Net-Next** para hash de senhas
- **GitHub Actions** (CI: build + test)

## Clonando e iniciando o projeto

Clone o projeto com: `git clone https://github.com/seu-usuario/gerenciador-alunos.git`
Acesse a pasta (no diretório onde o projeto se encontra) usando: `cd gerenciador-alunos`
Restaure os pacotes: `dotnet restore`
Aplique as migrations do EF Core: `dotnet ef database update`
Use isso para compilar: `dotnet build`
Para testes: `dotnet test`
E para iniciar a aplicação: `dotnet run`