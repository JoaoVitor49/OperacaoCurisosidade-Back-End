# Operação Curiosidade - Backend API

## 💡 Sobre o Projeto

Este repositório contém o código do **Backend (API RESTful)** do sistema **Operação Curiosidade**.

O **objetivo central** da aplicação é fornecer um sistema completo para a **Gestão de Clientes (CRUD)**. Ele permite que usuários criem e gerenciem seus respectivos clientes, enquanto oferece autenticação, autorização e um sistema otimizado de logs de atividade.

A arquitetura foi desenhada para garantir a **eficiência** nas operações e aplicar as regras de negócio de **segregação de dados** baseadas no perfil do usuário (Admin ou User).

## 🧱 Arquitetura e Tecnologia

O projeto foi estruturado para ser manutenível e escalável, utilizando padrões e tecnologias modernas:

* **Padrão Arquitetural:** **CQRS (Command Query Responsibility Segregation)** - Separação da lógica de escrita (Commands, para o CRUD) e leitura (Queries, para logs e listagens).
* **Camada de Dados:** Uso do padrão **Repository** e consultas otimizadas via Stored Procedures.
* **Tecnologia Principal:** ASP.NET Core ([Versão 8.0])
* **Linguagem:** C#
* **Banco de Dados:** SQL Server (Usando SSMS para gestão e LocalDB para desenvolvimento).
* **ORM:** Entity Framework Core (Estratégia Code First com Migrações).

## 🔒 Regras de Acesso e Segregação de Dados

O sistema possui dois perfis principais, e a visibilidade dos dados é controlada pela *Claim* de acesso do usuário:

| Perfil | Acesso ao CRUD de Clientes | Logs de Sistema |
| :--- | :--- | :--- |
| **Admin** | Acesso total a **todos** os clientes. | Acesso a **todos** os logs de atividade. |
| **User** | CRUD restrito apenas aos clientes que **ele próprio** cadastrou. | Acesso restrito apenas aos logs de atividade de sua autoria. |

## 🛠️ Funcionalidades Chave

* **CRUD Completo de Clientes:** Criação, Leitura, Atualização e Exclusão de clientes.
* **Autenticação e Autorização:** Sistema de login e registro, com definição de perfis (User/Admin).
* **Gestão de Logs Otimizada:** Consulta de logs com paginação eficiente e filtro de permissão via Stored Procedure (`LogList`).
* **Busca Flexível:** A pesquisa nos logs filtra o termo de busca nos campos **Ação** e **UserEmail**.

---

## 💾 Estrutura do SQL Server

Para garantir o funcionamento das consultas otimizadas e do mapeamento DTO, as seguintes estruturas devem ser criadas no banco de dados.

### 1. View: `DashboardList`

Esta **View Virtual** é utilizada pelo Entity Framework para mapear o DTO `DashboardList` (o contrato de dados para a listagem principal de clientes).

```sql
CREATE VIEW DashboardList AS
SELECT 
    c.Id,
    c.Name,
    c.Email,
    c.RegisterDate,
    c.IsActive,
    c.UserId
FROM 
    Clients c;
GO
```

### 2. Stored Procedure: `LogList`

Responsável pela paginação, pesquisa e aplicação da regra de permissão (Admin/User) na listagem de logs.

```sql
CREATE PROCEDURE LogList
    @Search NVARCHAR(255) = NULL,
    @Page INT,
    @Limit INT,
    @EmailLogado NVARCHAR(255) = NULL,
    @TotalRecords INT OUTPUT
AS
BEGIN
    -- 1. Definição do Offset
    DECLARE @Offset INT = (@Page - 1) * @Limit;

    -- Lógica de Pesquisa Centralizada:
    -- Esta variável @SearchFilter simplifica as cláusulas WHERE
    DECLARE @SearchFilter NVARCHAR(MAX) = 
        CASE 
            WHEN @Search IS NOT NULL AND @Search <> '' 
            THEN '%' + @Search + '%' 
            ELSE NULL 
        END;

    -- 2. Contar o total de registros
    SELECT @TotalRecords = COUNT(L.Id)
    FROM Logs L
    WHERE 
        -- Lógica de Permissão (Admin/User):
        (@EmailLogado IS NULL OR L.UserEmail = @EmailLogado)
        -- Filtro de Pesquisa (Action OU UserEmail):
        AND (
            @SearchFilter IS NULL -- Se não há termo de busca, a condição é TRUE
            OR L.Action LIKE @SearchFilter
            OR L.UserEmail LIKE @SearchFilter
        );

    -- 3. Selecionar os registros paginados com nomes de coluna exatos
    SELECT 
        L.Id, 
        L.TimeDate, 
        L.UserEmail, 
        L.Action 
    FROM Logs L
    WHERE 
        -- Aplica as mesmas condições de filtro e permissão
        (@EmailLogado IS NULL OR L.UserEmail = @EmailLogado)
        AND (
            @SearchFilter IS NULL
            OR L.Action LIKE @SearchFilter
            OR L.UserEmail LIKE @SearchFilter
        )
    ORDER BY 
        L.TimeDate DESC 
    OFFSET @Offset ROWS 
    FETCH NEXT @Limit ROWS ONLY;
END
GO
```

---

## 🚀 Como Executar Localmente

### Pré-requisitos
* SDK do .NET Core ([Versão 8.0]).
* Visual Studio.
* SQL Server LocalDB e SSMS.

### Inicialização do Banco de Dados
1.  **String de Conexão:** Verifique a `DefaultConnection` em `appsettings.Development.json`.
2.  **Aplicação das Migrações:** Crie as tabelas e o esquema inicial:
    ```bash
    Update-Database
    # OU
    dotnet ef database update
    ```
3.  **Criação Manual:** Execute os códigos SQL da **View** e da **Stored Procedure** acima no SSMS após a criação do banco de dados.

### Execução da API
1.  Abra a solução (`OperacaoCuriosidade.sln`) no Visual Studio.
2.  Inicie o projeto API.

A documentação da API (Swagger/OpenAPI) estará disponível em `https://localhost:[PORTA]/swagger/index.html`.

## 🔗 Repositório do Frontend

O cliente web que consome esta API pode ser encontrado no repositório do Frontend:

> [https://github.com/JoaoVitor49/OperacaoCurisosidade-Front-End]
