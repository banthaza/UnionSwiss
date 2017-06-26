SET
   IDENTITY_INSERT [Finance].[TaxBrackets] 
   ON;
MERGE INTO [Finance].[TaxBrackets] AS trgt USING (
VALUES
   (6, 5, 0, 18200, 0, 0), 
   ( 7, 5, 18201, 37000, 3572, 0.1900), 
   ( 9, 5, 37001, 80000, 3572, 0.3250), 
   ( 10, 5, 80001, 180000, 17547, 0.3700), 
   ( 13, 5, 180001, 2147483647, 54547, 0.4500)
) AS src([Id], [TaxYearId], [MinQualifyingValue], [MaxQualifyingValue], [BaseTaxValue], [IncrementMultiplier]) 
   ON trgt.[Id] = src.[Id] 
   WHEN
      MATCHED 
   THEN
      UPDATE
      SET
         [TaxYearId] = src.[TaxYearId] , [MinQualifyingValue] = src.[MinQualifyingValue] , [MaxQualifyingValue] = src.[MaxQualifyingValue] , [BaseTaxValue] = src.[BaseTaxValue] , [IncrementMultiplier] = src.[IncrementMultiplier] 
      WHEN
         NOT MATCHED BY TARGET 
      THEN
         INSERT ([Id], [TaxYearId], [MinQualifyingValue], [MaxQualifyingValue], [BaseTaxValue], [IncrementMultiplier]) 
      VALUES
         (
            [Id], [TaxYearId], [MinQualifyingValue], [MaxQualifyingValue], [BaseTaxValue], [IncrementMultiplier]
         )
      WHEN
         NOT MATCHED BY SOURCE 
      THEN
         DELETE
;
         SET
            IDENTITY_INSERT [Finance].[TaxBrackets] OFF;