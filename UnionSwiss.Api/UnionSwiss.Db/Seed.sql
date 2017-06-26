/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\HR\data\Script.Employees.sql
:r .\Finance\data\Script.TaxYears.sql
:r .\Finance\data\Script.TaxPeriods.sql
:r .\Finance\data\Script.TaxBrackets.sql
