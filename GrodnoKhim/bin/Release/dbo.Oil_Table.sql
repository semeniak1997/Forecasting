CREATE TABLE [dbo].[Oil_Table] (
    [Id]                  INT        IDENTITY (1, 1) NOT NULL,
    [Period]              DATE       NOT NULL,
    [OilPrise]            FLOAT (53) NOT NULL,
    [ForecastBenzene]     FLOAT (53) NULL,
    [ForecastCaprolactam] FLOAT (53) NULL,
    [ForecastPolyamid]    FLOAT (53) NULL,
    [ForecastFPT]         FLOAT (53) NULL,
    [ForecastCF]          FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

