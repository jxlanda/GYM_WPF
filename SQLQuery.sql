CREATE DATABASE GYMDB
USE GYMDB

CREATE TABLE TipoUsuario
(
	id INT IDENTITY,
	nombre VARCHAR(20),
	tipousuario CHAR(1),
	CONSTRAINT tipousuario_pkey PRIMARY KEY (id)
)

CREATE TABLE Usuario
(
	id INT IDENTITY,
	nombre VARCHAR(20),
	apaterno VARCHAR(20),
	amaterno VARCHAR(20),
	apodo VARCHAR(20),
	pin VARCHAR(20),
	imgpath VARCHAR(250),
	correo VARCHAR(150),
	telefono VARCHAR(20),
	genero CHAR(1),
	tipousuario_id INT,
	CONSTRAINT usuario_pkey PRIMARY KEY (id),
	CONSTRAINT tipousuario_fkey FOREIGN KEY (tipousuario_id) REFERENCES TipoUsuario(id)
)

CREATE TABLE Cliente
(
	id INT IDENTITY,
	nombre VARCHAR(20),
	apaterno VARCHAR(20),
	amaterno VARCHAR(20),
	apodo VARCHAR(20),
	pin VARCHAR(20),
	imgpath VARCHAR(250),
	correo VARCHAR(150),
	fnacimiento DATE,
	peso DECIMAL,
	estatura INT,
	genero CHAR(1),
	CONSTRAINT cliente_pkey PRIMARY KEY (id)
)

CREATE TABLE Ejercicio
(
	id INT IDENTITY,
	nombre VARCHAR(30),
	descripcion VARCHAR(250),
	CONSTRAINT ejercicio_pkey PRIMARY KEY (id)
)

CREATE TABLE Rutina
(
	id INT IDENTITY,
	dia VARCHAR(15),
	repeticiones INT,
	peso DECIMAL,
	ejercicio_id INT,
	cliente_id INT,
	CONSTRAINT rutina_pkey PRIMARY KEY (id),
	CONSTRAINT ejercicio_fkey FOREIGN KEY (ejercicio_id) REFERENCES Ejercicio(id),
	CONSTRAINT clienterutina_fkey FOREIGN KEY (cliente_id) REFERENCES Cliente(id)
)

-- INSERTS

INSERT INTO TipoUsuario VALUES 
('Administrador','A'),
('Entrenador','E')

INSERT INTO Usuario VALUES 
('Usuario2', 'APaterno', 'AMaterno','root','1234', 'C:\user.jpg', 'correo@outlook.com','6645314807','M',1)
