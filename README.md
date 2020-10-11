# basic-dotnet-api
Basic asp.net core api for Untitled Project


Elvileg VS-ben simán csak meg kell nyitni.

Esetleg ha kell, akkor SQL Studioval csináljatok egy UntitledProject nevű adatbázist, aztán még ezeket futtassátok le rá:
Create Table Product(
ProductId Int Identity(1,1) Primary Key,
ProductName Varchar(100) Not Null,
Category Varchar(100) Not Null,
Offerer Varchar(100),
Description Varchar(1000),
Price Decimal Not Null)
GO
Create Table AppUser(
UserId Int Identity(1,1) Not null Primary Key,
FirstName Varchar(30) Not null,
LastName Varchar(30) Not null,
UserName Varchar(30) Not null,
Email Varchar(50) Not null,
Password Varchar(20) Not null,
CreatedDate DateTime Default(GetDate()) Not Null)
GO
Insert Into UserInfo(FirstName, LastName, UserName, Email, Password) 
Values ('Inventory', 'Admin', 'Admin', 'admin@admin.com', 'Adm1n?0?0')

Szükség esetén írjátok át a ConnectionString-et az appsettings.json-ben.
Jelenleg ez van bent:
"ConnectionStrings": {
    "UntitledProjectDb": "Server=localhost\\SQLEXPRESS;Database=UntitledProject;Integrated Security=True;"
  }
