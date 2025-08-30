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

Clone o projeto com: `git clone https://github.com/seu-usuario/gerenciador-alunos.git` <br><br>
Acesse a pasta (no diretório onde o projeto se encontra) usando: `cd gerenciador-alunos` <br><br>
Restaure os pacotes: `dotnet restore` <br><br>
Aplique as migrations do EF Core: `dotnet ef database update` <br><br>
Use isso para compilar: `dotnet build` <br><br>
Para testes: `dotnet test` <br><br>
E para iniciar a aplicação: `dotnet run` <br><br>
