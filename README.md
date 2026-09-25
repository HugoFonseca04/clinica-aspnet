# Clínica ASP.NET MVC

Projeto final de ASP.NET MVC com .NET Core, Entity Framework Core, SQLite e Repository.

## Requisitos atendidos

- ASP.NET MVC com .NET 10
- SQLite
- Entity Framework Core
- Repository Pattern
- 3 CRUDs completos: Pacientes, Profissionais e Especialidades
- Cada model possui pelo menos 5 propriedades de dados
- Validações com Data Annotations
- Relacionamento entre Profissional e Especialidade
- Banco criado automaticamente na primeira execução com `EnsureCreated()`

## Como executar

Na pasta `ClinicaASPNet`:

```bash
dotnet restore
dotnet run
```

O banco `clinica.db` é criado automaticamente na primeira execução.

## CRUDs

- `/Pacientes`
- `/Profissional`
- `/Especialidade`
