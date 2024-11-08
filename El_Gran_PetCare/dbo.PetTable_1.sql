CREATE TABLE [dbo].[PetTable] (
    [petID]      INT           IDENTITY (1, 1) NOT NULL,
    [petName]    NVARCHAR (50) NULL,
    [petSpecies] NVARCHAR (50) NULL,
    [petBreed]   NVARCHAR (50) NULL,
    [petGender]  NVARCHAR(50)           NULL,
    [petAge]     INT           NULL,
    [ownerID]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([petID] ASC),
    CONSTRAINT [FK_PetTable_ownerID] FOREIGN KEY ([ownerID]) REFERENCES [dbo].[OwnerTable] ([ownerID])
);

