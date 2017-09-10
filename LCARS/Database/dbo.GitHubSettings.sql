CREATE TABLE [dbo].[GitHubSettings] (
    [Id]                   INT            NOT NULL,
    [Username]             NVARCHAR (50)  NULL,
    [Password]             NVARCHAR (50)  NULL,
    [BaseUrl]              NVARCHAR (MAX) NULL,
    [BranchThreshold]      INT            NOT NULL,
    [Owner]                NVARCHAR (MAX) NULL,
    [PullRequestThreshold] INT            NOT NULL,
    [RepositoriesString]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_GitHubSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

