CREATE TABLE [dbo].[IssueQueries] (
    [Id]       INT            NOT NULL,
    [Deadline] DATETIME2 (7)  NULL,
    [Jql]      NVARCHAR (MAX) NULL,
    [Name]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_IssueQueries] PRIMARY KEY CLUSTERED ([Id] ASC)
);

