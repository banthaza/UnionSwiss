CREATE TABLE [HR].[Employees]
(
	[Id] bigint NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    [AnnualSalary] INT NOT NULL DEFAULT 0, 
    [PensionContributionPercentage] INT NOT NULL DEFAULT 0, 
    [StartDate] DATE NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [CK_Employees_AnnualSalary_Min] CHECK (AnnualSalary >=0),
    CONSTRAINT [CK_Employees_PensionContributionPercentage_Max] CHECK (PensionContributionPercentage <=50),
	CONSTRAINT [CK_Employees_PensionContributionPercentage_Min] CHECK (PensionContributionPercentage >=0),

)
