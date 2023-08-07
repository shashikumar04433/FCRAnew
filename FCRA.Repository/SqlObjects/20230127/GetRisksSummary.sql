/*
-- =============================================
-- Author:		ATUL KUMAR
-- Create date: 27 JAN 2023
-- Description:	Get Inherent Risks Summary
-- =============================================
*/
CREATE OR ALTER PROCEDURE GetRisksSummary
@RiskTypeId INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT RFR.Name AS Category, RFR.WeightPercentage, RFRR.TotalWeightedScore AS AggregateRiskScore
		, CONVERT(DECIMAL(18,2), ISNULL(RFRR.TotalWeightedScore,0.00)* (RFR.WeightPercentage/100)) AS WeightedScore
		, RFR.LowRiskRange, RFR.MediumRiskMinRange, RFR.MediumRiskMaxRange, RFR.HighRiskRange
		FROM RiskType RTR WITH (NOLOCK)
		INNER JOIN RiskFactor RFR WITH (NOLOCK) ON RFR.RiskTypeId=RTR.Id
		LEFT JOIN RiskFactorResponse RFRR WITH (NOLOCK) ON RFR.Id=RFRR.RiskFactorId
	WHERE RTR.Id=@RiskTypeId
END
GO
