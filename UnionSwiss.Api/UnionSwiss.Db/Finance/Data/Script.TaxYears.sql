SET
   IDENTITY_INSERT [Finance].[TaxYears] 
   ON;
MERGE INTO [Finance].[TaxYears] AS trgt USING (
VALUES
   (5, 'Mar  1 2016 12:00:00:000AM', 'Feb 28 2017 12:00:00:000AM' )
) AS src([Id], [StartDate], [EndDate]) 
   ON trgt.[Id] = src.[Id] 
   WHEN
      MATCHED 
   THEN
      UPDATE
      SET
         [StartDate] = src.[StartDate] , [EndDate] = src.[EndDate] 
      WHEN
         NOT MATCHED BY TARGET 
      THEN
         INSERT ([Id], [StartDate], [EndDate]) 
      VALUES
         (
            [Id], [StartDate], [EndDate]
         )
      WHEN
         NOT MATCHED BY SOURCE 
      THEN
         DELETE
;
         SET
            IDENTITY_INSERT [Finance].[TaxYears] OFF;