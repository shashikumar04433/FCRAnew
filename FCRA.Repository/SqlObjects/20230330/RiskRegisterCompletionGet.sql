/*
-- =============================================
-- Author:		ATUL KUMAR
-- Create date: 30 MAR 2023
-- Description:	GET PERCENTAGE COMPLETION
-- =============================================
*/
CREATE OR ALTER PROCEDURE RiskRegisterCompletionGet
@CustomerId INT,
@CategoryType INT=1, -- 1=Stage, 2=Risk Type, 3=Geographic Presence, 4=Business Segment, 5=Business Segment
@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	IF (@CategoryType=1)
	BEGIN
		SELECT RF.StageId AS Id, COUNT(1) AS TotalQuestions, SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) TotalCompleted
			, (SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) * 100)/ COUNT(1) AS TotalPercentage
		FROM RiskFactor RF
			LEFT JOIN RiskSubFactor RSF ON RF.Id=RSF.RiskFactorId AND RF.CustomerId=RSF.CustomerId
			LEFT JOIN RiskSubFactorResponse RSR ON RSF.Id=RSR.RiskSubFactorId AND RSF.RiskFactorId=RSR.RiskFactorId
				AND RSF.CustomerId=RSR.CustomerId
		WHERE RF.CustomerId=@CustomerId
		GROUP BY RF.StageId;
		RETURN;
	END

	IF (@CategoryType=2)
	BEGIN
		SELECT RF.RiskTypeId AS Id, COUNT(1) AS TotalQuestions, SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) TotalCompleted
			, (SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) * 100)/ COUNT(1) AS TotalPercentage
		FROM RiskFactor RF
			LEFT JOIN RiskSubFactor RSF ON RF.Id=RSF.RiskFactorId AND RF.CustomerId=RSF.CustomerId
			LEFT JOIN RiskSubFactorResponse RSR ON RSF.Id=RSR.RiskSubFactorId AND RSF.RiskFactorId=RSR.RiskFactorId
				AND RSF.CustomerId=RSR.CustomerId
		WHERE RF.CustomerId=@CustomerId AND RF.StageId=@CategoryId
		GROUP BY RF.RiskTypeId;
		RETURN;
	END

	IF (@CategoryType=3)
	BEGIN
		SELECT RF.GeographicPresenceId AS Id, COUNT(1) AS TotalQuestions, SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) TotalCompleted
			, (SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) * 100)/ COUNT(1) AS TotalPercentage
		FROM RiskFactor RF
			LEFT JOIN RiskSubFactor RSF ON RF.Id=RSF.RiskFactorId AND RF.CustomerId=RSF.CustomerId
			LEFT JOIN RiskSubFactorResponse RSR ON RSF.Id=RSR.RiskSubFactorId AND RSF.RiskFactorId=RSR.RiskFactorId
				AND RSF.CustomerId=RSR.CustomerId
		WHERE RF.CustomerId=@CustomerId AND RF.RiskTypeId=@CategoryId
		GROUP BY RF.GeographicPresenceId;
		RETURN;
	END

	IF (@CategoryType=4)
	BEGIN
		SELECT RF.CustomerSegmentId AS Id, COUNT(1) AS TotalQuestions, SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) TotalCompleted
			, (SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) * 100)/ COUNT(1) AS TotalPercentage
		FROM RiskFactor RF
			LEFT JOIN RiskSubFactor RSF ON RF.Id=RSF.RiskFactorId AND RF.CustomerId=RSF.CustomerId
			LEFT JOIN RiskSubFactorResponse RSR ON RSF.Id=RSR.RiskSubFactorId AND RSF.RiskFactorId=RSR.RiskFactorId
				AND RSF.CustomerId=RSR.CustomerId
		WHERE RF.CustomerId=@CustomerId AND RF.GeographicPresenceId=@CategoryId
		GROUP BY RF.CustomerSegmentId;
		RETURN;
	END

	IF (@CategoryType=5)
	BEGIN
		SELECT RF.BusinessSegmentId AS Id, COUNT(1) AS TotalQuestions, SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) TotalCompleted
			, (SUM(CASE WHEN ISNULL(RSR.Score, 0)>0 THEN 1 ELSE 0 END) * 100)/ COUNT(1) AS TotalPercentage
		FROM RiskFactor RF
			LEFT JOIN RiskSubFactor RSF ON RF.Id=RSF.RiskFactorId AND RF.CustomerId=RSF.CustomerId
			LEFT JOIN RiskSubFactorResponse RSR ON RSF.Id=RSR.RiskSubFactorId AND RSF.RiskFactorId=RSR.RiskFactorId
				AND RSF.CustomerId=RSR.CustomerId
		WHERE RF.CustomerId=@CustomerId AND RF.CustomerSegmentId=@CategoryId
		GROUP BY RF.BusinessSegmentId;
		RETURN;
	END
END
GO