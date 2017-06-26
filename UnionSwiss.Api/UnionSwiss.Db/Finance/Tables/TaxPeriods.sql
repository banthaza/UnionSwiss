CREATE TABLE [Finance].[TaxPeriods]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
	[TaxYearId] BIGINT NOT NULL,
    [StartDate] DATETIME NULL, 
    [EndDate] DATETIME NULL, 
    CONSTRAINT [FK_TaxPeriods_TaxYears] FOREIGN KEY ([TaxYearId]) REFERENCES [Finance].[TaxYears]([Id])
	
)
