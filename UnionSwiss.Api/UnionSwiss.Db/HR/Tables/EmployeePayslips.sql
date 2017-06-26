CREATE TABLE [HR].[EmployeePayslips]
(
	[Id] bigint NOT NULL PRIMARY KEY IDENTITY, 
    [EmployeeId] BIGINT NOT NULL, 
	[TaxPeriodId] BIGINT NOT NULL, 
    [FullName] NVARCHAR(100) NOT NULL,   /*Purposfully denormalised to preserve data in the event of change*/
    [PayPeriod] NVARCHAR(100) NOT NULL,  /*Purposfully denormalised to preserve data in the event of change*/
    [GrossIncome] INT NOT NULL, 
	[IncomeTax] INT NOT NULL, 
    [NetIncome] INT NOT NULL, 
    [Pension] INT NOT NULL, 

    CONSTRAINT [FK_EmployeePayslips_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [HR].[Employees]([Id])

)
