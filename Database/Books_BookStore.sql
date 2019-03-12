USE [BookStore]
GO

CREATE TABLE Books(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Name NVARCHAR(255) NULL,
	Author NVARCHAR(255) NULL,
	Details NVARCHAR(255) NULL,
	ImgUrl NVARCHAR(255) NULL,
	Active Bit not null Default((1)),
	Price int null,
	PriceOffer int null
)

INSERT INTO [dbo].[Books]
           (
           [Name]
           ,[Author]
           ,[Details]
           ,[ImgUrl]
           ,[Active]
           ,[Price]
           ,[PriceOffer])
     VALUES
           (
		   'Harry Potter 1'
           ,'Harry Potter 1'
           ,'Harry Potter 1'
           ,'~/images/ggg.jpg'
           ,1
           ,19
           ,50
		   ),
		   ('Harry Potter 2'
           ,'Harry Potter 1'
           ,'Harry Potter 1'
           ,'~/images/ggg.jpg'
           ,1
           ,19
           ,50),
		   ('Harry Potter 3'
           ,'Harry Potter 1'
           ,'Harry Potter 1'
           ,'~/images/ggg.jpg'
           ,1
           ,19
           ,50),
		   ('Harry Potter 4'
           ,'Harry Potter 1'
           ,'Harry Potter 1'
           ,'~/images/ggg.jpg'
           ,1
           ,19
           ,50)
GO


