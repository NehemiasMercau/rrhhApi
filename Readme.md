# Ejercicio propuesto - POSSUMUS
## API de RRHH

### Herramientas Utilizadas
- .NetCore v7.0
- MSSQL Server
- Migraciones
- EntityFramework
- Swagger
- Serilog

## Consigna
*Desarrollar una API Rest que permita gestionar las siguientes entidades:*
- **Candidato**: nombre, apellido, fecha de nacimiento, email, teléfono
- **Empleo**: id, nombre-empresa, periodo
- endpoint para listar los candidatos con sus empleos. GET
- endpoint para crear un candidato junto con a sus empleos. POST
- endpoint para modificar un candidato. PUT
- endpoint para eliminar un candidato. DELETE
- Aplicar algún mecanismo de log para registrar las acciones en un archivo externo.

* * *

## Desarrollo
### Requerimientos
Para implementar el proyecto, es necesario tener instalado.
- MSSQL Server
- .NET SKD 7.0.100

#### Requerimientos. Migrations y Base de Datos
Si *dotnet ef* no esta instalado, instalarlos de forma global:
```sh
  dotnet tool install --global dotnet-ef
```
Ejecutar el siguiente comando para crear la base de datos según el Schema del proyecto
```sh
  dotnet ef database update
```
En el caso de que no se pueda crear la base de datos RRHH_DB con la herramienta *dotnet ef*, en MSSQL crear una nueva base de datos con el nombre RRHH_DB y correr el *script.sql* que se encuentra en la raíz del proyecto. 

#### Requerimientos. Migrations y Base de Datos. Cadena de Conexión
- Crear una instancia de base de datos, con SQLExpress (localhost\SQLEXPRESS), con lo que la cadena de conexión por defecto establecida en el proyecto, va a tomar la instancia creada según: (.\SQLExpress).

### Instalación

```sh
git clone https://github.com/NehemiasMercau/rrhhApi.git
cd rrhhApi
dotnet watch run
```
> Abrir navegador, ir: http://localhost:5029/swagger/index.html
> Abrir navegador, ir: http://localhost:5029/api/[endpoint]

_Los logs se guardarán en un archivo de texto con nombre '[Año][Mes][Día]' en la raíz del proyecto_