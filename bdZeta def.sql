USE [bdZeta]
GO
/****** Object:  Table [dbo].[Actividades]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actividades](
	[id_actividad] [int] IDENTITY(1,1) NOT NULL,
	[nombre_actividad] [varchar](100) NOT NULL,
	[coef_actividad] [decimal](3, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_actividad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActividadesUsuario]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActividadesUsuario](
	[id_actividad_usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_actividad] [int] NULL,
	[frecuencia_semanal] [int] NOT NULL,
 CONSTRAINT [PK__Activida__BB339B35F3B25A50] PRIMARY KEY CLUSTERED 
(
	[id_actividad_usuario] ASC,
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[id_articulo] [int] NOT NULL,
	[nombre_articulo] [varchar](50) NOT NULL,
	[img_portada_articulo] [varchar](50) NOT NULL,
	[autor] [varchar](50) NOT NULL,
	[texto_articulo] [varchar](max) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[id_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriasArticulosBlog]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriasArticulosBlog](
	[id_categoria] [int] NOT NULL,
	[id_articulo] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriasDeArticulos]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriasDeArticulos](
	[id_categoria] [int] NOT NULL,
	[nombre_categoria] [varchar](50) NOT NULL,
	[descripcion_categoria] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CategoriasDeArticulos] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comentarios]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentarios](
	[id_comentario] [int] IDENTITY(1,1) NOT NULL,
	[id_post] [int] NOT NULL,
	[id_usuario] [int] NULL,
	[contenido_comentario] [text] NOT NULL,
	[fecha_comentario] [datetime] NOT NULL,
 CONSTRAINT [PK__Comentar__1BA6C6F440DA2185] PRIMARY KEY CLUSTERED 
(
	[id_comentario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImagenesDeArticulos]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImagenesDeArticulos](
	[id_imagen] [int] NOT NULL,
	[src_imagen] [nchar](10) NOT NULL,
 CONSTRAINT [PK_ImagenesDeArticulos] PRIMARY KEY CLUSTERED 
(
	[id_imagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interacciones]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interacciones](
	[id_interaccion] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_post] [int] NULL,
	[id_comentario] [int] NULL,
	[tipo_interaccion] [smallint] NULL,
	[fecha_interaccion] [datetime] NOT NULL,
 CONSTRAINT [PK__Interacc__0152426C51FA4F3F] PRIMARY KEY CLUSTERED 
(
	[id_interaccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IntermediaArticulosImagenes]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IntermediaArticulosImagenes](
	[id_articulo] [int] NOT NULL,
	[id_imagen] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[id_log] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[accion] [varchar](200) NOT NULL,
	[fecha_accion] [datetime] NOT NULL,
	[descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_log] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notificaciones]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notificaciones](
	[id_notificacion] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[mensaje] [text] NOT NULL,
	[fecha_notificacion] [datetime] NOT NULL,
	[leida] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_notificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patologias]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patologias](
	[id_patologia] [int] IDENTITY(1,1) NOT NULL,
	[nombre_patologia] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_patologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[id_post] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[titulo_post] [varchar](200) NOT NULL,
	[contenido_post] [text] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[version] [int] NULL,
	[imagen_url] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_post] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[fecha_registro] [date] NULL,
	[ultima_sesion] [datetime] NULL,
	[edad] [int] NULL,
	[sexo] [bit] NULL,
	[peso] [float] NULL,
	[altura] [float] NULL,
	[password] [varchar](100) NULL,
 CONSTRAINT [PK__Usuarios__4E3E04AD54E50ACD] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Usuarios__AB6E6164BCC9C0FA] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuariosPatologias]    Script Date: 8/11/2024 15:57:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuariosPatologias](
	[id_usuario] [int] NOT NULL,
	[id_patologia] [int] NOT NULL,
 CONSTRAINT [PK_UsuariosPatologias] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_patologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Notificaciones] ADD  DEFAULT ((0)) FOR [leida]
GO
ALTER TABLE [dbo].[Posts] ADD  DEFAULT ((1)) FOR [version]
GO
ALTER TABLE [dbo].[ActividadesUsuario]  WITH CHECK ADD  CONSTRAINT [FK__Actividad__id_ac__5629CD9C] FOREIGN KEY([id_actividad])
REFERENCES [dbo].[Actividades] ([id_actividad])
GO
ALTER TABLE [dbo].[ActividadesUsuario] CHECK CONSTRAINT [FK__Actividad__id_ac__5629CD9C]
GO
ALTER TABLE [dbo].[ActividadesUsuario]  WITH CHECK ADD  CONSTRAINT [FK__Actividad__id_us__571DF1D5] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[ActividadesUsuario] CHECK CONSTRAINT [FK__Actividad__id_us__571DF1D5]
GO
ALTER TABLE [dbo].[CategoriasArticulosBlog]  WITH CHECK ADD  CONSTRAINT [FK_CategoriasArticulosBlog_Articulos] FOREIGN KEY([id_articulo])
REFERENCES [dbo].[Articulos] ([id_articulo])
GO
ALTER TABLE [dbo].[CategoriasArticulosBlog] CHECK CONSTRAINT [FK_CategoriasArticulosBlog_Articulos]
GO
ALTER TABLE [dbo].[CategoriasArticulosBlog]  WITH CHECK ADD  CONSTRAINT [FK_CategoriasArticulosBlog_CategoriasDeArticulos] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[CategoriasDeArticulos] ([id_categoria])
GO
ALTER TABLE [dbo].[CategoriasArticulosBlog] CHECK CONSTRAINT [FK_CategoriasArticulosBlog_CategoriasDeArticulos]
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD  CONSTRAINT [FK__Comentari__id_po__59FA5E80] FOREIGN KEY([id_post])
REFERENCES [dbo].[Posts] ([id_post])
GO
ALTER TABLE [dbo].[Comentarios] CHECK CONSTRAINT [FK__Comentari__id_po__59FA5E80]
GO
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD  CONSTRAINT [FK__Comentari__id_us__5AEE82B9] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Comentarios] CHECK CONSTRAINT [FK__Comentari__id_us__5AEE82B9]
GO
ALTER TABLE [dbo].[Interacciones]  WITH CHECK ADD  CONSTRAINT [FK__Interacci__id_co__5BE2A6F2] FOREIGN KEY([id_comentario])
REFERENCES [dbo].[Comentarios] ([id_comentario])
GO
ALTER TABLE [dbo].[Interacciones] CHECK CONSTRAINT [FK__Interacci__id_co__5BE2A6F2]
GO
ALTER TABLE [dbo].[Interacciones]  WITH CHECK ADD  CONSTRAINT [FK__Interacci__id_po__5CD6CB2B] FOREIGN KEY([id_post])
REFERENCES [dbo].[Posts] ([id_post])
GO
ALTER TABLE [dbo].[Interacciones] CHECK CONSTRAINT [FK__Interacci__id_po__5CD6CB2B]
GO
ALTER TABLE [dbo].[Interacciones]  WITH CHECK ADD  CONSTRAINT [FK__Interacci__id_us__5DCAEF64] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Interacciones] CHECK CONSTRAINT [FK__Interacci__id_us__5DCAEF64]
GO
ALTER TABLE [dbo].[IntermediaArticulosImagenes]  WITH CHECK ADD  CONSTRAINT [FK_IntermediaArticulosImagenes_Articulos] FOREIGN KEY([id_articulo])
REFERENCES [dbo].[Articulos] ([id_articulo])
GO
ALTER TABLE [dbo].[IntermediaArticulosImagenes] CHECK CONSTRAINT [FK_IntermediaArticulosImagenes_Articulos]
GO
ALTER TABLE [dbo].[IntermediaArticulosImagenes]  WITH CHECK ADD  CONSTRAINT [FK_IntermediaArticulosImagenes_ImagenesDeArticulos] FOREIGN KEY([id_imagen])
REFERENCES [dbo].[ImagenesDeArticulos] ([id_imagen])
GO
ALTER TABLE [dbo].[IntermediaArticulosImagenes] CHECK CONSTRAINT [FK_IntermediaArticulosImagenes_ImagenesDeArticulos]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK__Logs__id_usuario__5EBF139D] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK__Logs__id_usuario__5EBF139D]
GO
ALTER TABLE [dbo].[Notificaciones]  WITH CHECK ADD  CONSTRAINT [FK__Notificac__id_us__5FB337D6] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Notificaciones] CHECK CONSTRAINT [FK__Notificac__id_us__5FB337D6]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK__Posts__id_usuari__628FA481] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK__Posts__id_usuari__628FA481]
GO
ALTER TABLE [dbo].[UsuariosPatologias]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosPatologias_Patologias] FOREIGN KEY([id_patologia])
REFERENCES [dbo].[Patologias] ([id_patologia])
GO
ALTER TABLE [dbo].[UsuariosPatologias] CHECK CONSTRAINT [FK_UsuariosPatologias_Patologias]
GO
ALTER TABLE [dbo].[UsuariosPatologias]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosPatologias_Usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[UsuariosPatologias] CHECK CONSTRAINT [FK_UsuariosPatologias_Usuarios]
GO
