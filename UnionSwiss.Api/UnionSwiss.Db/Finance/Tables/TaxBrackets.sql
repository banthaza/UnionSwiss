CREATE TABLE [Finance].[TaxBrackets]
(
	[Id] bigint NOT NULL PRIMARY KEY IDENTITY, 
    [TaxYearId] BIGINT NOT NULL, 
    [MinQualifyingValue] INT NOT NULL, 
    [MaxQualifyingValue] INT NOT NULL, 
    [BaseTaxValue] INT NOT NULL, 
    [IncrementMultiplier] DECIMAL(18, 4) NOT NULL, 
	 CONSTRAINT [FK_TaxBracket_TaxYears] FOREIGN KEY ([TaxYearId]) REFERENCES [Finance].[TaxYears]([Id])
)
