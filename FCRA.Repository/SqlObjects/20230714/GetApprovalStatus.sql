
/*
-- =============================================
-- Author:		SACHCHIDANAND JHA
-- Create date: 03 JUNE 2023
-- Description:	GET APPROVAL STATUS 
-- =============================================
*/
CREATE OR ALTER   PROCEDURE [dbo].[GetApprovalStatus] 
@CustomerId INT,
@Status VARCHAR(2)
AS
BEGIN
	SET NOCOUNT ON;


	IF(@Status = 'A')
	BEGIN
	     select Distinct s.Name AS Stage, r.Name AS RiskType, co.Name AS Country
		 , c.Name AS Business, b.Name AS SubUnit, A.FinalStatus,
		 (CASE WHEN u.Name IS NULL THEN us.Name ELSE u.Name END ) AS ApprovedBy
		 , convert(varchar, A.PendingFrom, 103) AS PendingFrom
		 from dbo.ApprovalRequests A 
		 LEFT JOIN dbo.Stages s on s.Id = A.StageId
		 LEFT JOIN dbo.RiskType r on r.Id = A.RiskTypeId
		 LEFT JOIN dbo.GeographicPresence g on g.Id = A.GeographicPresenceId
		 INNER JOIN dbo.Country co on co.Id = g.CountryId
		 LEFT JOIN dbo.CustomerSegment c on c.Id = A.CustomerSegmentId
		 LEFT JOIN dbo.BusinessSegment b on b.Id = A.BusinessSegmentId
		 LEFT JOIN dbo.UserMaster u on u.Id = A.PendingWithUser
		 LEFT JOIN dbo.UserMaster us on us.Id = A.UpdatedBy
		 INNER JOIN dbo.ApprovalHistorys AH ON AH.ApprovalId = A.Id
		 WHERE A.CustomerId = @CustomerId AND (A.FinalStatus = 5 OR A.FinalStatus = 0)
	END
	ELSE IF(@Status = 'R' OR @Status = 'SR')
	BEGIN
	     select Distinct s.Name AS Stage, r.Name AS RiskType, co.Name AS Country
		 , c.Name AS Business, b.Name AS SubUnit, A.FinalStatus,
		 (CASE WHEN u.Name IS NULL THEN us.Name ELSE u.Name END ) AS ApprovedBy
		 , convert(varchar, A.PendingFrom, 103) AS PendingFrom
		 from dbo.ApprovalRequests A 
		 LEFT JOIN dbo.Stages s on s.Id = A.StageId
		 LEFT JOIN dbo.RiskType r on r.Id = A.RiskTypeId
		 LEFT JOIN dbo.GeographicPresence g on g.Id = A.GeographicPresenceId
		 INNER JOIN dbo.Country co on co.Id = g.CountryId
		 LEFT JOIN dbo.CustomerSegment c on c.Id = A.CustomerSegmentId
		 LEFT JOIN dbo.BusinessSegment b on b.Id = A.BusinessSegmentId
		 LEFT JOIN dbo.UserMaster u on u.Id = A.PendingWithUser
		 LEFT JOIN dbo.UserMaster us on us.Id = A.UpdatedBy
		 INNER JOIN dbo.ApprovalHistorys AH ON AH.ApprovalId = A.Id
		 WHERE A.CustomerId = @CustomerId AND (A.FinalStatus = 5 OR A.FinalStatus = 0) AND u.RoleId = 4
	END
	ELSE
	BEGIN
		 select Distinct s.Name AS Stage, r.Name AS RiskType, co.Name AS Country
		 , c.Name AS Business, b.Name AS SubUnit, A.FinalStatus,
		 (CASE WHEN u.Name IS NULL THEN us.Name ELSE u.Name END ) AS ApprovedBy
		 , convert(varchar, A.PendingFrom, 103) AS PendingFrom
		 from dbo.ApprovalRequests A 
		 LEFT JOIN dbo.Stages s on s.Id = A.StageId
		 LEFT JOIN dbo.RiskType r on r.Id = A.RiskTypeId
		 LEFT JOIN dbo.GeographicPresence g on g.Id = A.GeographicPresenceId
		 INNER JOIN dbo.Country co on co.Id = g.CountryId
		 LEFT JOIN dbo.CustomerSegment c on c.Id = A.CustomerSegmentId
		 LEFT JOIN dbo.BusinessSegment b on b.Id = A.BusinessSegmentId
		 LEFT JOIN dbo.UserMaster u on u.Id = A.PendingWithUser
		 LEFT JOIN dbo.UserMaster us on us.Id = A.UpdatedBy
		 INNER JOIN dbo.ApprovalHistorys AH ON AH.ApprovalId = A.Id
		 WHERE A.CustomerId = @CustomerId AND (A.FinalStatus <> 5 AND A.FinalStatus <> 0) AND u.RoleId = 5
	END


END