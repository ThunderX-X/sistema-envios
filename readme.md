# Sistema de envios

Este es el Web API de un sistema de envios que permite el manejo de cientes, envios y puntos de entrega.

Este proyecto ha sido desarrollado utilizando:

- .NET 6 con C#
- Estilo de arquitectura REST
- Arquitectura de microservicios
- API Gateway con ocelot
- Docker
- MongoDB

## Configuración

Cada microservicio (Excluyendo el gateway) requiere que se configuren las siguientes variables de entorno: 

- **MONGO_SERVER**
- **MONGO_DATABASE**
- **MONGO_COLLECTION**
- **LOG_LEVEL**

Adicionalmente cada microservicio tiene un `appsettings.json` en el cual se encuentran secciones con el siguiente formato: `"nombreSeccion": "{VARIABLE_ENTORNO}"`

Estas secciones funcionan como un listado de las variables de entorno especificas que requiere el servicio.

## Build de contenedor

Este proyecto ha sido construido para funcionar en conjunto con Docker, dentro de cada servicio existe un `Dockerfile` con el cual se puede construir su contenerdor.

Para construir un contenedor se deben seguir los siguientes pasos:

1. Dirigirse al directorio que contiene el servicio

    ```sh
    cd Services/Clients.Service
    ```

2. Construir el contenedor utilizando el Dockerfile

   ```sh
    docker build -t services/clients:0.1.0 .
    ```

### Prerequisitos

Todas las dependencias del proyecto son manejadas por medio de nuget, para instalarlas es necesario ejecutar el siguiente comando del sdk de .net:

```sh
dotnet restore
```

## Deploy

Aprovechando los beneficios de los contenedores este proyecto puede ser desplegado muy facilmente, actualmente este proyecto se encuentra desplegado en AWS en la siguiente URL:
[API Rest Sistema Envios]
