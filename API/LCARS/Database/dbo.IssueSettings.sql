CREATE TABLE [dbo].[IssueSettings] (
    [Id]       INT            NOT NULL,
    [Username] NVARCHAR (50)  NULL,
    [Password] NVARCHAR (MAX) NULL,
    [Url]      NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

