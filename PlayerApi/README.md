# **API gRPC - Players de Clash Royale**

Esta es una API gRPC desarrollada utilizando el lenguaje Go para aplicar lo aprendido en clase de sistemas distribuidos sobre gRPC, Esta API permite gestionar jugadores de Clash Royale mediante llamadas gRPC y realiza operaciones como crear un jugador, obtenerlo por su id, obtener varios por el nombre, eliminarlos y actualizarlos, incluyendo streaming para creación  de jugadores y streaming del servidor para búsqueda, La API se conecta con MongoDB para los datos que se van a guardar en la base de datos y se utiliza docker para facilitar el uso de esta misma

## **¿Qué utilicé?**

**Lenguaje**: Go 1.23

**Framework gRPC**: google.golang.org/grpc v1.68.1 con Protocol Buffers v3

**Base de datos**: MongoDB latest

**Contenedores**: Docker Compose y Dockerfile 

**ODM**: MongoDB Go Driver v1.17.1

**Pruebas**: Postman con gRPC requests 


## **Arquitectura del Sistema**

- **Cliente gRPC**: Realiza peticiones gRPC (unary, client streaming, server streaming)
- **Puerto 8103**: Servidor gRPC escuchando conexiones
- **Services**: Capa de lógica de negocio con implementación de los endpoints
- **Mappers**: Para que transforme a los modelos que interpreta
- **Models**: La estructura que tendra el player
- **Repositories**: La parte para acceso a los datos
- **Infrastructure**: Configuración de MongoDB y conexión a base de datos
- **MongoDB (puerto 27019)**: Base de datos NoSQL para guardar a los jugadores

## **Metodos gRPC Disponibles**

### **Unary:**
- `GetPlayerById` - Obtiene un jugador por su ID
- `UpdatePlayer` - Actualiza todos los campos de un jugador existente
- `DeletePlayer` - Elimina un jugador por su ID

### **Client Streaming:**
- `CreatePlayers` - Crea múltiples jugadores enviando un stream de requests (client → server)

### **Server Streaming:**
- `GetAllPlayersByName` - Busca jugadores por nombre y devuelve un stream de resultados (server → client)

### **Códigos de estado:**

- `OK` - Operación exitosa
- `NOT_FOUND` - Jugador no encontrado
- `ALREADY_EXISTS` - Jugador con ese nombre ya existe
- `INVALID_ARGUMENT` - Datos inválidos (nombre vacío, level < 1, valores negativos)
- `INTERNAL` - Error interno del servidor o de MongoDB
- `UNAVAILABLE` - Servicio no disponible o MongoDB no accesible

## **Instrucciones para ejecutar el proyecto**

### **Requisitos previos:**

- **Go 1.23** 
- **Docker** o Podman
- **Postman** (con soporte para gRPC) para las pruebas
- **Git** para clonar el repositorio
- **Puertos libres:** 8103 (gRPC), 27019 (MongoDB)

Nota: Si esos puertos no los tiene libres puede usar otros modificando el docker-compose.yaml

### **Pasos de instalación:**

1. Copiar el repo para poder correr la API utilizando la consola de comandos de su elección, ingresar con "cd" a la ruta donde quiera clonar el repositorio

2. Una vez dentro de esa ruta, con el siguiente comando podrá clonar el repositorio:
```bash
git clone https://github.com/MiltonChPe/Sistemas-Didstribuidos-MiltonUzzCh.git
```

3. Una vez descargado para acceder a los archivos del repo deberá usar el comando:
```bash
cd Sistemas-Didstribuidos-MiltonUzzCh
```

   3.1. Como el proyecto sigue en Pull Request es necesario acceder a la rama para poder correrlo, por lo tanto deberá utilizar el comando:
   ```bash
   git checkout feature/PlayerApi
   ```

   NOTA: LA RAMA SERA IGUAL QUE LA API REST, PERO AQUI LOS PASOS SON PARA CORRER SOLAMENTE GRPC, EN EL README DE REST YA LE DEJARE LOS PASOS PARA PODER EJECUTAR YA TODO REST JUNTO CON ESTA Y SOAP

   3.2. Una vez que diga "switched to a new branch" y el nombre de la rama, para acceder al archivo y correr la API deberá usar el comando:
   ```bash
   cd PlayerApi
   ```

4. Una vez dentro para compilar
```bash
go mod download
go build -o playerapi main.go
```

5. Levantar los contenedores con Docker Compose (esto construye la imagen y arranca MongoDB):
```bash
docker-compose up -d --build
```

6. Verificar que todos los contenedores estén corriendo correctamente:
```bash
docker ps
```
Se deberia ver `playerapi` en el puerto 8103 y `mongodb` en el puerto 27019

7.Ahora llegó el momento de probar la API 

9. Abrir Postman y crear una nueva **gRPC Request**

10. En la URL colocar: `localhost:8103`

11. Importar el archivo `.proto`:
   - Click en "Import .proto file"
   - Seleccionar el archivo `PlayerApi/Protos/player.proto`
   - Postman cargará automáticamente todos los métodos disponibles

12. Probar los métodos disponibles:

**GetPlayerById (Unary)**
```json
{
  "id": "673e9a2a4b8e4f2a1c3d5e7f"
}
```

**CreatePlayers (Client Streaming)**
Enviar múltiples mensajes:
```json
{
  "name": "MiltonGamer",
  "level": 13,
  "trophies": 6500,
  "clan": "Los Invencibles",
  "battles_played": 1500,
  "coins": 100000,
  "gems": 500
}
```
```json
{
  "name": "ProPlayer99",
  "level": 14,
  "trophies": 7200,
  "clan": "Elite Squad",
  "battles_played": 2000,
  "coins": 150000,
  "gems": 800
}
```
Luego hacer click en "End streaming" para recibir la respuesta

**UpdatePlayer (Unary)**
```json
{
  "id": "673e9a2a4b8e4f2a1c3d5e7f",
  "name": "MiltonPro",
  "level": 14,
  "trophies": 7000,
  "clan": "Champions",
  "battles_played": 1600,
  "coins": 120000,
  "gems": 600
}
```

**GetAllPlayersByName (Server Streaming)**
```json
{
  "name": "Milton"
}
```
Recibirás un stream de todos los jugadores cuyo nombre contiene "Milton"

**DeletePlayer (Unary)**
```json
{
  "id": "673e9a2a4b8e4f2a1c3d5e7f"
}
```

### **Opción B: gRPCurl**
Si prefieres usar gRPCurl desde terminal:
```bash
grpcurl -plaintext localhost:8103 list

grpcurl -plaintext -d '{"id":"673e9a2a4b8e4f2a1c3d5e7f"}' localhost:8103 playerpb.PlayerService/GetPlayerById
```

13. Y listo, API gRPC terminada wuuu 🎉
