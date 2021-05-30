/*
** Author:		Mairon Andres Florez
** Date:		2021-05-29
** Description:	Base de datos para prueba
** Sql Server 2019 Express (Modern_Spanish_CI_AS)
*/

--- Crear base de datos para el proyecto nombrada inalambria_redsocial
USE MASTER 
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'inalambria_redsocial')
BEGIN
	CREATE DATABASE inalambria_redsocial
	COLLATE Modern_Spanish_CI_AS
END
GO

USE inalambria_redsocial
GO
/*
El cliente está interesado en construir una plataforma que a manera de
red social establezca un canal de comunicación entre los diferentes
interlocutores del mercado de poseedores de mascotas y así acerque a
los productores de alimentos, servicios médicos veterinarios, guarderías
y networking entre otros poseedores de mascotas.
Las publicaciones incluirán texto (con formato por defecto o con formato
especificado por el usuario), imágenes, voz, video, archivos.
El cliente requiere que se construyan los módulos mensajería, publicidad
(banners, publicaciones sugeridas en el tablero), área comercial (ventas
de los usuarios normales, ventas de grandes proveedores).
	
Publicaciones: 
	a) texto (con formato por defecto o con formato especificado por el usuario), 
	b) imágenes
	c) voz 
	d) video
	e) archivos.

Modulos:
	a) Mensajeria (¿¿chats??, es a modo red social !!)
	b) Publicidad
		- banners
		- publicaciones sugeridas
	c) Comercial (¿¿carrito de compras??)
		- ventas usuario normal  
		- ventas usuario proveedor
*/

DROP TABLE IF EXISTS rsocial_Publicidad
GO
DROP TABLE IF EXISTS rsocial_Mensajeria
GO
DROP TABLE IF EXISTS rsocial_MensajeriaEstado
GO
DROP TABLE IF EXISTS rsocial_PublicacionArchivo
GO
DROP TABLE IF EXISTS rsocial_PublicacionVideo
GO 
DROP TABLE IF EXISTS rsocial_PublicacionImagen
GO 
DROP TABLE IF EXISTS rsocial_Publicacion
GO
DROP TABLE IF EXISTS rsocial_PublicacionEstado
GO
DROP TABLE IF EXISTS rsocial_Usuario
GO

CREATE TABLE rsocial_Usuario
(
	Id INT NOT NULL IDENTITY(1, 1),
	Nombre VARCHAR(150) NOT NULL,
	Apellido VARCHAR(150),
	Telefono VARCHAR(50),
	Email VARCHAR(250) NOT NULL,
	Contrasena VARCHAR(255) NOT NULL,
	IsBloqueado BIT NOT NULL DEFAULT(0),
	IsActivo BIT NOT NULL DEFAULT(1),
	FechaCreacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	CONSTRAINT PK_PrimaryKeyUsuario PRIMARY KEY(Id),
	CONSTRAINT UK_UniqueUsuario UNIQUE(Email)
)
GO

INSERT INTO rsocial_Usuario(Nombre, Apellido, Telefono, Email, Contrasena) VALUES
('Mairon Andres', 'Florez', '3058128826', 'andresflorez0799@gmail.com', 'vMHK4sNnQYHuE1xiHwfj9A==')
GO

CREATE TABLE rsocial_PublicacionEstado
(
	Id INT NOT NULL,
	Nombre VARCHAR(30) NOT NULL,
	CONSTRAINT PK_PrimaryKeyPublicacionEstado PRIMARY KEY(Id),
)
GO
INSERT INTO rsocial_PublicacionEstado(Id, Nombre) VALUES
(1, 'Inicial'),	---1
(2, 'Publicado'),  ---2
(3, 'Pausado'),	---3
(4, 'Eliminado')	---4
GO

DROP TABLE IF EXISTS Publicacion
GO
CREATE TABLE rsocial_Publicacion
(
	Id INT NOT NULL IDENTITY(1, 1),
	IdUsuario INT NOT NULL,
	IdEstado INT NOT NULL DEFAULT(1), ---Inicial o Creado
	Fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	Texto VARCHAR(2500) NULL,	
	CONSTRAINT PK_PrimaryKeyPublicacion PRIMARY KEY(Id),
	CONSTRAINT FK_PublicacionUsuario FOREIGN KEY(IdUsuario) REFERENCES rsocial_Usuario(Id),
	CONSTRAINT FK_PublicacionPublicacionEstado FOREIGN KEY(IdEstado) REFERENCES rsocial_PublicacionEstado(Id)
)
GO

