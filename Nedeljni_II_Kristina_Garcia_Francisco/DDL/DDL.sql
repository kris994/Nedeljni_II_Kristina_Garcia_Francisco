-- Dropping the tables before recreating the database in the order depending how the foreign keys are placed.
IF OBJECT_ID('tblClinic', 'U') IS NOT NULL DROP TABLE tblClinic;
IF OBJECT_ID('tblClinicAdministrator', 'U') IS NOT NULL DROP TABLE tblClinicAdministrator;
IF OBJECT_ID('tblClinicMaintenance', 'U') IS NOT NULL DROP TABLE tblClinicMaintenance;
IF OBJECT_ID('tblClinicPatient', 'U') IS NOT NULL DROP TABLE tblClinicPatient;
IF OBJECT_ID('tblClinicDoctor', 'U') IS NOT NULL DROP TABLE tblClinicDoctor;
IF OBJECT_ID('tblClinicManager', 'U') IS NOT NULL DROP TABLE tblClinicManager;
IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;
if OBJECT_ID('vwClinicAdministrator','v') IS NOT NULL DROP VIEW vwClinicAdministrator;
if OBJECT_ID('vwClinicMaintenance','v') IS NOT NULL DROP VIEW vwClinicMaintenance;
if OBJECT_ID('vwClinicManager','v') IS NOT NULL DROP VIEW vwClinicManager;
if OBJECT_ID('vwClinicDoctor','v') IS NOT NULL DROP VIEW vwClinicDoctor;
if OBJECT_ID('vwClinicPatient','v') IS NOT NULL DROP VIEW vwClinicPatient;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ClinicDB')
CREATE DATABASE ClinicDB;
GO

USE ClinicDB
CREATE TABLE tblClinic(
	ClinicID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	ClinicName VARCHAR (40)					NOT NULL,
	CreatingDate DATE						NOT NULL,
	ClinicOwner VARCHAR (40)				NOT NULL,
	ClinicAddress VARCHAR (40) 				NOT NULL,
	ClinicFloorNumber INT					NOT NULL,
	RoomsPerFloor INT     					NOT NULL,
	Balcony BIT								NOT NULL,
	Garden BIT								NOT NULL,
	EmergencyVehicleParkingLoots INT		NOT NULL,
	InvalidVehicleParkingLoots INT			NOT NULL,
);

USE ClinicDB
CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	FirstName VARCHAR (40)					NOT NULL,
	LastName VARCHAR (40)					NOT NULL,
	IdentificationCard VARCHAR (9) UNIQUE	NOT NULL,
	Gender CHAR								NOT NULL,
	DateOfBirth DATE     					NOT NULL,
	Citizenship VARCHAR (40)				NOT NULL,
	Username VARCHAR (40) UNIQUE			NOT NULL,
	UserPassword CHAR (1000)				NOT NULL,
);

USE ClinicDB
CREATE TABLE tblClinicAdministrator (
	AdminID INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID) NOT NULL,
);

USE ClinicDB
CREATE TABLE tblClinicMaintenance (
	MaintenanceID INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	ClinicExtentionAllowed BIT					NOT NULL,
	DisabledAccessabilityResponsibility BIT		NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID) NOT NULL,
);

USE ClinicDB
CREATE TABLE tblClinicManager (
	ManagerID INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	FloorNumber INT								NOT NULL,
	MaxNumberOfDoctors INT,
	MinNumberOfRooms INT,
	OmissionNumber INT DEFAULT 0				NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID) NOT NULL,
);

USE ClinicDB
CREATE TABLE tblClinicDoctor (
	DoctorID INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	UniqueNumber VARCHAR (9) UNIQUE				NOT NULL,
	BankAccount VARCHAR (20) UNIQUE				NOT NULL,
	Department VARCHAR (40)						NOT NULL,
	WorkingShift VARCHAR (40)					NOT NULL,
	ReceivingPatient BIT						NOT NULL,
	ManagerID INT FOREIGN KEY REFERENCES tblClinicManager(ManagerID),
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID) NOT NULL,
);

USE ClinicDB
CREATE TABLE tblClinicPatient (
	PatientID INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	HealthCareNumber INT UNIQUE					NOT NULL,
	ExperationDate DATE							NOT NULL,
	UniqueNumber VARCHAR(9) FOREIGN KEY REFERENCES tblClinicDoctor(UniqueNumber) NOT NULL,
	UserID INT FOREIGN KEY REFERENCES tblUser(UserID) NOT NULL,
);

GO
CREATE VIEW vwClinicAdministrator AS
	SELECT	tblUser.*, tblClinicAdministrator.AdminID 
	FROM	tblUser, tblClinicAdministrator
	WHERE	tblUser.UserID = tblClinicAdministrator.UserID

GO
CREATE VIEW vwClinicMaintenance AS
	SELECT	tblUser.*, 
			tblClinicMaintenance.ClinicExtentionAllowed, tblClinicMaintenance.DisabledAccessabilityResponsibility, 
			tblClinicMaintenance.MaintenanceID
	FROM	tblUser, tblClinicMaintenance 
	WHERE	tblUser.UserID = tblClinicMaintenance.UserID

GO
CREATE VIEW vwClinicManager AS
	SELECT	tblUser.*, tblClinicManager.FloorNumber, tblClinicManager.MaxNumberOfDoctors, tblClinicManager.MinNumberOfRooms,
			tblClinicManager.OmissionNumber, tblClinicManager.ManagerID
	FROM	tblUser, tblClinicManager
	WHERE	tblUser.UserID = tblClinicManager.UserID

GO
CREATE VIEW vwClinicDoctor AS
	SELECT	tblUser.*, tblClinicDoctor.UniqueNumber, tblClinicDoctor.BankAccount, tblClinicDoctor.Department,
			tblClinicDoctor.WorkingShift, tblClinicDoctor.ReceivingPatient, tblClinicDoctor.ManagerID, tblClinicDoctor.DoctorID
	FROM	tblUser, tblClinicDoctor
	WHERE	tblUser.UserID = tblClinicDoctor.UserID
	
GO
CREATE VIEW vwClinicPatient AS
	SELECT	tblUser.*, tblClinicPatient.HealthCareNumber, tblClinicPatient.ExperationDate, tblClinicPatient.UniqueNumber,
			tblClinicPatient.PatientID
	FROM	tblUser, tblClinicPatient
	WHERE	tblUser.UserID = tblClinicPatient.UserID