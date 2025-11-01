# **API REST - Cartas de Clash conectado con SOAP**

Esta es una API REST que desarrolle utilizando el lenguaje Java para aplicar lo aprendido en clase de dsitribuidos sobre las API rest, ne la cual ocupe Spring Boot que consume el servicio SOAP clashroyale que es una API creada en el parcial anteriror, esta nueva Api REST permite gestionar las cartas de Clash Royale y realizar acciones como las que se podian desde el Api Soap, agregando nuevas funcionaldiades a la par que modifique el api Soap para adaptarse y poder ser consumido, esta Api implementa autenticación y autorización mediante OAuth2 con Ory Hydra, tambien cache con Redis, y maneja errores SOAP traduciendolos a codigos HTTP apropiados

## **¿Qué utilicé?**

**Lenguaje**: Java 17

**Framework**: Spring Boot 3.3.0 con Maven como herramienta de gestión de dependencias

**Seguridad**: OAuth2 Resource Server con Ory Hydra v2.2 y utilizando los JWT tokens

**Cache**: Redis 7-alpine para almacenamiento en memoria

**Base de datos**: 
- MariaDB 10.11 (El que usa SOAP)
- PostgreSQL 16-alpine (usada por Ory Hydra)

**SOAP**: Spring Web Services con WebServiceTemplate

**Contenedores**: Docker Compose y docker file para el control central de todo lo necesario

**Pruebas**: Utilizando Postman 

**ORM**: Hibernate (en el servicio SOAP)

**Especificacion de la Api**: OpenAPI/Swagger

## **Arquitectura del Sistema**

La api sigue lo siguiente:
-El cliente postman tiene las peticiones HTTP rest y OAuth2 para la validacion con el token
-Despues el Api ocupando el puerto 8100 manda la peticion
-Pasa por el controller que es donde se maneja la parte de errores y sintaxis mas de rest
-Despues pasa por el service donde esta la logica de negocio
-Despues por la parte del Gateway donde se traduce con los mappers y se ocupa la logica y consumir a SOAP
-Y se toma de la infraestructura para que tome lo que tiene SOAP
-El servicio SOAP aporta con las funciones y todo lo que se entrego el parcial pasado
-Se conecta con la base de datos de mariadb 
-Regresa y con el controller y se devuelve respuesta de la peticion


## **Endpoints Disponibles**
[GET] - GetbyID
[GET] - GetAllCards (paginado)
[POST] - createCard
[PUT] - UpdateCard
[PATCH] - PatchCard (que es un update pero especifico)
[DELETE] - deleteCard

### **Codigos de respuesta de HTTP:**

- `200 OK` - operacion exitosa (GET, PUT, PATCH)
- `201 Created` - se creo correctamente (POST)
- `204 No Content` - eliminado correctamente (DELETE)
- `400 Bad Request` - Datos invalidos (elixir > 10, type/rarity incorrectos)
- `401 Unauthorized` - Sin autorizacion por token no valido
- `403 Forbidden` - Token valido pero sin scope bien
- `404 Not Found` - Carta no encontrada
- `409 Conflict` - Carta con ese nombre ya existe
- `502 Bad Gateway` - Servicio SOAP no disponible o error inesperado


## **Instrucciones para ejecutar el proyecto**

### **Requisitos previos:**

- **Java 17** instalado
- **Docker Desktop** o Podman 
- **Postman** para pruebas
- **Git** para clonar el repositorio
- **Puertos libres:** 8100 (REST), 8089 (SOAP), 6379 (Redis), 3312 (MariaDB), 4444/4445 (Hydra), 5432 (PostgreSQL)

Nota: Si esos puertos no los tiene libres puede usar otros pero pues es cambiando el docker-compose

### **Pasos de instalación:**

1-Copiar el repo para poder correr la api utilizando la consola de comandos de su eleccion ingresar con "cd" a la ruta donde quiera clonar el repositorio

2-Una vez dentro de esa ruta, con el siguiente comando podra clonar el repositorio: git clone https://github.com/MiltonChPe/Sistemas-Didstribuidos-MiltonUzzCh.git

