
/*
-- =============================================
-- Author:		SACHCHIDANAND JHA
-- Create date: 01 JUNE 2023
-- Description:	GET CHECK OF APPROVAL PROCESS COMPLETION FOR CUSTOMER 
-- =============================================
*/
CREATE OR ALTER   PROCEDURE [dbo].[GetApprovalCompletion] 
@CustomerId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @result bit;
	DECLARE @count1 NUMERIC;
	DECLARE @count2 NUMERIC;
	DECLARE @versionId NUMERIC;
	
	SET @versionId = (select max(Id) from dbo.CustomerVersionMaster where CustomerId = @CustomerId)

	set @count1 = (select count(*)
		FROM RiskFactor RF
			LEFT JOIN RiskSubFactor RSF ON RF.Id=RSF.RiskFactorId AND RF.CustomerId=RSF.CustomerId
			LEFT JOIN RiskSubFactorResponse RSR ON RSF.Id=RSR.RiskSubFactorId AND RSF.RiskFactorId=RSR.RiskFactorId
				AND RSF.CustomerId=RSR.CustomerId
		WHERE RF.CustomerId= @CustomerId )

	set @count2 = (select count(*)
		FROM RiskFactor RF
		INNER JOIN dbo.ApprovalRequests AR ON AR.StageId = RF.StageId AND AR.RiskTypeId = RF.RiskTypeId AND AR.GeographicPresenceId = RF.GeographicPresenceId
		AND AR.CustomerSegmentId = RF.CustomerSegmentId AND AR.BusinessSegmentId = RF.BusinessSegmentId AND AR.CustomerId = RF.CustomerId
			--INNER JOIN dbo.CustomerVersionMaster CV on RF.CustomerId = CV.CustomerId
			LEFT JOIN RiskSubFactor RSF ON RF.Id=RSF.RiskFactorId AND RF.CustomerId=RSF.CustomerId
			LEFT JOIN RiskSubFactorResponse RSR ON RSF.Id=RSR.RiskSubFactorId AND RSF.RiskFactorId=RSR.RiskFactorId
				AND RSF.CustomerId=RSR.CustomerId
		WHERE RF.CustomerId= @CustomerId AND AR.CreatedOn > (select max(CreatedOn) from dbo.CustomerVersionMaster where CustomerId = @CustomerId) AND AR.FinalStatus = 5)

		IF(@count1 = @count2)
		BEGIN
			SET @result = 1;
		END
		ELSE
		BEGIN
			SET @result = 0;
		END
		select @result AS Result;
END