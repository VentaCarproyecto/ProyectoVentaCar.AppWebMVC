CREATE DATABASE VentacarProyecto
GO 

USE VentacarProyecto

--Creamos la tabla  Marcas
CREATE TABLE Marcas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Marca VARCHAR(25) NOT NULL
);

-- Tabla Departamentos
CREATE TABLE Departamentos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Departamento VARCHAR(25) NOT NULL
);

-- creamos la tabla Vendedores
CREATE TABLE Vendedores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(25) NOT NULL,
	Apellido NVARCHAR(25) NOT NULL,
    Telefono INT NOT NULL,
    Direccion VARCHAR(100) NOT NULL,
	Email NVARCHAR(50) NOT NULL,
	Dui INT NOT NULL,
    Password NVARCHAR(255) NOT NULL,
	Role NVARCHAR(20) NOT NULL
);

--Creamos la tabla Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR (25) NOT NULL, 
    Apellido NVARCHAR(50) NOT NULL,
    Telefono CHAR(9) NOT NULL,
	Direccion VARCHAR(100) NOT NULL,
	Correo NVARCHAR(50) NOT NULL,
    Dui INT NOT NULL,
	Password NVARCHAR(255) NOT NULL,
	Role NVARCHAR(20) NOT NULL
);

--Creamos la tabla Autos
CREATE TABLE Autos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdVendedor INT NOT NULL,
	IdDepartamento INT NOT NULL,
    IdMarca INT NOT NULL,
	AnnoFabricacion CHAR(4),
    Modelo VARCHAR(25) NOT NULL,
    DescripcionA VARCHAR (200) NOT NULL,
	Kilometraje DECIMAL(6,3),
	Estado VARCHAR(25), 
	Precio DECIMAL(8,2)NOT NULL, 
	URLImagen VARCHAR(255),
	URT DATETIME  NOT NULL,
	FechaRP DATETIME,
    Comentario varchar(50),
    FOREIGN KEY (IdVendedor) REFERENCES Vendedores(Id),
    FOREIGN KEY (IdMarca) REFERENCES Marcas(Id),
	FOREIGN KEY (IdDepartamento) REFERENCES Departamentos(Id)

);

--creamos la tabla CarritoCompras
CREATE TABLE CarritoCompras (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    Estado TINYINT NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clientes(Id)
);

--Creamos la tabla Repuestos
CREATE TABLE Repuestos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreRepuesto VARCHAR(25) NOT NULL,
	IdVendedor INT NOT NULL, 
	IdDepartamento INT NOT NULL,
	ImgProducto VARCHAR(255) NOT NULL,
	Compatiblilidad VARCHAR(50) NOT NULL,
	DescripcionR VARCHAR(200) NOT NULL,
	Proveniencia VARCHAR(25) NOT NULL, 
	EstadoRP VARCHAR(25) NOT NULL,
	Precio DECIMAL(8, 2) NOT NULL,
    FechaRP DATETIME NOT NULL,
    Disponibilidad INT NOT NULL,
    Actividad TINYINT,
	ComentarioR VARCHAR(50),
	FOREIGN KEY (IdVendedor) REFERENCES Vendedores(Id),
	FOREIGN KEY (IdDepartamento) REFERENCES Departamentos(Id)

);



--creamos la tabla ItemsCarrito
CREATE TABLE ItemsCarrito (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdCarrito INT NOT NULL,
    IdRepuesto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(8, 2) NOT NULL,
    FOREIGN KEY (IdCarrito) REFERENCES CarritoCompras(Id),
    FOREIGN KEY (IdRepuesto) REFERENCES Repuestos(Id)
);

--Creamos la tabla Ventas
CREATE TABLE Ventas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
	IdRepuesto INT NOT NULL,
    NumeroTarjeta INT NOT NULL,
	NombreTrajeta VARCHAR(50),
    FechaExpTrajeta DATETIME NOT NULL,
	RImgProducto VARBINARY(MAX) NOT NULL, 
    TotalPago DECIMAL(8, 2) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Clientes(Id),
	FOREIGN KEY (IdRepuesto)REFERENCES Repuestos(Id)

);

--creamos la tabla DetalleVentas
CREATE TABLE DetalleVentas (
    ID INT PRIMARY KEY IDENTITY(1,1),
    IdVenta INT NOT NULL,
    IdRepuesto INT,
    IdAuto INT,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL (10,2) NOT NULL,
    FOREIGN KEY (IdVenta) REFERENCES Ventas(Id),
    FOREIGN KEY (IdRepuesto) REFERENCES Repuestos(Id),
    FOREIGN KEY (IdAuto) REFERENCES Autos(Id)
);

