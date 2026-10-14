USE [NeuroGGest]
GO

CREATE TABLE [dbo].[Usuario](
	[login] [varchar](50) NOT NULL,
	[clave] [varchar](50) NOT NULL,
	[rol] [varchar](50) NOT NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Estudiante](
	[id] [varchar](20) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[gradoAcademico] [varchar](50) NULL,
	[contactoApod] [varchar](100) NULL,
	[creadoPor] [varchar](50) NULL,
	[fechaCreacion] [datetime] NULL,
	[modificadoPor] [varchar](50) NULL,
	[fechaModific] [datetime] NULL,
	[login] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Especialista](
	[id] [varchar](20) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[especialidad] [varchar](100) NULL,
	[creadoPor] [varchar](50) NULL,
	[fechaCreacion] [datetime] NULL,
	[modificadoPor] [varchar](50) NULL,
	[fechaModific] [datetime] NULL,
	[login] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Taller](
	[id] [varchar](20) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[tipoTaller] [varchar](50) NULL,
	[ocupacionMaxima] [int] NULL,
	[fechaTaller] [datetime] NULL,
	[creadoPor] [varchar](50) NULL,
	[fechaCreacion] [datetime] NULL,
	[modificadoPor] [varchar](50) NULL,
	[fechaModific] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Cita](
	[id] [varchar](20) NOT NULL,
	[id_Estudiante] [varchar](20) NOT NULL,
	[id_Especialista] [varchar](20) NOT NULL,
	[fecha] [date] NULL,
	[hora] [time](7) NULL,
	[estado] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Evaluacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_Cita] [varchar](20) NULL,
	[observaciones] [text] NULL,
	[resultados] [text] NULL,
	[recomendacion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Inscripcion_Taller](
	[id_Taller] [varchar](20) NOT NULL,
	[id_Estudiante] [varchar](20) NOT NULL,
	[asistencia] [varchar](20) NULL,
 CONSTRAINT [PK_Inscripcion] PRIMARY KEY CLUSTERED 
(
	[id_Taller] ASC,
	[id_Estudiante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'admin', N'1', N'Administrador', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP001', N'1', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP002', N'2', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP003', N'3', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP004', N'4', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP005', N'5', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP006', N'6', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP007', N'7', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP008', N'8', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP009', N'9', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'ESP010', N'10', N'Especialista', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST001', N'1', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST002', N'2', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST003', N'3', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST004', N'4', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST005', N'5', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST006', N'6', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST007', N'7', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST008', N'8', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST009', N'9', N'Estudiante', 1)
INSERT [dbo].[Usuario] ([login], [clave], [rol], [activo]) VALUES (N'EST010', N'10', N'Estudiante', 1)
GO

INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST001', N'Ana Torres', N'Primaria', N'Luis Torres', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST001')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST002', N'Carlos Ruiz', N'Secundaria', N'María Ruiz', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST002')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST003', N'Lucía Mendoza', N'Primaria', N'Pedro Mendoza', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST003')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST004', N'Jorge Salas', N'Secundaria', N'Rosa Salas', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST004')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST005', N'Valeria Gómez', N'Primaria', N'Juan Gómez', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST005')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST006', N'Miguel Castro', N'Secundaria', N'Elena Castro', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST006')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST007', N'Daniela Flores', N'Primaria', N'José Flores', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST007')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST008', N'Sebastián Rojas', N'Secundaria', N'Patricia Rojas', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST008')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST009', N'Camila Vargas', N'Primaria', N'Ricardo Vargas', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST009')
INSERT [dbo].[Estudiante] ([id], [nombre], [gradoAcademico], [contactoApod], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'EST010', N'Diego Herrera', N'Secundaria', N'Mónica Herrera', N'admin', CAST(N'2026-06-09T16:30:58.657' AS DateTime), NULL, NULL, N'EST010')
GO

INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP001', N'Sandra Pérez', N'Psicología Infantil', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP001')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP002', N'Ricardo Díaz', N'Psicopedagogía', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP002')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP003', N'Paola Medina', N'Terapia de Lenguaje', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP003')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP004', N'Luis Fernández', N'Psicología Clínica', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP004')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP005', N'María León', N'Orientación Educativa', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP005')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP006', N'José Campos', N'Psicología Infantil', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP006')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP007', N'Andrea Silva', N'Terapia Ocupacional', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP007')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP008', N'Fernando Ortiz', N'Psicopedagogía', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP008')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP009', N'Claudia Ramos', N'Psicología Clínica', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP009')
INSERT [dbo].[Especialista] ([id], [nombre], [especialidad], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific], [login]) VALUES (N'ESP010', N'Gabriel Soto', N'Terapia de Lenguaje', N'admin', CAST(N'2026-06-09T16:30:58.677' AS DateTime), NULL, NULL, N'ESP010')
GO

INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL001', N'Manejo de Emociones', N'Psicológico', 30, CAST(N'2026-06-11T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL002', N'Habilidades Sociales', N'Psicológico', 25, CAST(N'2026-06-12T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL003', N'Técnicas de Estudio', N'Académico', 40, CAST(N'2026-06-13T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL004', N'Comunicación Efectiva', N'Desarrollo Personal', 20, CAST(N'2026-06-14T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL005', N'Control de Estrés', N'Psicológico', 25, CAST(N'2026-06-15T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL006', N'Liderazgo Juvenil', N'Desarrollo Personal', 30, CAST(N'2026-06-16T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL007', N'Orientación Vocacional', N'Académico', 35, CAST(N'2026-06-17T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL008', N'Trabajo en Equipo', N'Desarrollo Personal', 30, CAST(N'2026-06-18T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL009', N'Autoestima y Motivación', N'Psicológico', 20, CAST(N'2026-06-19T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
INSERT [dbo].[Taller] ([id], [nombre], [tipoTaller], [ocupacionMaxima], [fechaTaller], [creadoPor], [fechaCreacion], [modificadoPor], [fechaModific]) VALUES (N'TAL010', N'Planificación del Tiempo', N'Académico', 40, CAST(N'2026-06-20T00:00:00.000' AS DateTime), N'admin', CAST(N'2026-06-09T16:30:58.703' AS DateTime), NULL, NULL)
GO

INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT001', N'EST001', N'ESP001', CAST(N'2026-06-01' AS Date), CAST(N'09:00:00' AS Time), N'Realizada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT002', N'EST002', N'ESP002', CAST(N'2026-06-02' AS Date), CAST(N'10:00:00' AS Time), N'Realizada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT003', N'EST003', N'ESP003', CAST(N'2026-06-03' AS Date), CAST(N'11:00:00' AS Time), N'Realizada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT004', N'EST004', N'ESP004', CAST(N'2026-06-04' AS Date), CAST(N'14:00:00' AS Time), N'Programada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT005', N'EST005', N'ESP005', CAST(N'2026-06-05' AS Date), CAST(N'15:00:00' AS Time), N'Realizada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT006', N'EST006', N'ESP006', CAST(N'2026-06-06' AS Date), CAST(N'08:30:00' AS Time), N'Realizada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT007', N'EST007', N'ESP007', CAST(N'2026-06-07' AS Date), CAST(N'09:30:00' AS Time), N'Programada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT008', N'EST008', N'ESP008', CAST(N'2026-06-08' AS Date), CAST(N'10:30:00' AS Time), N'Realizada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT009', N'EST009', N'ESP009', CAST(N'2026-06-09' AS Date), CAST(N'13:00:00' AS Time), N'Realizada')
INSERT [dbo].[Cita] ([id], [id_Estudiante], [id_Especialista], [fecha], [hora], [estado]) VALUES (N'CIT010', N'EST010', N'ESP010', CAST(N'2026-06-10' AS Date), CAST(N'16:00:00' AS Time), N'Programada')
GO

SET IDENTITY_INSERT [dbo].[Evaluacion] ON
GO

