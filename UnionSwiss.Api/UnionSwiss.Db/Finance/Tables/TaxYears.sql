﻿CREATE TABLE [Finance].[TaxYears]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL
)
