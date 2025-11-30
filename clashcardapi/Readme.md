# **API REST - Cartas y Jugadores de Clash Royale conectado con SOAP y gRPC**

Esta es una API REST que desarrollé utilizando el lenguaje Java para aplicar lo aprendido en clase de distribuidos sobre REST. Utilicé Spring Boot que consume dos servicios: el servicio SOAP clashroyale (API creada en el primer parcial) y el servicio gRPC PlayerApi. Esta API REST permite gestionar tanto las cartas como los jugadores de Clash Royale, realizando acciones completas sobre ambas. La API implementa autenticación y autorización mediante OAuth2 con Ory Hydra, cache con Redis, y maneja errores tanto de SOAP como de gRPC traduciéndolos a códigos HTTP apropiados

## **¿Qué utilicé?**

**Lenguaje**: Java 17

**Framework**: Spring Boot 3.3.0 con Maven como herramienta de gestión de dependencias

**Seguridad**: OAuth2 Resource Server con Ory Hydra v2.2 y utilizando los JWT tokens

**Cache**: Redis 7-alpine para almacenamiento en memoria

**Base de datos**: 
- MariaDB 10.11 (El que usa SOAP para las cartas)
- MongoDB latest (El que usa gRPC para los jugadores)
- PostgreSQL 16-alpine (usada por Ory Hydra)

**SOAP**: Spring Web Services con WebServiceTemplate para consumir el servicio de cartas

**gRPC**: gRPC 1.58.0 con Protocol Buffers 3.24.0 para consumir el servicio de jugadores

**Contenedores**: Docker Compose y Dockerfile para el control central de todo lo necesario

**Pruebas**: Utilizando Postman y curl/Git Bash

**ORM**: Hibernate (en el servicio SOAP)

**ODM**: MongoDB Go Driver v1.17.1 (en el gRPC)

**Especificación de la API**: OpenAPI/Swagger

## **Arquitectura del Sistema**

La API sigue el siguiente flujo:

### **Para las Cartas (SOAP):**
- El cliente Postman envía peticiones HTTP REST con OAuth2 para validación con token
- La API REST (puerto 8100) recibe la petición
- Pasa por el **Controller** que maneja la validación, errores y sintaxis REST
- Luego por el **Service** donde está la lógica de negocio
- Después por el **Gateway** donde se traduce con los **Mappers** y se consume el servicio SOAP
- Se conecta con la **Infraestructura** que comunica con el servicio SOAP
- El servicio **SOAP** (puerto 8089) aporta las funciones implementadas en el parcial pasado
- Se conecta con la base de datos **MariaDB** (puerto 3312)
- Regresa la respuesta a través del Controller al cliente

### **Para los Jugadores (gRPC):**
- El cliente envía peticiones HTTP REST con OAuth2 para validación con token
- La API REST (puerto 8100) recibe la petición
- Pasa por el **PlayerController** que maneja la validación, errores y sintaxis REST
- Luego por el **PlayerService** donde está la lógica de negocio
- Después por el **PlayerGateway** donde se traduce con el **PlayerMapper** y se consume el servicio gRPC
- Se conecta con la **GrpcClientConfig** que crea el canal de comunicación gRPC
- El servicio **gRPC PlayerApi** (puerto 8103) implementado en Go procesa las peticiones
- Se conecta con la base de datos **MongoDB** (puerto 27019)
- Regresa la respuesta a través del PlayerController al cliente

### **Manejo de Errores:**
- **SOAP Faults** se traducen a códigos HTTP (400, 404, 409, 502)
- **gRPC Status Codes** se mapean a códigos HTTP:
  - `NOT_FOUND` → 404
  - `INVALID_ARGUMENT` → 400
  - `ALREADY_EXISTS` → 409
  - `UNAVAILABLE/INTERNAL` → 502

## **Endpoints Disponibles**

### **Cartas (SOAP):**
- `[GET]` - GetByID - Obtiene una carta por su ID
- `[GET]` - GetAllCards (paginado) - Lista todas las cartas con paginación
- `[POST]` - CreateCard - Crea una nueva carta
- `[PUT]` - UpdateCard - Actualiza todos los campos de una carta
- `[PATCH]` - PatchCard - Actualización parcial de campos específicos
- `[DELETE]` - DeleteCard - Elimina una carta por su ID

### **Jugadores (gRPC):**
- `[GET]` - GetPlayerById - Obtiene un jugador por su ID de MongoDB
- `[GET]` - GetAllPlayersByName - Busca jugadores por nombre (server streaming)
- `[POST]` - CreatePlayers - Crea múltiples jugadores (client streaming)
- `[PUT]` - UpdatePlayer - Actualiza todos los campos de un jugador
- `[DELETE]` - DeletePlayer - Elimina un jugador por su ID

