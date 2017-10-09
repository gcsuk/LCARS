CREATE TABLE [dbo].[Environments] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [SiteId] INT            NOT NULL,
    [Status] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Environments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Environments_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Sites] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Environments_SiteId]
    ON [dbo].[Environments]([SiteId] ASC);

