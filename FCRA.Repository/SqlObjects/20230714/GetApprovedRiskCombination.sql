/*
-- =============================================
-- Author:		SACHCHIDANAND JHA
-- Create date: 21 JUNE 2023
-- Description:	GET Distinct combination for Risk Factor 
-- =============================================
*/
CREATE OR ALTER PROCEDURE [dbo].[GetApprovedRiskCombination] 
 @VersionId int

AS
BEGIN
select distinct  R.StageId, R.RiskTypeId, R.GeographicPresenceId, R.CustomerSegmentId, R.BusinessSegmentId from dbo.ApprovedRiskFactorResponse A
INNER JOIN dbo.RiskFactor R  ON A.RiskFactorId = R.Id
 where A.ApprovalId = @VersionId

END