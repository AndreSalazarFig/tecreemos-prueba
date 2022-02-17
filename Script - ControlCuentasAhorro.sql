Create database ControlCuentasAhorro
go

Use ControlCuentasAhorro
Go

Create table Clientes(
	IdCliente int identity(1,1) Primary key not null,
	Nombre nvarchar(50) not null,
	ApellidoPaterno nvarchar(50) not null,
	ApellidoMaterno nvarchar(50) not null,
	NumeroCliente nvarchar(5) not null,
	FechaAlta Datetime not null
)

create table CuentaAhorro(
	IdCuentaAhorro int identity(1,1) Primary key not null,
	IdCliente int foreign key references Clientes(IdCliente) not null,
	SaldoActual decimal(8,2) not null,
	NumeroCuenta nvarchar(5) not null,
	FechaAlta Datetime not null
)

create table Transaccion(
	IdTransaccion int identity(1,1) Primary key not null,
	IdCuentaAhorro int foreign key references CuentaAhorro(IdCuentaAhorro) not null,
	IdTipoOperacion int not null,
	Monto decimal(8,2) not null,
	SaldoAnterior decimal(8,2) not null,
	SaldoActual decimal(8,2) not null,
	FechaOperacion Datetime not null
)