### **Códigos de respuesta HTTP:**

- `200 OK` - Operación exitosa (GET, PUT, PATCH)
- `201 Created` - Se creó correctamente (POST)
- `204 No Content` - Eliminado correctamente (DELETE, PUT/PATCH sin contenido)
- `400 Bad Request` - Datos inválidos (validaciones fallidas)
- `401 Unauthorized` - Sin autorización, token no válido
- `403 Forbidden` - Token válido pero sin scopes correctos
- `404 Not Found` - Recurso no encontrado (carta o jugador)
- `409 Conflict` - Recurso con ese nombre ya existe
- `502 Bad Gateway` - Servicio SOAP/gRPC no disponible o error inesperado


## **Instrucciones para ejecutar el proyecto**

### **Requisitos previos:**

- **Java 17** instalado
- **Docker Desktop** o Podman 
- **Postman** para pruebas
- **Git** para clonar el repositorio
- **Git Bash** (opcional, para pruebas con curl)
- **Puertos libres:** 8100 (REST), 8089 (SOAP), 8103 (gRPC), 6379 (Redis), 3312 (MariaDB), 27019 (MongoDB), 4444/4445 (Hydra), 5432 (PostgreSQL)

Nota: Si esos puertos no los tiene libres puede usar otros, pero es cambiando el docker-compose.yaml

### **Pasos de instalación:**

1. Copiar el repo para poder correr la API utilizando la consola de comandos de su elección, ingresar con "cd" a la ruta donde quiera clonar el repositorio

2. Una vez dentro de esa ruta, con el siguiente comando podrá clonar el repositorio:
```bash
git clone https://github.com/MiltonChPe/Sistemas-Didstribuidos-MiltonUzzCh.git
```

3. Una vez descargado, para acceder a los archivos del repo deberá usar el comando:
```bash
cd Sistemas-Didstribuidos-MiltonUzzCh
```

   3.1. Como el proyecto sigue en Pull Request es necesario acceder a la rama para poder correrlo, por lo tanto deberá utilizar el comando:
   ```bash
   git checkout feature/ClashCardApi
   ```

   3.2. Una vez que diga "switched to a new branch" y el nombre de la rama, para acceder al archivo y correr la API deberá usar el comando:
   ```bash
   cd clashcardapi
   ```

4. Una vez dentro, primero es compilar el proyecto para verificar que todo funciona bien utilizando:
```bash
.\mvnw.cmd clean package -DskipTests
```

5. Una vez listo se levantan los contenedores. Como todo está centralizado en un docker-compose, solamente se ocupa el siguiente comando:
```bash
docker-compose up -d --build
```

**NOTA:** El docker-compose ahora incluye automáticamente:
- El servicio SOAP de cartas (clashroyale-soap)
- El servicio gRPC de jugadores (player-api) 
- Todas las bases de datos necesarias (MariaDB, MongoDB, PostgreSQL)
- Redis, Ory Hydra y la API REST

6. Se hace un `docker ps` para ver que todos los contenedores estén corriendo correctamente. Deberían aparecer:
- clashcard-api (REST - puerto 8100)
- clashroyale-soap (SOAP - puerto 8089)
- player-api (gRPC - puerto 8103)
- player-mongodb (MongoDB - puerto 27019)
- mariadb (puerto 3312)
- redis (puerto 6379)
- hydra (puertos 4444/4445)
- postgres (puerto 5432)

7. Para verificar que funcionan los servicios:

   **API SOAP :** En su navegador colocar el siguiente link para ver el contrato:
   ```
   http://localhost:8089/ws/cards.wsdl
   ```

   **API gRPC :** Verificar logs del contenedor:
   ```bash
   docker logs player-api
   ```
   Debería mostrar "Server running on port 8080"

8. Ahora momento de abrir Postman y probar
9. Primero para la autenticación en Postman damos en "New" y agregamos un HTTP, seleccionamos POST y se colocará lo siguiente:

   **URL:** `http://localhost:4445/admin/clients`
   
   **Headers:** `Content-Type: application/json`
   
   **Body (Raw JSON):**
   ```json
   {
     "client_id": "my-client",
     "client_secret": "secret",
     "grant_types": ["client_credentials"],
     "scope": "read write",
     "token_endpoint_auth_method": "client_secret_basic"
   }
   ```
   
   Da en "Send" y debe salir un **201**. Guarde el `client_id` y el `client_secret` ya que se ocuparán.

10. Ahora como esta API ocupa OpenAPI/Swagger es más fácil poder trabajar con todos los métodos. En el navegador colocar el siguiente link:
    ```
    http://localhost:8100/swagger-ui/index.html
    ```
    Una vez en la página, abajo de lo que dice en grande "OpenAPI definition" está en letras pequeñas algo que dice `/v3/api-docs`. Píquele y llevará a un archivo con un JSON. Con `Ctrl + A` copiar todo ese contenido.