3-Una vez descargado para acceder a los archivos del repo debera usar el comando: cd Sistemas-Didstribuidos-MiltonUzzCh

    3.1-Como el proyecto sigue en Pull Request es necesario acceder a la rama para poder correrlo, por lo tanto debera utilizar el comando:  git checkout feature/ClashCardApi

    3.2-Una vez que diga "switched to a new branch" y el nombre de la rama, para acceder al archivo y correr la API debera usar el comando cd clashcardapi

4-Una vez dentro antes que nada primero es compilar el poyecto para que se vea que todo funciona bien utilizando: 
.\mvnw.cmd clean package -DskipTests

5- Una vez listo se levantan los contenedores y como todo esta centralizado a un docker compose solamente se ocupa el siguiente comando:
docker-compose up -d --build clashcard-api

6-Se hace un docker ps para ver que todos los contenedores esten corriendo correctamente

7-Para verificar que funciona el API SOAP,esto es opcional pero si quiere hacerlo, en su navegador colocar el siguiente link para ver el contraro:
http://localhost:8089/ws/cards.wsdl

8-Ahora llego el momento de abrir postman y comenzar con las pruebas

9-Primero para la autorizacion en postman damos en new y agregamos un HTTP, seleccionamos POST y se colocara lo siguiente:
En la parte de URL: http://localhost:4445/admin/clients
En Headers en Content-Type: application/json
En body se usa un Raw json y se coloca lo siguiente: 
{
  "client_id": "clash-royale-client",
  "client_secret": "super-secret-123",
  "grant_types": ["client_credentials"],
  "scope": "read write",
  "token_endpoint_auth_method": "client_secret_basic"
}
Da en send y debe salir un 201, guarde client_id y el secret ya que se ocuparan

10- Ahora como este Api ocupa OpenApi/Swagger es mas facil poder trabajar con todos los metodos de esta, en el navegador colocar el siguiente link:
http://localhost:8100/swagger-ui/index.html // Con el puerto que se haya creado si es que se cambio
Y una vez en la pagina abajo de lo que dice en grande "OpenAPI definition" esta en letras pequeñas algo que dice /v3/api-docs, picarle y llevara a un archivo con un json, con ctrl + a copiar todo ese contenido 

11- Una vez copiado todo eso regresar a postman y en seleccionar donde dice import y pegar ese json, automaticamente le dara la opcion para importar los endpoints del API

12-Vera la coleccion como OpenApi definitions y desplegara las carpetas hasta llegar a los endpoints, al picarle a uno automaticamente se pondra el URL necesario para las peticiones, si no lo hace estas son: 

GET `/api/v1/cards/{id}` //GetbyID
GET `/api/v1/cards?page=0&pageSize=10&sortBy=id&sortDirection=ASC`  //GetAllCards
POST `/api/v1/cards` //CreateCard
PUT `/api/v1/cards/{id}` //UpdateCard
PATCH `/api/v1/cards/{id}` //patchCard 
DELETE `/api/v1/cards/{id}`//Delete card

13- Ahora para tener el token listo para usarse, seleccione el primer endpoint que requiera usar, y en la seccion de Authorization estara la parte de configure new token y rellenara de la forma siguiente
Token name: El nombre que quiera para identificarlo
GrantType: Client credentials
Acess Token URL: http://localhost:4444/oauth2/token
Client ID: El que se pidio que recordara, si no igual esta en el post que hizo cuando lo creo
Client secret: Igual mismo que arriba
Scope: read write
Client Authentication: Send as Basic Auth header

y baja y selecciona Get New Access Token y le enviara una palomita si se creo bien y selecciona use token

14- Ahora nadamas queda probar los endpoints, para que funcionen en cada endpoint que use en la seccion de Authorization donde dice Token seleccione el que creo para que lo deje usarlos si no no se podra ya que ahi es donde se esta implementado la verificacion con hydra 

15- Y listo Api rest terminada wuuu :