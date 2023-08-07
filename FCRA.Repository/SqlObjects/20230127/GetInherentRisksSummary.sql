/*
-- =============================================
-- Author:		ATUL KUMAR
-- Create date: 27 JAN 2023
-- Description:	Get Inherent Risks Summary
-- =============================================
*/
CREATE OR ALTER PROCEDURE GetInherentRisksSummary
AS
BEGIN
	SET NOCOUNT ON;

    ;WITH CTE_RETAIL AS
	(SELECT RFR.Name AS Category, RFR.WeightPercentage, RFRR.TotalWeightedScore 
		, RFR.LowRiskRange, RFR.MediumRiskMinRange, RFR.MediumRiskMaxRange, RFR.HighRiskRange
		FROM RiskType RTR WITH (NOLOCK)
		INNER JOIN RiskFactor RFR WITH (NOLOCK) ON RFR.RiskTypeId=RTR.Id
		LEFT JOIN RiskFactorResponse RFRR WITH (NOLOCK) ON RFR.Id=RFRR.RiskFactorId
	WHERE RTR.Id=1)
	,CTE_CORPORATE AS
	(SELECT RFR.Name AS Category, RFR.WeightPercentage, RFRR.TotalWeightedScore 
		, RFR.LowRiskRange, RFR.MediumRiskMinRange, RFR.MediumRiskMaxRange, RFR.HighRiskRange
		FROM RiskType RTR WITH (NOLOCK)
		INNER JOIN RiskFactor RFR WITH (NOLOCK) ON RFR.RiskTypeId=RTR.Id
		LEFT JOIN RiskFactorResponse RFRR WITH (NOLOCK) ON RFR.Id=RFRR.RiskFactorId
	WHERE RTR.Id=2)

	SELECT COALESCE(R.Category, C.Category) AS Category, COALESCE(R.WeightPercentage, C.WeightPercentage) AS WeightPercentage
		, R.TotalWeightedScore AS RetailScore, C.TotalWeightedScore AS CorporateScore
		, CONVERT(DECIMAL(18,2), (ISNULL(R.TotalWeightedScore,0.00)+ ISNULL(C.TotalWeightedScore,0.00))/2) AS AggregateRiskScore
		, CONVERT(DECIMAL(18,2), CONVERT(DECIMAL(18,2), ((ISNULL(R.TotalWeightedScore,0.00)+ ISNULL(C.TotalWeightedScore,0.00))/2))* (COALESCE(R.WeightPercentage, C.WeightPercentage)/100)) AS WeightedScore
		, COALESCE(R.LowRiskRange, C.LowRiskRange) AS LowRiskRange
		, COALESCE(R.MediumRiskMinRange, C.MediumRiskMinRange) AS MediumRiskMinRange
		, COALESCE(R.MediumRiskMaxRange, C.MediumRiskMaxRange) AS MediumRiskMaxRange
		, COALESCE(R.HighRiskRange, C.HighRiskRange) AS HighRiskRange
		FROM CTE_RETAIL R
		FULL OUTER JOIN CTE_CORPORATE C ON R.Category=C.Category
END
GO