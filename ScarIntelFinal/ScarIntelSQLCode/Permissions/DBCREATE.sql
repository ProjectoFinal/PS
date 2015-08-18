create table _User (
	name varchar(10) primary key,
	password varchar(20) not null,
	check  ( DATALength([password])>=6)
)

create table _Role (
	name varchar(50) primary key,
	junior varchar(50) references _Role
)

/*create table _Session(
	id int primary key
)*/

create table _UserAssignment(
	name varchar(10) references _User,
	mainRole varchar(50) references _Role,
	primary key (name, mainRole)
)

create table _Permission(
	action varchar(50),
	resource varchar(50),
	primary key (action, resource)
)

create table _PermissionAssignment(
	action varchar(50),
	resource varchar(50),
	foreign key (action, resource) references _Permission,
	mainRole varchar(50) references _Role
	primary key (action, resource, mainRole)
)

