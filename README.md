# AndresFlorez.RedSocial.Api
- Api REST desarrollada en .Net5 con lenguaje de programación C#
- Se utiliza Swagger para su documentación (.Net5 por defecto con algunas modificaciones)
- Entity Framework Core para la gestion de los datos creada en sql server
- Sql Server 2019 Developer Edition (el proyecto cuenta con el script necesario de creación dentro del proyecto de Modelo 'SQL Query Creacion BD.sql')
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
		
		
::::::: A continuacion mayor detalle del proyecto ::::::

2021-05-31
Consideraciones Generales
1. Se utiliza .Net5 con lenguaje de programacion C#9 de  por ser el framework Y lenguaje mejor conocido por el desarrollador (teniendo en cuenta el tiempo indicado).

2. Se utiliza Sql Server (2019 Developer Edition), por ser el gestor de bd mas conocido y de mejor manejo por el desarrollador ademas que por facilidad de integracion con .Net5 viene bien, tambien considerando tiempos de entrega, escalabilidad y opciones de llevar la solucion a la nube.

2. Se implementa Entity Framework para la gestion del modelo de la solucion y la integracion con la base de datos, utilizando un base de datos previamente creada (DataBase First), utilizando la utilidad de Scaffold-DbContext.

3. Se diseña la solucion en 4 Capas Separadas por proyectos (Datos, Logica, Modelo y Api-Web), y como adicional otros proyectos mas de Utilidad o helpers.

Modelo: Toda la configuracion de Entity Framework construida de forma automatica mediente DataBase First usando el comando o utilidad de Scaffold-DbContext, se crea carpeta para separar el modelo actual, esto considerando que a futuro si se ve la necesidad de agregar otros se logre con un orden con carpetas.

Datos: Cuenta con una implementacion Generica a modo de repositorio aprovechando este patron, ademas se crearon dos carpetas para agrupar los contratos o interfaces que se exponen asi como cada una de las implementaciones, esto para el consumo de datos, en este caso el acceso y gestion sobre el modelo. Cabe resaltar que se aprovecha la herencia para minimizar y aprovechar el codigo lo maximo posible. Hace falta validar el principio de Unit of Work dentro de la implementacion, no se trabajo por tiempo, pero no resulta ser tan complejo de revisarlo.

Logica: Capa/proyecto que agrupa toda la logica y manipulacion de los datos, cuenta con sus interfaces que se exponen para su consumo, la intencion y la idea de esta separacion e spoder incluir todo lo que tenga que ver con reglas y consideraciones de logica y no dejarala del lado de datos o la presentacion web o la api.

4. Se realiza la implementacion de SignalR para la comunicacion o mensajeria instantanea, se elije por su facil implementacion, cuenta con las capacidades para utilizarlo en mensajeria en tiempo real, ademas que se integra muy facil al proyeco, esto se puede sacar y llevarlo a un microservicio en caso que la aplicacion lo amertie a futuro.

Consideraciones del almacenamiento

1. Se deja una tabla independiente para guardar la informacion relacionada con los archivos de las publicaciones, esto considerando que se tendra un algo volumen de dichos registros, lo que a largo y mediano plazo facilitara mantenimiento, por ejemplo poder segregrar en microservicios estas responsabilidades, utilizar distintos repositorios, entre otras.

2. La tabla de registros solo tendra la ruta o acceso fisico de los recursos de videos e imagenes de las publicaciones. considerando que se tendran bastantes registros en el mediamo y largo plazo con esto se busca optimizacion de las busquedas en la bd y ademas tambien que se podria repartir la dependencia en varios repositorios o bases de datos. Esto considerando que el almacenamiento fisico resulta ser el mas economico que un almacenamiento en base de datos. Por facilidad almacenamiento en base de datos seria lo mejor y facilitaria una migracion; pero por costos se cosidera que es la mejor opcion por el momento, en caso que a futuro se tomara la desicion de llevar los datos directo a una bd, solo seria modificar el modelo y su respectiva logica.

3. Se elije Sql Server por ser el mas familiar o mejor conocido por el desarrollador, pero esto no es una camisa de fuerza, podria facilmente posteriormente ser cambiado a otro motor de base de datos, ya que toda la logica de acceso a la capa de datos queda separada en un solo proyecto y ademas que se consume o accede mediente sus interfaces, y ademas se puede aprovechar el ORM de entity framework que ya esta configurado .

4. Se utiliza un objeto de respuesta hacia el usuario que consume el api.

5. Se aprovecha Swagger que trae pre-configurado la pantilla de Api de .net5 para la documentacion.

6. Se creo proyecto utilitario para gestionar extensiones de conversion, se dejan algunas extensiones para conversiones numericas y de fecha, esto lo hice mas por la failidad y rapidez que me brinda poder usarlas


Consideraciones de Seguridad

1. En la api construida se utiliza Json Web Token como mecanismo de autenticacion exponiendo un metodo para solicitar dicho token con un usuario y contraseña.

2. Se muestran al usuario mensajes genericos en el proceso de login, eje: si el usuario y/o password son incorrectos simplemente se indica login fallido. 

3. Queda pendiente implementar un sistemas de control y gestion de cantidad de peticiones, por tiempo no se logra pero estaba planeado hacerlo.

4. Se configura el CORS de la api para validar los host que se conectan a dicha api, esto en una publicacion de produccion es lo mas recomendable y una de las cosas a considerar en una api.

5. Se implementa un filtro para validar las peticiones de la api cuenten realmente con la autorizacion.

6. Se establece un periodo de maximo 1 hora para vencimiento del token

7. Se aprovecha la implementacion del JWT para proteger la integracion del Hub de SignalR (hombre ya esta configirado y listo pues solo se le agrega esa capa de seguridad al chat).

8. Se agrega dentro de proyecto de Utilidades una clase dedicada a seguridad donde se crean metodos para encriptado y descencriptado con algoritmo AES.



