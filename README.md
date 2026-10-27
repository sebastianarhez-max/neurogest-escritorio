# NeuroGest

Aplicación de escritorio para gestionar citas, evaluaciones y talleres de estudiantes.


## Arquitectura (3 capas)
- **CapaDatos:** acceso a datos y entidades.
- **CapaNegocio:** lógica de negocio.
- **Presentacion:** formularios de la aplicación.


## Cómo ejecutarlo
1. Ejecuta el script `sql/NeuroGest_Script.sql` en SQL Server para crear la base de datos `NeuroGGest`.
2. Abre `NeuroGest.sln` en Visual Studio.
3. Revisa la cadena de conexión en `App.config` y ajústala a tu servidor.
4. Ejecuta el proyecto `Presentacion`.


## Tecnologías

- **Lenguaje:** C#
- **Interfaz:** Windows Forms
- **Acceso a datos:** Entity Framework
- **Base de datos:** SQL Server
- **Arquitectura:** 3 capas (presentación, lógica de negocio y datos)

## Funcionalidades

- Gestión de citas, evaluaciones y talleres de estudiantes.
- Acceso por roles.
