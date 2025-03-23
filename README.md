# 🧪 Prueba Técnica - Loymark

Este repositorio contiene la solución completa para la prueba técnica solicitada por **Loymark**, la cual incluye un sistema de registro de usuarios y visualización de historial de actividades.

---

## 📂 Estructura del Proyecto

```plaintext
/CrudUsers/
│
├── front-end/      # Proyecto Blazor (.NET 8)
├── back-end/       # API RESTful .NET 8 con EF Core
└── database/       # Scripts SQL para SQL Server
```

---

## 🧾 Requerimientos

### Funcionalidades
- Registro de usuarios con los siguientes campos:
  - Nombre, Apellido, Correo electrónico, Fecha de nacimiento, Teléfono (opcional)
- Visualización de un historial de actividades por cada acción sobre un usuario.
- Navegación entre módulos mediante una barra (NavBar).

---

## 🚀 Tecnologías Usadas

| Capa         | Tecnología                  |
|--------------|------------------------------|
| Front-end    | Blazor (.NET 8)              |
| Back-end     | ASP.NET Core Web API (.NET 8)|
| Base de datos| SQL Server                   |
| ORM          | Entity Framework Core 8      |

---

## 🗃️ Base de Datos

La solución usa SQL Server como motor de base de datos. Los scripts necesarios para crear la base de datos a modo de demostracion de conocimiento se creo un script para tablas y procedimientos almacenados.

están ubicados en la carpeta [`/database`](./database).

### Tablas:
- `usuarios`
- `actividades`

Cada acción CRUD sobre `usuarios` se registra automáticamente en la tabla `actividades`.

---

## ⚙️ Instrucciones de Ejecución

### 1. Clonar Repositorio
```bash
git clone https://github.com/RoyMartinez/CrudUsers.git
cd CrudUsers
```

### 2. Crear la base de datos
simplemente levantar el proyecto back-end, y EF Core Code First creará la base de datos automáticamente con Database.Migrate().
(Solo asegurar que la cadena de coneccion del Appsettings este correcta)

### 3. Levantar el back-end
```bash
cd back-end
dotnet restore
dotnet run
```
### 4. Levantar el front-end
```bash
cd front-end
dotnet restore
dotnet run
```

📞 Contacto
Roy Roger Martinez Cano
Email: roymartinez.dev@gmail.com
LinkedIn: linkedin.com/in/roymartinez
