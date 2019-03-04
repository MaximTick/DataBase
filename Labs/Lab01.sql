--В таблице Transport перед ее созданием изменить столбец taransportNumber на transportNumber
use master;

create database Trucking;
go 

use Trucking;

create table Client(
id int identity(1,1),
clientName nvarchar(70) not null,
constraint pk_client primary key(id)
);

create table Goods(
id int identity(1,1),
shippingName nvarchar(70) not null, --наименование доставки
weightGoods int not null,
constraint pk_goods primary key(id)
);

create table City(
id int identity(1,1),
city nvarchar(30) not null,
constraint pk_city primary key(id)
);

create table Driver(
id int identity(1,1) unique,
lastName nvarchar(30) not null,
firstName nvarchar(30) not null,
driversLicenseNumber int not null,
category nvarchar(10) not null,
salary money not null,
[year] int not null,
--constraint pk2_driver primary key(lastname, [year])
) on [primary]
go

--drop table Driver;
--alter table Transport drop fk_driver;
alter table Transport add constraint fk_driver foreign key(idDriver) references Driver(id)

create table Transport(
id int identity(1,1),
taransportNumber nchar(7) not null,
capacity int not null,	
idDriver int not null,
constraint pk_trucks  primary key(id),
constraint fk_driver foreign key (idDriver) references Driver(id)
);

create table Carriage(
id int identity(1,1),
idClient int not null,
idGoods int not null,
idTransport int not null,
idCity int not null,
dateOfDelivery datetime not null,
typeOfService nvarchar(60) not null,
constraint check_type check (typeofService = 'Автомобильная' or typeofService = 'Железнодорожная' or 
typeofService = 'Авиационная' or typeofService = 'Морская/речная'),
constraint pk_carriage primary key(id),
constraint fk_client foreign key (idClient) references Client(id),
constraint fk_goods foreign key (idGoods) references Goods(id),
constraint fk_trucks foreign key (idTransport) references Transport(id),
constraint fk_city foreign key (idCity) references City(id)
);

create procedure GetInfoCarriage
as
select cl.clientName[Имя клиента], g.shippingName[Поставка], g.weightGoods[вес поставки], 
d.lastName[Фамилия доставщика], d.firstName [Иия доставщика], city.city[В город], dateOfDelivery[дата поставки] from 
Client cl join Carriage car on cl.id = car.idClient 
join Goods g on g.id = car.idGoods
join Transport t on t.id = car.idTransport 
join Driver d on d.id = t.idDriver
join City city on city.id = car.idCity
go

exec GetInfoCarriage


create view InfoAboutCarrage as
select cl.clientName[Имя клиента], g.shippingName[Поставка], g.weightGoods[вес поставки], 
d.lastName[Фамилия доставщика], d.firstName [Иия доставщика], city.city[В город], dateOfDelivery[дата поставки] from 
Client cl join Carriage car on cl.id = car.idClient 
join Goods g on g.id = car.idGoods
join Transport t on t.id = car.idTransport 
join Driver d on d.id = t.idDriver
join City city on city.id = car.idCity

select * from InfoAboutCarrage;

create nonclustered index IX_Carriage on Carriage (dateOfDelivery);

create procedure [dbo].[AddCarriage]
	@idClient int,
	@idGoods int,
	@idTransport int,
	@idCity int,
	@dateOfDelivery datetime,
	@typeOfService nvarchar(60)
AS

declare @weight int;
declare @capacity int;
select @weight = weightGoods, @capacity = capacity from Goods, Transport where @idGoods = Goods.id and @idTransport = Transport.id;
if(@weight < @capacity)
insert into Carriage (idClient, idGoods, idTransport,idCity, dateOfDelivery, typeOfService) 
select @idClient, @idGoods, @idTransport, @idCity, @dateOfDelivery, @typeOfService
if(@weight > @capacity)
print N'Вес для перевозки не может быть больше грузоподъемности грузовика';
select id, idClient, idGoods, idTransport, idCity, dateOfDelivery, typeOfService from Carriage where id = SCOPE_IDENTITY()
go


create procedure [GetAllCarriage]
as
select idClient, idGoods, idTransport, idCity, dateOfDelivery, typeOfService from Carriage
go

execute [dbo].[AddCarriage] @idClient = 11, @idGoods = 1,  @idTransport = 1, @idCity = 7, @dateOfDelivery =  '2019-02-24'

create procedure InsertIntoClient
@clientName nvarchar(70)
as 
insert into Client(clientName) values(@clientName);



---trigger
CREATE TABLE History 
(
    id INT IDENTITY(1,1) PRIMARY KEY,
    carriageId INT NOT NULL,
    operation NVARCHAR(200) NOT NULL,
	createAt DATETIME NOT NULL DEFAULT GETDATE(),
);
go
create trigger Carriage_Insert on Carriage after insert
as 
insert into History (carriageId, operation) select id , 'Добавлена поставка' from inserted
go

select * from history

