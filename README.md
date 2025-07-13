# User Management - Prueba Técnica

<img src="UserManagement/UserManagement.Web/wwwroot/Images/Pantalla%20usuario.png" alt="Vista previa" width="1000"/>
<img src="UserManagement/UserManagement.Web/wwwroot/Images/Pantalla%20gestion%20usuario.png" alt="Vista previa" width="1000"/>
<img src="UserManagement/UserManagement.Web/wwwroot/Images/Modal%20confirmacion%20eliminación.png" alt="Vista previa" width="1000"/>
<img src="UserManagement/UserManagement.Web/wwwroot/Images/Confirmacion%20de%20eliminación.png" alt="Vista previa" width="1000"/>

Este proyecto implementa un sistema de gestión de usuarios (CRUD) siguiendo una arquitectura en capas (Capa de presentación, Capa de negocio - API, Capa de datos) utilizando ASP.NET MVC, Web API y Entity Framework Core para acceder a procedimientos almacenados de la base de datos.

Cabe recalcar que hubo algo que me parecio confuso del enunciado de la prueba ya que tenia duda si era un sp el cual tuviera todas las operaciones del CRUD o si era un sp por cada operación: "Debe crear un procedimiento almacenado donde haga el proceso CRUD para la tabla creada anteriormente Por favor hacer esta prueba en .NET", he optado por realizar la segunda, un sp por cada operación ya que me parecio una forma mas limpia de hacer las cosas.

## 🚀 Tecnologías usadas
- .NET 8
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (Con Sps)
- Bootstrap (Framework UI por defecto con MVC)
- Mapster (mapeo de Dtos <> entidades de dominio)
- Inyección de dependencias
- Patron Repository

## 📁 Estructura del proyecto
### ✅ UserManagement.Web - Capa de presentación - Proyecto WEB
Esta implementado con ASP.NET Core MVC versión .NET 8, esta capa se encarga de la interacción del usuario con el aplicativo y consumir la Api para hacer las operaciónes del CRUD, este consumi de la API se hace a través de HttpClient para efectuar las solicitudes HTTP

````markdown
├── Controllers             →  Controladores MVC para direccionamiento de views y gestion de consultas a la api
├── Dtos                    → Clases para transferencia y modelado de respuestas
├── Models                  → Modelos de vista y estructuras
├── Services                → Servicios HTTP que consumen la API por medio de HttpClient
├── Views                   → Vistas Razor (UI)
├── wwwroot                 → Archivos estáticos (CSS, JS, imágenes)
├── Program.cs              → Registro de servicios (UserHttpService y enlazarlo con la URL de la API)
`````
### ✅ UserManagement.Api - Capa de negocios – Servicio Api rest
Esta implementado con ASP.NET Core Web API version .Net 8

````markdown
├── Controllers             → Controladores HTTP con los entpoint para el CRUD ([HttpGet], [HttpPost], [HttpPut], [HttpDelete])
├── Dtos                    → Clases de transferencia para exponer en la API sin exponer directamente las entidades de dominio
├── Interfaces              → Interfaces de servicios de negocio
├── Services                → Lógica de negocio, inyecta los repository, hace validaciones, mapeos con Mapster y expone Dtos
├── Program.cs              → Registro de servicios, DBContext, Swagger, etc.
├── appsettings.json        → Configuraciones como cadena de conexión
`````
### ✅ UserManagement.Data - Capa de datos

````markdown
├── Context                 → Configuración y contexto de la base de datos
├── Entities                → Entidades del dominio / modelo de base de datos
├── Interfaces              → Interfaces para los repositorios
├── Repositories            → Implementaciones de acceso a datos, por medio de Entity Framework Core se accede a los SP 
`````

## 🧠 Funcionalidades principales

- ✅ Crear usuarios
- ✅ Listar usuarios
- ✅ Editar usuarios
- ✅ Eliminar usuarios
- ✅ Validación de datos
- ✅ Uso de procedimientos almacenados (Stored Procedures)
- ✅ Consumo de API REST desde MVC vía `HttpClient`

## 🛠️ Cómo ejecutar el proyecto
1. Clona el repositorio:
   ```bash
   git clone https://github.com/andresfaf/user-management.git
   ```
2. Configura la cadena de conexión:
  - `UserManagement.Api/appsettings.json - Cambiar Server="ServerName" por nombre de servidor local`
  ```csharp
   "ConnectionStrings": {
     "DefaultConnection": "Server=.; Database=UserManagement; Trusted_Connection=True; TrustServerCertificate=True"
   }
  ````
3. Restaura Base de datos - UserManagement.bak

4. Abrir la solución y establecer como proyectos de inicio:
- UserManagement.Web
- UserManagement.Api
    - Click derecho sobre la solución
    - Configurar proyectos de inicio
    - Seleccionar varios proyectos de inicio
    - En la tabla seleccionar en columna acción (Inicio) en los dos proyectos (Web - Api)
    - Click en Aceptar

5. verificar puerto de la API y si ha cambiado con respecto al que esta actualmente en el archivo programs de la Capa Web, modificarlo.
```scharp
builder.Services.AddHttpClient<IUserHttpService, UserHttpService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});
````
6. Seleccionar en elemento de inicio el nuevo perfil de inicio (Esta al lado de botón iniciar)

7. ✅ OK para ejecutar el proyecto.
