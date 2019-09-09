CREATE TABLE [dbo].[Asia_Table] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [Period]      DATE       NOT NULL,
    [Oil]         FLOAT (53) NOT NULL,
    [Benzene]     FLOAT (53) NOT NULL,
    [Caprolactam] FLOAT (53) NOT NULL,
    [Polyamid]    FLOAT (53) NOT NULL,
    [FPT]         FLOAT (53) NOT NULL,
    [CF]          FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

