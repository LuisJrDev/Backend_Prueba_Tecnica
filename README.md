# Backend API RestFul - **Gestión de Posts, Comentarios y Categorías**

Este es el backend de una API Restful desarrollado en **.NET Core** para la gestión de **posts, comentarios y categorías**. A continuación, se detallan las instrucciones para instalar, configurar y ejecutar el proyecto.

## Tabla de contenidos

1. [Requisitos previos](#requisitos-previos)
2. [Instalación](#instalación)
3. [Configuración](#configuracion)
4. [Ejecución](#ejecución)
5. [Estructura de la Base de Datos](#estructura-de-la-base-de-datos)
6. [Estructura del proyecto](#estructura-del-proyecto)
7. [Endpoints del API](#endpoints-del-api)
8. [Manejo de errores](#manejo-de-errores)

---

## Requisitos previos

Antes de instalar y ejecutar el proyecto, asegúrate de tener instalados los siguientes componentes:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (local o en la nube)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) o [Visual Studio Code](https://code.visualstudio.com/)
- Visual Studio Community 2022 - Paquete Desarrollo de ASP.NET y web
- [Postman](https://www.postman.com/) o similar para probar el API - Opcional por que se puede usar Swagger
- [Git](https://git-scm.com/) para clonar el repositorio

## Instalación

1. Clona el repositorio:

   ```bash
   git clone https://github.com/LuisJrDev/Backend_Prueba_Tecnica.git
   
2. Navega al directorio del proyecto:
   
   ```bash
   cd Backend_Prueba_Tecnica

3. Abrir el Proyecto con Visual Studio

4. Restaura los paquetes NuGet:
   ```bash
   dotnet restore
   
5. Asegúrate de tener acceso a la base de datos. Puedes usar SQL Server localmente o configurar una conexión a una base de datos remota.

## Configuracion
### Configuración de la base de datos

1. La estructura de la base de datos estara debajo para que configurarla correctamente y asegúrate de tener las credenciales correctas.

2. Edita el archivo `appsettings.json` con las credenciales de conexión a la base de datos:
   ```json
    "ConnectionStrings": {
      "ConexionSQL": "Data Source=(local)\\SQLEXPRESS;Initial Catalog=PostComments;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;" 
    }
   
3. Recuerda haber configurado la estructura de la base de datos y que el nombre de la base de datos coincida con el del campo `Initial Catalog=` de la conexion.

### Configuración del servidor

1. Asegúrate de que el puerto y la URL base para el servidor estén correctamente configurados en el archivo `launchSettings.json`:
    ```json
        "profiles": {
          "http": {
            "commandName": "Project",
            "dotnetRunMessages": true,
            "launchBrowser": true,
            "launchUrl": "swagger",
            "applicationUrl": "http://localhost:5228",
            "environmentVariables": {
              "ASPNETCORE_ENVIRONMENT": "Development"
            }
          },
        "IIS Express": {
          "commandName": "IISExpress",
          "launchBrowser": true,
          "launchUrl": "swagger",
          "environmentVariables": {
            "ASPNETCORE_ENVIRONMENT": "Development"
          }
        }
    }
 
## Ejecución

1. Para ejecutar el proyecto localmente, ya teniendo abierto el proyecto en Visual Studio y abriendo el archivo llamado `Program.cs`

2. En la parte superior nos aparecera una opcion de DEBUG como esta
   <br>
     ![image](https://github.com/user-attachments/assets/e5e62aba-3bc2-494f-9331-b6128eb17285)

4. Lo ejecutamos y esperamos que ejecute y nos abra la interfaz de Swagger o si prefieren usar **Postman**

5. Si Usaras **Postman** para hacer las peticiones aqui tendras los endpoints de la API ( Ver [Endpoints del API](#endpoints-del-api) ).

## Estructura de la Base de Datos

Para garantizar que todos los desarrolladores trabajen con la misma estructura de base de datos, hemos incluido un archivo `.sql` que contiene el script necesario para crear todas las tablas y relaciones en SQL Server.

### Instrucciones para ejecutar la estructura de la base de datos

1. Asegúrate de tener acceso a una instancia de **SQL Server**.
   
2. Clona este repositorio y navega a la carpeta `DBstructure` donde está el archivo `db_structure.sql`.

3. Abre **SQL Server Management Studio (SSMS)** o cualquier otra herramienta de administración de bases de datos SQL.

4. Conéctate a tu servidor de SQL Server y selecciona la base de datos donde deseas crear la estructura. Si aún no tienes una base de datos, puedes crear una con el siguiente comando:

   ```sql
   CREATE DATABASE PostComments;
   GO
   
5. Carga el archivo `db_structure.sql` en tu editor de SQL y ejecútalo. Este archivo generará las tablas necesarias para los posts, comentarios y categorías, junto con sus relaciones y restricciones.

### Archivo de estructura
   El archivo db_structure.sql contiene la siguiente estructura básica:
   
   - **Posts**
   - **Comentarios**
   - **Categorías**
   - Relaciones entre las tablas y las claves foráneas.
     
   > **Nota:** Asegúrate de que las credenciales y configuraciones en el archivo `appsettings.json` coincidan con la configuración de la base de datos que acabas de crear.
        
## Estructura del proyecto

      |-- Controllers
      |-- Models - contiene los DTOs
      |-- Repositories - contiene las interfaces y los repositorios
      |-- Program.cs
      |-- appsettings.json
      
- Controllers: Contiene los controladores que manejan las solicitudes HTTP.
- Models: Define los modelos y DTOs utilizados en la base de datos y la transferencia de datos.
- Repositories: Encargado de la comunicación con la base de datos, incluyendo las interfaces y los repositorios que implementan la lógica de acceso a datos.
- Program.cs: Punto de entrada del proyecto.
- appsettings.json: Archivo de configuración de la aplicación.
      


## Endpoints del API

### Posts
- GET `/api/posts` - Lista todos los posts
- GET `/api/posts/{id}` - Obtiene los detalles del Post por ID
- POST `/api/posts` - Crea un nuevo post
- PUT `/api/posts/{id}` - Actualiza un post
- DELETE `/api/posts/{id}` - Elimina un post
- POST `/api/posts/{id}/categorias` - Agregar categorias al Post

### Categorías
- GET `/api/category` - Lista todas las categorías
- POST `/api/category` - Crea una nueva categoría
- PUT `/api/category/{id}` - Actualiza una categoría
- DELETE `/api/category/{id}` - Elimina un categoría

### Comentarios
- POST `/api/comment` - Añade un comentario a un post
- PUT `/api/comment` - elimina un comentario de post
- DELETE `/api/comment/{id}` - Elimina un comentario

## Manejo de errores

El proyecto utiliza `SweetAlert2` para manejar los errores y confirmaciones en el frontend, y en el backend se manejan las excepciones mediante `try-catch`. Las respuestas de error estándar incluyen códigos de estado HTTP apropiados como `400` (Bad Request), `404` (Not Found), y `500` (Internal Server Error).












