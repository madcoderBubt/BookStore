CREATE DATABASE BookStore;

USE BookStore;

create table Accounts(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(255) NULL,
	Email NVARCHAR(255) NULL,
	Password NVARCHAR(255) NULL,
	Username NVARCHAR(255) NULL,
	RoleId INT NOT NULL DEFAULT((0)),
	UserType NVARCHAR(1) NOT NULL DEFAULT((0)),
	Status NVARCHAR(2) DEFAULT((1)),
	CreateAt DATETIME NOT NULL DEFAULT(getdate()),
	UpdateAt DATETIME NOT NULL DEFAULT(getdate()),
)

CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY NOT NULL PRIMARY KEY,
	[Name] [nvarchar](255) NULL,
	[Author] [nvarchar](255) NULL,
	[Details] [nvarchar](255) NULL,
	[ImgUrl] [nvarchar](255) NULL,
	[Active] [bit] NOT NULL,
	[Price] [int] NULL,
	[PriceOffer] [int] NULL,
	[CategoryId] [int] NULL,
)

CREATE TABLE [dbo].[Categories](
	[Id] [int] NOT NULL PRIMARY KEY IDENTITY,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
)

CREATE TABLE [dbo].[ShopingCartItems](
	[Id] [int] NOT NULL PRIMARY KEY,
	[Amount] [int] NULL,
	[ShopingCartId] [nvarchar](50) NULL,
	[BookId] [int] NULL,
)

CREATE TABLE [dbo].[Order](
	[Id] [int] NOT NULL PRIMARY KEY,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[OrderTotal] [decimal](18, 0) NULL,
	[OrderedPlaced] [datetime] NULL,
)

CREATE TABLE [dbo].[OrderDetail](
	[Id] [int] NOT NULL PRIMARY KEY,
	[Amount] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[BookId] [int] NULL,
	[OrderId] [int] NULL,
)