# WebAPI_Livraria_ASP.NET
API REST na plataforma .NET usando a ASP.NET Core, EF Core na abordagem Code-First e aplicando Migrations e SQLServer.

APLICAÇÃO DE REGRAS DE NEGOCIOS:
- DTOs. Não mostra informações sensiveis
- Se o cliente possui vendas não é excluido
- Altera somente propriedades especificadas de uma classe
- No cadastro de vendas diminui ou altera-se o estoque
