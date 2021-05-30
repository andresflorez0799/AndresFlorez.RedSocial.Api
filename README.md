# AndresFlorez.RedSocial.Api
- Api REST desarrollada en .Net5 con lenguaje de programación C#
- Se utiliza Swagger para su documentación (.Net5 por defecto con algunas modificaciones)
- Entity Framework Core para la gestion de los datos creada en sql server
- Sql Server 2019 Developer Edition (el proyecto cuenta con el script necesario de creación)
- Se crearon capa de manejo de datos y logica de forma generica
- Se agrego sistema de autenticacion  con JWT donde se debe contar con usuario y contraseña
  para obtener token, el cual sera usado para las peticiones posteriores.


Proyecto de prueba que cumpla con las siguientes caracteristicas:

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
