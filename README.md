# Gestión de Activos Fijos Toyota

## Descripción

Este proyecto está diseñado para la gestión de activos físicos en la industria automotriz, con un enfoque específico en Toyota. La aplicación web desarrollada facilita y optimiza la administración de inventarios y la disponibilidad de productos en la venta de automóviles y repuestos. Se utiliza el patrón de diseño MVC (Modelo, Vista, Controlador) para estructurar la aplicación, y SQL Server Management como la base de datos para gestionar la información.

El objetivo principal es proporcionar una solución eficiente y fácil de usar que permita a Toyota gestionar sus activos de manera efectiva, garantizando que los productos estén siempre disponibles y que la administración de inventarios se realice de manera precisa.

## Tipos de Usuario

En este proyecto se contemplan dos tipos de usuarios, cada uno con funcionalidades específicas:

- **Usuario Administrativo**:
  - **Gestión de Usuarios**: Puede ver la lista de usuarios activos, así como editarlos, eliminarlos y agregar nuevos usuarios. Esto permite mantener un control actualizado y preciso de las personas que tienen acceso al sistema.
  - **Gestión de Vehículos**: Tiene la capacidad de visualizar, agregar, editar y eliminar vehículos del inventario. Esto es crucial para mantener la disponibilidad y actualización de los automóviles disponibles para la venta.
  - **Gestión de Repuestos**: Al igual que con los vehículos, el administrador puede gestionar los repuestos, asegurando que los productos necesarios estén siempre disponibles para los clientes.

- **Usuario Cliente**:
  - **Compras**: Los clientes pueden navegar por el inventario de vehículos y repuestos, agregarlos a su carrito de compras, y proceder a realizar la compra. También tienen la opción de eliminar productos no deseados de su carrito.
  - **Gestión de Perfil**: Los clientes pueden administrar y actualizar su información personal, asegurando que su perfil esté siempre actualizado con la información correcta.

## Instalación

Para poner en marcha el proyecto, sigue los pasos a continuación:

1. **Instalar Herramientas Necesarias**:
   - **Visual Studio 2022**: Es el entorno de desarrollo utilizado para construir la aplicación.
   - **SQL Server Management Studio 2022**: Utilizado para gestionar la base de datos que soporta la aplicación.

2. **Clonar el Repositorio**:
   - Dirígete a [GitHub](https://github.com/MariaValeriaJimenezSolano/ActivosFijos_ProyectoFinal.git) y clona el repositorio para obtener la última versión del proyecto.

3. **Configurar la Conexión a la Base de Datos**:
   - Abre el proyecto en Visual Studio 2022 y configura la conexión a la base de datos a través de SQL Server Management Studio. Asegúrate de que la base de datos esté correctamente configurada para que la aplicación pueda acceder a los datos necesarios.

4. **Verificar Conexión a Internet**:
   - Asegúrate de tener una conexión a internet estable para ejecutar la aplicación, ya que algunas funcionalidades pueden requerir acceso a la web.

## Uso

### Interfaz de Usuario

- **Pantalla de Login**: Al iniciar la aplicación, se muestra una pantalla de inicio de sesión con dos opciones: ingresar o crear una cuenta.
  - **Crear Cuenta**: Se despliega un formulario solicitando información personal, incluyendo cédula, nombre, apellidos, edad, género, teléfono, dirección, correo y contraseña. El sistema incluye validaciones para asegurar que los datos se ingresen correctamente.
  - **Ingreso**: Los usuarios ingresan con su cédula y contraseña. Dependiendo del tipo de usuario, serán redirigidos a la interfaz de administrador o cliente.

### Funcionalidades del Administrador

- **Gestión de Usuarios**: 
  - Acceso a una lista de usuarios con opciones para agregar, editar o eliminar usuarios.
  - La interfaz de usuario para agregar o editar incluye campos para la información personal del usuario, y validaciones para asegurar la integridad de los datos.

- **Gestión de Repuestos y Vehículos**: 
  - Interfaz con filtros para buscar repuestos y vehículos por modelo, año y precio.
  - Opciones para agregar, editar o eliminar repuestos y vehículos del inventario. Al agregar un nuevo producto, se permite seleccionar una imagen desde los archivos del equipo.

- **Gestión de Perfil**:
  - Opción para modificar la información personal del administrador, asegurando que los datos del perfil estén siempre actualizados.

### Funcionalidades del Cliente

- **Navegación y Compra**:
  - Los clientes pueden buscar vehículos y repuestos disponibles utilizando filtros de búsqueda por modelo, año y precio.
  - Opción para ver detalles de los productos, agregarlos al carrito de compras y proceder con la compra. 

- **Carrito de Compras**:
  - Visualización del carrito con opción para eliminar productos antes de proceder al pago.
  - Al realizar un pago, se solicita la información de la tarjeta, y se confirma la compra con un mensaje de éxito.

- **Gestión de Perfil**:
  - Similar a la funcionalidad del administrador, los clientes pueden actualizar su información personal.

- **Redes Sociales**:
  - Acceso rápido a las redes sociales de Toyota, incluyendo Facebook, X (Twitter), e Instagram.

## Autores

Este proyecto fue desarrollado por:

- Esteban Watkins Monge
- Mariana Vega Jiménez
- Alessandro Joshue Roverssi Aiza
- María Valeria Jiménez Solano

## Enlaces

- Repositorio del Proyecto: [GitHub](https://github.com/MariaValeriaJimenezSolano/ActivosFijos_ProyectoFinal.git)
