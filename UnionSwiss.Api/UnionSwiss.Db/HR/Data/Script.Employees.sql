SET
   IDENTITY_INSERT [HR].[Employees] 
   ON;
MERGE INTO [HR].[Employees] AS trgt USING (
VALUES
   ( 1, N'Andrew', N'Baker', 60050, 9, 'Apr  1 2017'), 
   ( 2, N'Chris', N'Davies', 120000, 10, 'Jun 24 2017')
) AS src([Id], [FirstName], [LastName], [AnnualSalary], [PensionContributionPercentage], [StartDate]) 
   ON trgt.[Id] = src.[Id] 
   WHEN
      MATCHED 
   THEN
      UPDATE
      SET
         [FirstName] = src.[FirstName] , [LastName] = src.[LastName] , [AnnualSalary] = src.[AnnualSalary] , [PensionContributionPercentage] = src.[PensionContributionPercentage] , [StartDate] = src.[StartDate] 
      WHEN
         NOT MATCHED BY TARGET 
      THEN
         INSERT ([Id], [FirstName], [LastName], [AnnualSalary], [PensionContributionPercentage], [StartDate]) 
      VALUES
         (
            [Id], [FirstName], [LastName], [AnnualSalary], [PensionContributionPercentage], [StartDate]
         )
      WHEN
         NOT MATCHED BY SOURCE 
      THEN
         DELETE
;
         SET
            IDENTITY_INSERT [HR].[Employees] OFF;