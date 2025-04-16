# Beyond test

Prueba técnica para Beyond

## Descripción de la solución

IMPORTANTE: esta aplicación está configurada para que funcione con SQL Server, por tanto hay que tenerlo
instalado en el equipo.

Arquitectura limpia con dos capas de presentación, una web API y una aplicación de consola.

Ambas se se nutren de los casos de uso de la capa de aplicación.

## Tareas realizadas

1. Crear aplicación en consola en .NET 8 o superior que cumpla
   con los requisitos explicados en la anterior página.
2. Añadir unit tests (en el framework que quieras) para
   validar un happy path, el ejemplo de arriba, y algunos
   test cases de error que no se permitan y tienen que fallar.
3. Convertir la aplicación en consola inicial en una aplicación
   Web API, donde un servidor TodoListServer reciba peticiones para
   gestionar TodoLists o Progressions y devuelva los resultados por pantalla.
4. Desarrollada una solución de .NET que cumple con buenas prácticas.
5. Gestión central de dependencias y versionado de .NET
6. Github action
7. Testing (aunque no me ha dado tiempo a terminarlo) dedicado a cada capa
8. Sistema de migraciones, inyección de dependencias y patrón mediator.
9. Swagger 

## Tareas que no me ha dado tiempo

1. Hacer el cliente web
2. Limpieza de algunas partes del código, por ejemplo, los eventos de dominio
3. 
