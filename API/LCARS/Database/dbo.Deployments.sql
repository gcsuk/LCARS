CREATE TABLE [dbo].[Deployments] (
    [Id]        INT            NOT NULL,
    [ServerUrl] NVARCHAR (MAX) NULL,
    [ServerKey] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

