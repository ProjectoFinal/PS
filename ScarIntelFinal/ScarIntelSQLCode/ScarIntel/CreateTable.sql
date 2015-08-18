USE BacIntel;


CREATE TABLE Person (
	id INT IDENTITY(1,1) PRIMARY KEY ,
	name VARCHAR(150) NOT NULL,
	birthday DATETIME not null ,
	birthplace VARCHAR(50) not null ,
	adress VARCHAR(50) not null
)


Create Table Biometric_Type(
	id INT IDENTITY(1,1) PRIMARY KEY ,
	name VARCHAR(20) not null,
	data IMAGE not null,
	person INT FOREIGN KEY REFERENCES Person(id) not null,
	UNIQUE ( person , name )	
)

CREATE TABLE Crime_Type (
	id INTEGER IDENTITY(1,1) PRIMARY KEY ,
	name VARCHAR(150) NOT NULL,
	UNIQUE ( name )
)


CREATE TABLE Regist(
	id INTEGER IDENTITY(1,1) PRIMARY KEY ,
	data	  DATETIME		 not null, 
	descript VARCHAR(500) 
)

CREATE TABLE Regist_CrimeType(
	id INTEGER IDENTITY(1,1) PRIMARY KEY ,
	crimeType INTEGER  FOREIGN KEY REFERENCES Crime_Type(id)not null ,
	regist INTEGER FOREIGN KEY REFERENCES Regist(id) not null,
	
)

CREATE TABLE Participant(
	id INTEGER IDENTITY(1,1) PRIMARY KEY ,
	person INTEGER    FOREIGN KEY REFERENCES Person(id)not null ,
	regist INTEGER   FOREIGN KEY REFERENCES Regist(id) not null,
	
)

CREATE TABLE Document_Type (
	id   INTEGER IDENTITY(1,1)  PRIMARY KEY,
	name VARCHAR(20) not null,
	UNIQUE ( name )
)

CREATE TABLE Document (
	id INTEGER IDENTITY(1,1) PRIMARY KEY ,
	code VARCHAR (15),
	emission_local VARCHAR(20) not null ,
	emission_date DATETIME not null ,
	expiration_date DATETIME not null,
	docu_type INTEGER FOREIGN KEY REFERENCES Document_Type(id) not null,
	person_id INT FOREIGN KEY REFERENCES Person(id) not null,
	
	CHECK  ( emission_date < expiration_date ) ,
	UNIQUE ( code , docu_type )
)



CREATE TABLE Session (
	id INTEGER IDENTITY(1,1) PRIMARY KEY ,
	token VARCHAR (128) not null ,
	_user VARCHAR (15) not null ,
	emission_time DATETIME not null ,
	expiration_time DATETIME not null,
	UNIQUE (token)
)