INSERT [dbo].[Evaluacion] ([id], [id_Cita], [observaciones], [resultados], [recomendacion]) VALUES (1, N'CIT001', N'Ansiedad leve', N'Nivel adecuado', N'Seguimiento mensual')
INSERT [dbo].[Evaluacion] ([id], [id_Cita], [observaciones], [resultados], [recomendacion]) VALUES (2, N'CIT002', N'Dificultad de concentración', N'Rendimiento regular', N'Aplicar técnicas de estudio')
INSERT [dbo].[Evaluacion] ([id], [id_Cita], [observaciones], [resultados], [recomendacion]) VALUES (3, N'CIT003', N'Problemas de pronunciación', N'Mejoría progresiva', N'Continuar terapia')
INSERT [dbo].[Evaluacion] ([id], [id_Cita], [observaciones], [resultados], [recomendacion]) VALUES (4, N'CIT005', N'Estrés académico', N'Nivel moderado', N'Reducir carga académica')
INSERT [dbo].[Evaluacion] ([id], [id_Cita], [observaciones], [resultados], [recomendacion]) VALUES (5, N'CIT006', N'Adaptación escolar adecuada', N'Buen progreso', N'Mantener seguimiento')
INSERT [dbo].[Evaluacion] ([id], [id_Cita], [observaciones], [resultados], [recomendacion]) VALUES (6, N'CIT008', N'Habilidades sociales satisfactorias', N'Buen desempeño', N'Participar en talleres')
INSERT [dbo].[Evaluacion] ([id], [id_Cita], [observaciones], [resultados], [recomendacion]) VALUES (7, N'CIT009', N'Autoestima baja', N'Nivel mejorable', N'Asesoría psicológica')
GO

SET IDENTITY_INSERT [dbo].[Evaluacion] OFF
GO

INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL001', N'EST001', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL001', N'EST005', N'Inscrito')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL001', N'EST009', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL002', N'EST002', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL002', N'EST008', N'Inscrito')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL003', N'EST003', N'Faltó')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL003', N'EST006', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL003', N'EST010', N'Inscrito')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL004', N'EST004', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL004', N'EST007', N'Inscrito')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL005', N'EST001', N'Inscrito')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL005', N'EST005', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL006', N'EST006', N'Faltó')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL006', N'EST008', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL007', N'EST007', N'Inscrito')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL007', N'EST010', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL008', N'EST002', N'Inscrito')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL008', N'EST004', N'Asistió')
INSERT [dbo].[Inscripcion_Taller] ([id_Taller], [id_Estudiante], [asistencia]) VALUES (N'TAL008', N'EST008', N'Asistió')
GO

ALTER TABLE [dbo].[Evaluacion] ADD UNIQUE NONCLUSTERED 
(
	[id_Cita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Especialista] FOREIGN KEY([id_Especialista])
REFERENCES [dbo].[Especialista] ([id])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Especialista]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Estudiante] FOREIGN KEY([id_Estudiante])
REFERENCES [dbo].[Estudiante] ([id])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Estudiante]
GO
ALTER TABLE [dbo].[Especialista]  WITH CHECK ADD  CONSTRAINT [FK_Especialista_Usuario] FOREIGN KEY([login])
REFERENCES [dbo].[Usuario] ([login])
GO
ALTER TABLE [dbo].[Especialista] CHECK CONSTRAINT [FK_Especialista_Usuario]
GO
ALTER TABLE [dbo].[Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Estudiante_Usuario] FOREIGN KEY([login])
REFERENCES [dbo].[Usuario] ([login])
GO
ALTER TABLE [dbo].[Estudiante] CHECK CONSTRAINT [FK_Estudiante_Usuario]
GO
ALTER TABLE [dbo].[Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_Evaluacion_Cita] FOREIGN KEY([id_Cita])
REFERENCES [dbo].[Cita] ([id])
GO
ALTER TABLE [dbo].[Evaluacion] CHECK CONSTRAINT [FK_Evaluacion_Cita]
GO
ALTER TABLE [dbo].[Inscripcion_Taller]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Estudiante] FOREIGN KEY([id_Estudiante])
REFERENCES [dbo].[Estudiante] ([id])
GO
ALTER TABLE [dbo].[Inscripcion_Taller] CHECK CONSTRAINT [FK_Inscripcion_Estudiante]
GO
ALTER TABLE [dbo].[Inscripcion_Taller]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Taller] FOREIGN KEY([id_Taller])
REFERENCES [dbo].[Taller] ([id])
GO
ALTER TABLE [dbo].[Inscripcion_Taller] CHECK CONSTRAINT [FK_Inscripcion_Taller]
GO
