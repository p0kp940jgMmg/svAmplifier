create schema dco

create table dco.Pick
(       Id int identity primary key,
		Latitude varchar(16),
		Longitude varchar(16),
		MushroomName varchar(32) not null,
		MushroomPicUrl varchar(128),
		DatePicked datetime not null,
		WeightInGrams int,
        UserId int references dco.[User](id)
		)
go
create table dco.[User]
(
	Id int identity primary key,
	Street 	varchar(32),
	Zipcode	varchar(32),
	City 	varchar(32),
    AspNetId  nvarchar(450) references dbo.AspNetUsers(Id) unique
	)
