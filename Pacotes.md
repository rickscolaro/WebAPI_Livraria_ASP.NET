
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add "name"
dotnet ef database update (criarBanco)
dotnet ef migrations remove // remove a ultima migração
dotnet ef migrations script -o./script.sql(gera um script das migrations)

// Esconder informações sensíveis // DTOs
dotnet add package AutoMapper
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection


-- Segurança --
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer


// Ja gera login e logout
dotnet new mvc --auth Individual


//dotnet new gitignore