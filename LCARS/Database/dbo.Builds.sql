CREATE TABLE [dbo].[Builds] (
    [Id]             INT            NOT NULL,
    [ServerPassword] NVARCHAR (MAX) NULL,
    [ServerUrl]      NVARCHAR (MAX) NULL,
    [ServerUsername] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Builds] PRIMARY KEY CLUSTERED ([Id] ASC)
);

