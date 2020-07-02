IF DB_ID('Data_Records') IS NULL
    create database Data_Records;
GO	
use Data_Records
--Deleting tables and views, if they exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblReport')
	drop table tblReport;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblEmployee')
	drop table tblEmployee;
IF EXISTS(select * FROM sys.views where name = 'vwReport')
	drop view vwReport;
IF EXISTS(select * FROM sys.views where name = 'vwEmployee')
	drop view vwEmployee;
IF EXISTS(select * FROM sys.views where name = 'vwManager')
	drop view vwManager;

--Creating a table of employees
use Data_Records
create table tblEmployee(
EmployeeID int IDENTITY(1,1) PRIMARY KEY,
Name varchar(30) NOT NULL,
Surname varchar(50) NOT NULL,
DateOfBirth date NOT NULL,
JMBG varchar(13) UNIQUE NOT NULL,
BankAccountNumber varchar(18) UNIQUE NOT NULL,
Email varchar(30) UNIQUE NOT NULL,
Salary int NOT NULL,
Position varchar(30) NOT NULL,
Username varchar(30) UNIQUE NOT NULL,
Password varchar(30) NOT NULL,
Sector varchar(30),
AccessLevel varchar(30),
--ReportID int FOREIGN KEY REFERENCES tblReport(ReportID),
CONSTRAINT CHK_DateOfBirth CHECK(DATEDIFF(day,DateOfBirth,GETDATE())>= 5479),
CONSTRAINT CHK_JMBG CHECK(LEN(JMBG)=13 AND ISNUMERIC(JMBG)=1),
CONSTRAINT CHK_BankAccountNumber CHECK(LEN(BankAccountNumber)=18 AND ISNUMERIC(BankAccountNumber)=1),
CONSTRAINT CHK_Salary CHECK(Salary > 0),
CONSTRAINT CHK_Sector CHECK(Sector IN('HR','FINANCIAL','R&D')),
CONSTRAINT CHK_AccessLevel CHECK(AccessLevel IN('modify','read-only'))
);

--Creating a table of reports
use Data_Records
create table tblReport(
ReportID int IDENTITY(1,1) PRIMARY KEY,
Date date NOT NULL,
Project varchar(50) NOT NULL,
Hours int NOT NULL,
EmployeeID int FOREIGN KEY REFERENCES tblEmployee(EmployeeID) NOT NULL
);

--Creating a view of reports
GO
create view vwReport as
select r.*, e.Name + ' ' + e.Surname 'Employee', e.Position
from tblReport r
INNER JOIN tblEmployee e
ON r.EmployeeID = e.EmployeeID;
--Creating a view of employees
GO
create view vwEmployee as
select EmployeeID, Name, Surname, DateOfBirth, JMBG, BankAccountNumber, Email, Salary, Position, Username, Password
from tblEmployee
where Sector IS NULL and AccessLevel IS NULL;
--Creating a view of managers
GO
create view vwManager as
select *
from tblEmployee
where Sector IS NOT NULL and AccessLevel IS NOT NULL;