CREATE TABLE rsocial_PublicacionImagen
(
	Id INT NOT NULL IDENTITY(1, 1),
	IdPublicacion INT NOT NULL,
	ImagenRuta VARCHAR(255) NOT NULL,
	ImagenNombre VARCHAR(80),
	ImagenExtension VARCHAR(80),
	CONSTRAINT PK_PrimaryKeyPublicacionImagen PRIMARY KEY(Id),
	CONSTRAINT FK_PublicacionImagenPublicacion FOREIGN KEY (IdPublicacion) REFERENCES rsocial_Publicacion(Id)
)
GO

CREATE TABLE rsocial_PublicacionVideo
(
	Id INT NOT NULL IDENTITY(1, 1),
	IdPublicacion INT NOT NULL,
	VideoRuta VARCHAR(255) NOT NULL,
	VideoNombre VARCHAR(80),
	VideoExtension VARCHAR(80),
	CONSTRAINT PK_PrimaryKeyPublicacionVideo PRIMARY KEY(Id),
	CONSTRAINT FK_PublicacionVideoPublicacion FOREIGN KEY (IdPublicacion) REFERENCES rsocial_Publicacion(Id)
)
GO

CREATE TABLE rsocial_PublicacionArchivo
(
	Id INT NOT NULL IDENTITY(1, 1),
	IdPublicacion INT NOT NULL,
	ArchivoRuta VARCHAR(255) NOT NULL,
	ArchivoNombre VARCHAR(80),
	ArchivoExtension VARCHAR(80),
	CONSTRAINT PK_PrimaryKeyPublicacionArchivo PRIMARY KEY(Id),
	CONSTRAINT FK_PublicacionArchivoPublicacion FOREIGN KEY (IdPublicacion) REFERENCES rsocial_Publicacion(Id)
)
GO

/*Mensajeria*/
CREATE TABLE rsocial_MensajeriaEstado
(
	Id INT NOT NULL,
	Nombre VARCHAR(30) NOT NULL,
	CONSTRAINT PK_PrimaryKeyMensajeriaEstado PRIMARY KEY(Id),
)
GO
INSERT INTO rsocial_MensajeriaEstado(Id, Nombre) VALUES
(1, 'Enviado'), (2, 'Recibido'), (3, 'Archivado'), (4, 'Eliminado')
GO

CREATE TABLE rsocial_Mensajeria
(
	Id INT NOT NULL IDENTITY(1, 1),
	IdUsuarioOrigen INT NOT NULL,
	IdUsuarioDestino INT NOT NULL,
	IdEstado INT NOT NULL, 
	Fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	Mensaje VARCHAR(255),
	CONSTRAINT PK_PrimaryKeyMensajeria PRIMARY KEY(Id),
	CONSTRAINT FK_MensajeriaUsuario_Origen FOREIGN KEY(IdUsuarioOrigen) REFERENCES rsocial_Usuario(Id),
	CONSTRAINT FK_MensajeriaUsuario_Destino FOREIGN KEY(IdUsuarioDestino) REFERENCES rsocial_Usuario(Id),
	CONSTRAINT FK_MensajeriaMensajeriaEstado FOREIGN KEY(IdEstado) REFERENCES rsocial_MensajeriaEstado(Id),
)
GO

/*Publicidada*/
CREATE TABLE rsocial_Publicidad
(
	Id INT NOT NULL IDENTITY(1,1),
	Informacion VARCHAR(255),  ---Texto de la publicidad
	Banner VARBINARY(MAX),	---Este campo de banner lo dejo en bd considerando que no seran una cantidad importante de registros sobre la tabla
	FechaFinalizacion DATETIME, ---Para considerar una fecha final de mostrar inclusive la hora
	IsActivo BIT NOT NULL DEFAULT(1),  ---Activo o inactivo
	VinculoComercial VARCHAR(255) NULL,  ---considerado para colocar un hipervinculo a alguna ruta de compra, mas info, etc
	FechaCreacion DATETIME,
	CONSTRAINT PK_PrimaryKeyPublicidad PRIMARY KEY(Id),
)
GO

/*Bitacora de errores*/
DROP TABLE IF EXISTS rsocial_BitacoraErrores
GO
CREATE TABLE rsocial_BitacoraErrores
(
	Id INT IDENTITY(1,1) NOT NULL,
	ErrorMessage varchar(5000) NULL,
	ErrorType varchar(255) NULL,
	ErrorInnerException varchar(8000) NULL,
	ErrorStackTrace varchar(8000) NULL,
	ErrorSource varchar(5000) NULL,
	ErrorTargetSite varchar(255) NULL,
	ErrorTimeStamp datetime not null default current_timestamp,
	CONSTRAINT PK_PrimaryKeyBitacoraErrores PRIMARY KEY(Id),
)
GO