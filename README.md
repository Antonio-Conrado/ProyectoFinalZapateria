# Proyecto Tienda de Zapateria
# API REST de Zapatería

Esta API REST se creo con ASP.NET Core Web API y proporciona una interfaz para realizar operaciones CRUD en varias entidades, como productos, categorías, marcas, tallas, roles, ventas e inventarios.

## Requisitos previos

* .Net 7.0
* Visual Studio 2022 Version 17.7.6 en adelante
* SQL Server

## Instalación

1. Clona el repositorio de GitHub
2.  Ejecuta BD17-11-2023.sql en SQL

##### Enpoints de la API Rest

Los siguientes endpoints están disponibles para las siguientes entidades:

**Categoria:**  Operaciones CRUD para categorías.

**Marca:** Permiten a los usuarios consultar las marcas disponibles.

**Talla:** Permiten a los usuarios consultar las tallas disponibles.

**Rol:** Operaciones CRUD para Rol.

**Venta:** Permiten a los usuarios consultar registrar nuevas ventas.

**Inventario:** Operaciones CRUD para Inventario.

**Usuario:**  Operaciones CRUD para Usuario, además se utilizó bcrypt para el hash de la contraseña en la base de datos y se añadio el endpoint para autenticarse.

#### Captura de un endpoint para obtener la lista de Roles a traves de swagger
![](https://iili.io/JIqyaAQ.png)

#### Captura de la clave Acceso de un usuario utilizando Bcrypt en la base de datos
![](https://iili.io/JIBRXgs.jpg)

### Para autenticarse utilizar los datos del archivo Usuarios.txt

## Blazor Web Assembly

Para consumir la API REST, se utilizó Blazor Web Assembly.

La autorización basada en roles se implementó utilizando el componente Authorized. Este componente comprueba el rol del usuario actual y muestra la vista solo si el usuario tiene el rol requerido mediante el proceso de autenticacion mediante Login.

Se añadio el iframe para el informe del dashboard en power BI.

#### Captura de vista por roles. (Administrador)
![](https://iili.io/JIBTW0b.jpg)



#### Captura de vista por roles. (Empleado)
![](https://iili.io/JIBTw5Q.jpg)



#### Captura del iframe de power BI
![](https://iili.io/JIBITcx.png)

