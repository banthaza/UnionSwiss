SET
   IDENTITY_INSERT [Finance].[TaxPeriods] 
   ON;
MERGE INTO [Finance].[TaxPeriods] AS trgt USING (
VALUES
   (
      25, 5, 'Mar  1 2016 12:00:00:000AM', 'Mar 31 2016 12:00:00:000AM'
   )
, 
   (
      26, 5, 'Apr  1 2016 12:00:00:000AM', 'Apr 30 2016 12:00:00:000AM'
   )
, 
   (
      27, 5, 'May  1 2016 12:00:00:000AM', 'May 31 2016 12:00:00:000AM'
   )
, 
   (
      28, 5, 'Jun  1 2016 12:00:00:000AM', 'Jun 30 2016 12:00:00:000AM'
   )
, 
   (
      29, 5, 'Jul  1 2016 12:00:00:000AM', 'Jul 31 2016 12:00:00:000AM'
   )
, 
   (
      30, 5, 'Aug  1 2016 12:00:00:000AM', 'Aug 31 2016 12:00:00:000AM'
   )
, 
   (
      31, 5, 'Sep  1 2016 12:00:00:000AM', 'Sep 30 2016 12:00:00:000AM'
   )
, 
   (
      32, 5, 'Oct  1 2016 12:00:00:000AM', 'Oct 31 2016 12:00:00:000AM'
   )
, 
   (
      33, 5, 'Nov  1 2016 12:00:00:000AM', 'Nov 30 2016 12:00:00:000AM'
   )
, 
   (
      34, 5, 'Dec  1 2016 12:00:00:000AM', 'Dec 31 2016 12:00:00:000AM'
   )
, 
   (
      35, 5, 'Jan  1 2017 12:00:00:000AM', 'Jan 31 2017 12:00:00:000AM'
   )
, 
   (
      36, 5, 'Feb  1 2017 12:00:00:000AM', 'Feb 28 2017 12:00:00:000AM'
   )
) AS src([Id], [TaxYearId], [StartDate], [EndDate]) 
   ON trgt.[Id] = src.[Id] 
   WHEN
      MATCHED 
   THEN
      UPDATE
      SET
         [TaxYearId] = src.[TaxYearId] , [StartDate] = src.[StartDate] , [EndDate] = src.[EndDate] 
      WHEN
         NOT MATCHED BY TARGET 
      THEN
         INSERT ([Id], [TaxYearId], [StartDate], [EndDate]) 
      VALUES
         (
            [Id], [TaxYearId], [StartDate], [EndDate]
         )
      WHEN
         NOT MATCHED BY SOURCE 
      THEN
         DELETE
;
         SET
            IDENTITY_INSERT [Finance].[TaxPeriods] OFF;