11. Una vez copiado todo eso, regresar a Postman y seleccionar donde dice "Import" y pegar ese JSON. Automáticamente le dará la opción para importar los endpoints de la API.

12. Verá la colección como "OpenAPI definitions" y desplegará las carpetas hasta llegar a los endpoints. Al picarle a uno automáticamente se pondrá la URL necesaria para las peticiones. Si no lo hace, estas son:

    **Cartas (SOAP):**
    - `GET /api/v1/cards/{id}` - GetByID
    - `GET /api/v1/cards?page=0&pageSize=10&sortBy=id&sortDirection=ASC` - GetAllCards
    - `POST /api/v1/cards` - CreateCard
    - `PUT /api/v1/cards/{id}` - UpdateCard
    - `PATCH /api/v1/cards/{id}` - PatchCard 
    - `DELETE /api/v1/cards/{id}` - DeleteCard

    **Jugadores (gRPC):**
    - `GET /api/players/{id}` - GetPlayerById
    - `GET /api/players/search?name={nombre}` - GetAllPlayersByName
    - `POST /api/players` - CreatePlayers (enviar array de jugadores)
    - `PUT /api/players` - UpdatePlayer
    - `DELETE /api/players/{id}` - DeletePlayer

13. Ahora para tener el token listo para usarse, seleccione el primer endpoint que requiera usar, y en la sección de "Authorization" estará la parte de "Configure New Token" y rellenará de la forma siguiente:

    - **Token name:** El nombre que quiera para identificarlo
    - **Grant Type:** Client Credentials
    - **Access Token URL:** `http://localhost:4444/oauth2/token`
    - **Client ID:** `my-client` (el que pidió que recordara)
    - **Client Secret:** `secret` (igual que arriba)
    - **Scope:** `read write`
    - **Client Authentication:** Send as Basic Auth header

    Baja y selecciona "Get New Access Token". Le enviará una palomita si se creó bien y selecciona "Use Token".

14. Ahora nada más queda probar los endpoints. Para que funcionen, en cada endpoint que use en la sección de "Authorization" donde dice "Token", seleccione el que creó para que lo deje usarlos, si no, no se podrá ya que ahí es donde se está implementando la verificación con Hydra.

15. **Ejemplos de peticiones para Jugadores (gRPC):**

    **POST - Crear jugadores:**
    ```json
    [
      {
        "name": "MiltonGamer",
        "level": 13,
        "trophies": 6500,
        "clan": "Los Invencibles",
        "battlesPlayed": 1500,
        "coins": 100000,
        "gems": 500
      },
      {
        "name": "ProPlayer99",
        "level": 14,
        "trophies": 7200,
        "clan": "Elite Squad",
        "battlesPlayed": 2000,
        "coins": 150000,
        "gems": 800
      }
    ]
    ```

    **GET - Buscar por nombre:**
    ```
    GET /api/players/search?name=Milton
    ```

    **PUT - Actualizar jugador:**
    ```json
    {
      "id": "673e9a2a4b8e4f2a1c3d5e7f",
      "name": "MiltonPro",
      "level": 14,
      "trophies": 7000,
      "clan": "Champions",
      "battlesPlayed": 1600,
      "coins": 120000,
      "gems": 600
    }
    ```

16. **Pruebas con Git Bash:**

   Usar curl desde Git Bash, primero obtén el token:
    ```bash
    TOKEN=$(curl -s -X POST http://localhost:4444/oauth2/token \
      -H "Content-Type: application/x-www-form-urlencoded" \
      -d "grant_type=client_credentials&client_id=my-client&client_secret=secret&scope=read write" \
      | grep -o '"access_token":"[^"]*' | cut -d'"' -f4)
    ```

    Luego prueba los endpoints:
    ```bash
    # Crear jugadores
    curl -X POST "http://localhost:8100/api/players" \
      -H "Authorization: Bearer $TOKEN" \
      -H "Content-Type: application/json" \
      -d '[{"name": "Milton", "level": 14, "trophies": 7500, "clan": "Legends", "battlesPlayed": 1500, "coins": 100000, "gems": 5000}]'

    # Buscar por ID
    curl -X GET "http://localhost:8100/api/players/673e9a2a4b8e4f2a1c3d5e7f" \
      -H "Authorization: Bearer $TOKEN"

    # Buscar por nombre
    curl -X GET "http://localhost:8100/api/players/search?name=Milton" \
      -H "Authorization: Bearer $TOKEN"
    ```

17. Y listo, API REST con integración SOAP y gRPC terminada 🎉