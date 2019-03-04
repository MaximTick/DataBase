use master
go
exec sp_configure 'clr enabled', 1;
go
reconfigure 
go

use Trucking
go
----------------------------------------------------------------------------------------------------------------
alter database Trucking set TRUSTWORTHY  ON;
create assembly DB_Lab03_Assembly from 'D:\study3course\6thsem\Db\Labs\DB_Lab03\DB_Lab03\bin\Debug\DB_Lab03.dll' WITH PERMISSION_SET = SAFE;
go

create procedure MyStoredProcedure @StartDate datetime, @EndDate datetime
as
external name DB_Lab03_Assembly.StoredProcedures.MyStoredProcedure;
go

create procedure DriverPivot @StartYear int, @EndYear int
as
external name DB_Lab03_Assembly.StoredProcedures.DriverPivot;
go

execute MyStoredProcedure @StartDate = '2019-01-01', @EndDate = '2020-02-12';

execute DriverPivot @StartYear = 2015, @EndYear = 2017;



create type dbo.myroute
external name DB_Lab03_Assembly.RouteCity
go

declare @s myroute
set @s = 'Minsk, Moscow';
select @s.ToString();

alter table Carriage add FromTo myroute;



select lastName, firstName, [year], sum(salary) as salary 
from Driver group by lastName, firstName, [year]
order by lastName;

select distinct  lastName, [2015], [2016], [2017] from (select lastName, [year], salary from Driver where [year] between 2015 and 2016) as salary
PIVOT (sum(salary) for [year] in([2015], [2016], [2017])) as my_pivot 
group by lastName, [2015], [2016], [2017]
order by lastName




