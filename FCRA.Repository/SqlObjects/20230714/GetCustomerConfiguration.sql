/*
-- =============================================
-- Author:		SACHCHIDANAND JHA
-- Create date: 29 MAY 2023
-- Description:	GET CUSTOMER FIELD CONFIGURATION
-- =============================================
*/
CREATE OR ALTER   PROCEDURE [dbo].[GetCustomerConfiguration]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT F.Id AS FieldId
		,F.[Name] AS FieldName
		, ISNULL(CC.[Visible],0) [Visible]
	FROM FormMaster F
		INNER JOIN CustomerConfiguration CC ON F.Id=CC.FieldId
	ORDER BY F.[Id]
END


