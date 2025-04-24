# 📋 CrudTaskAPI

Projeto desenvolvido em .NET 6, responsável por fornecer a API RESTful para gerenciamento de tarefas (Tasks), com operações de CRUD e integração com banco de dados SQL Server.

---

## 📌 Tecnologias Utilizadas

- .NET 6
- Entity Framework Core
- SQL Server
- Swagger (Swashbuckle)
- AutoMapper

---

## 📖 Funcionalidades

- Criar nova tarefa
- Listar todas as tarefas
- Consultar tarefa por ID
- Atualizar tarefa existente
- Deletar tarefa

---

## 📦 Como Executar o Back-end

### 📑 Pré-requisitos:
- .NET 6 SDK
- SQL Server

### 📥 Clone o projeto

git clone https://github.com/Rudio1/CrudTaskAPI.git
cd CrudTaskAPI

### ⚙️ Configure a string de conexão em `appsettings.json`

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=CrudTaskDB;User Id=USUARIO;Password=SENHA;"
  }
}

### 📌 Execute as migrations

dotnet ef database update

### ▶️ Rode o projeto

dotnet run

Acesse via Swagger:
https://localhost:5001/swagger

![image](https://github.com/user-attachments/assets/509b8745-e080-43ff-8105-4cae27a04cdb)

---

## 🌐 Integração com o Front-end

Este projeto se conecta ao front-end desenvolvido em React disponível em:
👉 https://github.com/Rudio1/FrontEndTaskAPI

