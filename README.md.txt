# NeuroGest

Aplicación de escritorio para gestionar citas, evaluaciones y talleres de estudiantes.

## Tecnologías
- C# y Windows Forms
- Entity Framework 6
- SQL Server

## Arquitectura (3 capas)
- **CapaDatos:** acceso a datos y entidades.
- **CapaNegocio:** lógica de negocio.
- **Presentacion:** formularios de la aplicación.

## Funcionalidades
- Inicio de sesión con roles: administrador, especialista y estudiante.
- Gestión de estudiantes, especialistas, citas, evaluaciones y talleres.
- Dashboards y reportes con gráficos de citas y asistencia a talleres.

## Cómo ejecutarlo
1. Ejecuta el script `sql/NeuroGest_Script.sql` en SQL Server para crear la base de datos `NeuroGGest`.
2. Abre `NeuroGest.sln` en Visual Studio.
3. Revisa la cadena de conexión en `App.config` y ajústala a tu servidor.
4. Ejecuta el proyecto `Presentacion`.

## Nota
Proyecto académico del curso [nombre del curso]. Los datos incluidos en el script son de prueba.