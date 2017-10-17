CREATE TABLE [dbo].[SitesEnvironments] (
    [Id]      INT            NOT NULL IDENTITY,
    [SiteId]  INT            NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [SiteUrl] NVARCHAR (MAX) NOT NULL,
    [PingUrl] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SitesEnvironments_Sites] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Sites] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);
