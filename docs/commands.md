# Comandos

Toda a criacao dos projetos e solution deste projeto foram feitos via dotnet CLI e nao pelo Visual Studio (como eh o padrao)

## Build geral
```dotnet
dotnet build
```

## Rodar testes
```dotnet
dotnet test
```

## Rodar API
```dotnet
dotnet run --project src/InvestmentPortfolio.API
```

## Watch mode (hot reload)
```dotnet
dotnet watch --project src/InvestmentPortfolio.API
```

## Migrations (EF Core)
```dotnet
dotnet ef migrations add InitialCreate --project src/InvestmentPortfolio.Infrastructure --startup-project src/InvestmentPortfolio.API
```

```dotnet
dotnet ef database update --project src/InvestmentPortfolio.Infrastructure --startup-project src/InvestmentPortfolio.API
```

## Criar a solution
```dotnet
dotnet new sln -n InvestmentPortfolio
```

## Projetos
```dotnet
dotnet new classlib -n InvestmentPortfolio.Domain -o src/InvestmentPortfolio.Domain
```

```dotnet
dotnet new classlib -n InvestmentPortfolio.Application -o src/InvestmentPortfolio.Application
```

```dotnet
dotnet new classlib -n InvestmentPortfolio.Infrastructure -o src/InvestmentPortfolio.Infrastructure
```

```dotnet
dotnet new webapi -n InvestmentPortfolio.API -o src/InvestmentPortfolio.API
```

## Testes
```dotnet
dotnet new xunit -n InvestmentPortfolio.UnitTests -o tests/InvestmentPortfolio.UnitTests
```

```dotnet
dotnet new xunit -n InvestmentPortfolio.IntegrationTests -o tests/InvestmentPortfolio.IntegrationTests
```

## Adicionar na solution
```dotnet
dotnet sln add src/**/*.csproj tests/**/*.csproj
```

## Application referencia Domain
```dotnet
dotnet add src/InvestmentPortfolio.Application/InvestmentPortfolio.Application.csproj reference src/InvestmentPortfolio.Domain/InvestmentPortfolio.Domain.csproj
```

## Infrastructure referencia Domain e Application
```dotnet
dotnet add src/InvestmentPortfolio.Infrastructure/InvestmentPortfolio.Infrastructure.csproj reference src/InvestmentPortfolio.Domain/InvestmentPortfolio.Domain.csproj
```

```dotnet
dotnet add src/InvestmentPortfolio.Infrastructure/InvestmentPortfolio.Infrastructure.csproj reference src/InvestmentPortfolio.Application/InvestmentPortfolio.Application.csproj
```

## API referencia todos
```dotnet
dotnet add src/InvestmentPortfolio.API/InvestmentPortfolio.API.csproj reference src/InvestmentPortfolio.Domain/InvestmentPortfolio.Domain.csproj
```

```dotnet
dotnet add src/InvestmentPortfolio.API/InvestmentPortfolio.API.csproj reference src/InvestmentPortfolio.Application/InvestmentPortfolio.Application.csproj
```

```dotnet
dotnet add src/InvestmentPortfolio.API/InvestmentPortfolio.API.csproj reference src/InvestmentPortfolio.Infrastructure/InvestmentPortfolio.Infrastructure.csproj
```

## Adiciona package no Infrastructure
```dotnet
cd src/InvestmentPortfolio.Infrastructure

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package StackExchange.Redis
dotnet add package RabbitMQ.Client
```

## Adiciona package no Application  
```dotnet
cd src/InvestmentPortfolio.Application

dotnet add package MediatR
```

## Adiciona package no testes
```dotnet
cd tests/InvestmentPortfolio.IntegrationTests

dotnet add package FluentAssertions
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Testcontainers.PostgreSql
```

```dotnet
cd tests/InvestmentPortfolio.UnitTests

dotnet add package FluentAssertions
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Testcontainers.PostgreSql
```

## docker-compose

Ao inves de usar o SQL Server eu preferi usar um container rodando PostgreSQL + Redis, por serem gratuitos, leves e mais "cloud-friendly" =)

### Rodando o container

Para executar o container eh soh executar o seguinte comando:


```docker
docker-compose up -d
```
