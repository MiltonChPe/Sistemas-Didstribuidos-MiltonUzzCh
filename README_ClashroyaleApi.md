## **Proyecto SOAP Cartas de Clash Royale**



###### **Que utilice?**

**Lenguaje**: Java 17

**Base de datos**: MariaDB 10.11

**Framework**: Spring Boot 3.1.0 para la infraestructura de la API, usando MAVEN como herramienta para iniciar el proyecto con java y descargar las librerías y dependencias necesarias

**Contenedores**: Con Docker 

**Pruebas**: Insomnia

**Migraciones**: Flyway 

**Implementación del ORM:** Hibernate



###### **Explicacion de la API**

Para crear esta API soap utilizando java comencé creando el framework con la infraestructura de spring boot, utilizando visual estudio code que permitia inicializar el spring con tipo Maven Project para java, eligiendo dependencias y descargando las extensiones para que todo funcioanra correctamente. Creando primero el card.xsd para comienzo del contract, y el pom.xml para las dependencias, versión del lenguaje y la estructura básica, después empezar a crear las carpetas y archivos basándonos en como lo hacíamos en clase. Primero archivos necesarios para lo que pide que es el WebService para java y trabajar con el xsd para el contrato y el ClashroyaleApplication para iniciar la API con lo que pide springboot y el properties como el appsettigns para la base de datos, el Docker file para la imagen que usara, y la migración con flyway, después la entidad principal que fue la CardEntity y el CardRepository que era donde se accedia a los datos pero en vez de hacerlo como en .NET describiendo los métodos fue con los que venían incluidos usando un extend, después hacer los DTOS para crear lo que usa el api para los datos para recibir, después los mappers que es para convertir las entidades y todo para traducir a dtos, y los validadores para que tenga limites en las funciones en caso de errores o datos invalidos para que funcionara correctamente y sin repetidos en la bd, y los Services para la implementación de la lógica de negocio con lo que trabaja la API, y los endpoints que son para las peticiones al momento de hacer las pruebas.



###### **INSTRUCCIONES PARA HACERLA FUNCIONAR**

***Antes de correrla se debe tener lo siguiente:*** 

**-**Java 17

-Docker o podman funcionando para los contenedores

-Insomnia instalado para las pruebas 

-Tener en cuenta que los puertos para la bd 3312:3306 y para la api 8089:8080 deben estar libres, si se quieren usar otros cambiar el archivo de Docker compose en la parte de PORTS de las respectivas api y base de datos, y modificar el application.properties en el spring.datasource.url donde diga el puerto cambiarlo al requerido



1-Copiar/descargar o copiar el repo con todos los archivos de la API

2-Copiar la ruta de acceso donde se haya guardado la carpeta principal de la API

3-Usar cualquier consola de comandos de eleccion y utilizando el comando cd y la ruta entre comillas

4-Despues como primer paso levantar la base de datos ocupando: docker-compose up clash-royale-db -d 

Nota: si ocupa podman solo cambiar Docker por podman en todos los comandos pertinentes 

5-Hacer la compilación de la API con el comando: .\\mvnw.cmd clean package -DskipTests

6-Hacer build para la imagen de la API con el comando: docker-compose build --no-cache

7-Crear y correr el contenedor: docker-Compose up -d

8-Hacer Docker ps para comprobar que los contenedores se esten ejecutando correctamente

9-Para revisar el contrato en tu navegador colocar el link: http://localhost:8089/ws/cards.wsdl (si se cambio el puerto solo cambiar el 8089 por el utilizado)

10-Si logra verse el contrato significa que esta funcionando entonces hora de hacer las pruebas

11-Abrir insomnia y en donde marca import dar click, seleccionar la opcion de URL y colocar: http://localhost:8089/ws/cards.wsdl

12-Dar donde dice Scan y si aparece 2 request significa que si funciona, darle en import, y después seleccionar la colección creada, donde se verán los dos endpoints createcard y getcardbyid

13-Seleccionar el createcard y donde dice "POST" colocar: localhost:8089/ws/cards.wsdl y seleccionar el apartado body, se mostrara el mensaje soap y donde estan los mandatory podras colocar los datos de la carta de Clash royale que quieres creaer, y al dar send deberá salir un 200 OK y mostrar los datos de la carta, si muestra error con un 500 se señalara que clase de error hubo con un mensaje

14-Para verificar que se creara la carta debes seleccionar en el endpoint getbyid y en "POST" nuevamente colocar: localhost:8089/ws/cards.wsdl, colocar el id de la carta en el mandatory y dar send, deberá aparecer un 200 OK y la información de la carta con ese ID, si marca un error se mostrara un mensaje del tipo de error que surgio 

15- y wuuu Api funcional

16- Para apagar el API usar el comando: docker-compose down 









