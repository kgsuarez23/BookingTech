# Inicialización del Proyecto

Siga los pasos a continuación para configurar el entorno en un nuevo equipo:

## 1. Configuración de la Base de Datos

- **Ubicación del Script:**  
  En la carpeta **ModelDatabase** se encuentra el archivo `SqlSolucionBooking.sql`. Este script contiene el modelo de datos diseñado en *SQL Server* para soportar los requerimientos del sistema.

- **Acciones del Script:**  
  Al ejecutar el script se realizarán las siguientes acciones:  
  - Creación de la base de datos **DB_Hotel**.  
  - Generación de todas las entidades, procedimientos almacenados y triggers necesarios.  
  - Creación del usuario `usr_booking_tech` con rol `db_owner` (requerido en el paso 2).  
  - Inserción de datos de prueba en las entidades.  
    > **Nota:** Aunque el sistema permite la creación de nuevos usuarios, se puede utilizar uno de los usuarios registrados en la tabla `Users` con la contraseña `test`.

## 2. Configuración de la Solución API

La solución API consta de los siguientes proyectos:

- **Api.GestionHotel:**  
  Proyecto de tipo *ASP.NET Core Web API* (proyecto principal).

- **Api.Manager:**  
  Proyecto de tipo *Biblioteca de Clases*.

- **Api.Manager.Base:**  
  Proyecto de tipo *Biblioteca de Clases*.

### Cadena de Conexión

- **Requerimiento:**  
  Es necesario configurar la cadena de conexión a la base de datos.

- **Acción:**  
  Reemplace el valor de la propiedad `ConnectionStrings.ConnectionBD` por la cadena de conexión correspondiente a su entorno.

> **Importante:**  
> No se recomienda modificar los demás valores, ya que algunos se utilizaron para cifrar información almacenada en la base de datos. Si decide probar otros valores, asegúrese de actualizar también los datos correspondientes en la base de datos.

## 3. Configuración del Servicio de Correo

- **Propósito:**  
  Las propiedades en `ServiceMail` utilizan un correo activo y funcional, necesario para enviar notificaciones por email.

- **Recomendación:**  
  No se recomienda alterar estos valores. En caso de realizar cambios, utilice las credenciales de su servicio de correo preferido.

## 4. Autenticación y Perfilamiento de Usuario

- **Requerimiento:**  
  Algunos métodos del sistema requieren perfilamiento de usuario.

- **Acción:**  
  Realice el login a través del controlador `AuthLogin` en el endpoint `LoginQuery` y registre el JWT para acceder a dichos métodos.

## 5. Configuración de Postman

- **Archivo de Colección:**  
  En la carpeta `Testing_postman` se incluye el archivo `Booking.postman_collection.json`.

- **Uso:**  
  Este archivo se puede cargar en Postman para simplificar el uso del API. Recuerde modificar las variables `baseUrl`, `user` y `pass` según sus necesidades.

---

Con estos pasos, su entorno de desarrollo quedará correctamente configurado para trabajar con el proyecto.
