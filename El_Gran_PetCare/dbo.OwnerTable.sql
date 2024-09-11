CREATE TABLE [dbo].[OwnerTable]
(
	[ownerID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ownerName] NVARCHAR(50) NULL, 
    [ownerPhone] NVARCHAR(50) NULL, 
    [ownerEmail] NVARCHAR(50) NULL
)
