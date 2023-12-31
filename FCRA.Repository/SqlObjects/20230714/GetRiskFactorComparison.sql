/*
-- =============================================
-- Author:		SACHCHIDANAND JHA
-- Create date: 21 JUNE 2023
-- Description:	GET Risk Factor VERSION COMPARISON
-- =============================================
*/
CREATE OR ALTER PROCEDURE [dbo].[GetRiskFactorComparison] 
 @FromVersionId int,
 @ToVersionId int

AS
BEGIN
select A.Id, A.Name, A.StageId, A.WeightPercentage, D.TotalWeightedScore into #tmp1  from dbo.RiskFactor A
INNER JOIN dbo.ApprovalRequests B ON A.CustomerId= B.CustomerId AND A.StageId = B.StageId AND A.RiskTypeId = B.RiskTypeId AND A.GeographicPresenceId = B.GeographicPresenceId
AND A.CustomerSegmentId = B.CustomerSegmentId AND A.BusinessSegmentId = B.BusinessSegmentId
INNER JOIN dbo.RiskFactorResponse C ON C.RiskFactorId = A.Id
INNER JOIN dbo.ApprovedRiskFactorResponse D on D.RiskFactorId = A.Id AND B.Id = D.ApprovalId
WHERE B.Id = @FromVersionId and D.ApprovalId = @FromVersionId


select A.Id, A.Name, A.StageId, A.WeightPercentage, D.TotalWeightedScore into #tmp2 from dbo.RiskFactor A
INNER JOIN dbo.ApprovalRequests B ON A.CustomerId= B.CustomerId AND A.StageId = B.StageId AND A.RiskTypeId = B.RiskTypeId AND A.GeographicPresenceId = B.GeographicPresenceId
AND A.CustomerSegmentId = B.CustomerSegmentId AND A.BusinessSegmentId = B.BusinessSegmentId
INNER JOIN dbo.RiskFactorResponse C ON C.RiskFactorId = A.Id
INNER JOIN dbo.ApprovedRiskFactorResponse D on D.RiskFactorId = A.Id AND B.Id = D.ApprovalId
WHERE B.Id = @ToVersionId and D.ApprovalId = @ToVersionId

select A.Id, A.Name, A.StageId, C.Name AS StageName, A.WeightPercentage AS WeightPercentage1, A.TotalWeightedScore AS TotalWeightedScore1,
((CASE WHEN ISNULL(A.TotalWeightedScore, 0)>0 THEN A.TotalWeightedScore ELSE 0 END) * A.WeightPercentage/100) AS TotalPercentage1
, B.WeightPercentage  AS WeightPercentage2, B.TotalWeightedScore AS TotalWeightedScore2,
((CASE WHEN ISNULL(B.TotalWeightedScore, 0)>0 THEN B.TotalWeightedScore ELSE 0 END) * B.WeightPercentage/100) AS TotalPercentage2 from #tmp1 A
INNER JOIN #tmp2 B ON A.Id = B.Id
INNER JOIN Stages C ON A.StageId = C.Id


drop table #tmp1
drop table #tmp2

END