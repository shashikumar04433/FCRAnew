/*
-- =============================================
-- Author:		SACHCHIDANAND JHA
-- Create date: 29 MAY 2023
-- Description:	UPDATE VISIBILITY OF FIELDS
-- =============================================
*/
CREATE OR ALTER PROCEDURE [dbo].[UpdateCustomerConfiguration]
@configuration UT_CustomerConfiguration READONLY
AS 
BEGIN
BEGIN TRAN
	BEGIN TRY
		DELETE FROM CustomerConfiguration;

		INSERT INTO CustomerConfiguration(FieldId, FieldName, Visible)
		SELECT FieldId , (SELECT FM.Name FROM FormMaster FM WHERE FM.MenuId = 4 AND FM.Sequence = FieldId ), Visible  FROM @configuration;
				
		COMMIT;
		SELECT 1 AS Result, 'Process completed' Response;
	END TRY
	BEGIN CATCH
		ROLLBACK;
		SELECT 0 AS Result, 'Process failed ' +  ERROR_MESSAGE() Response;
	END CATCH
END