-- =============================================
-- Author:		Atul Kumar
-- Create date: 2021-Jan-2022
-- Description:	Get Roles Permissions mapping
-- =============================================
CREATE OR ALTER PROCEDURE GetAllRolePermissionMapping

AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, [Name], [Description], IsActive FROM RoleMaster ORDER BY [Name]

	SELECT Id, [Name], [Description], IsActive FROM TypeMaster ORDER BY [Name]

	SELECT Id, [Name], [Description], IsActive, TypeId FROM SubTypeMaster ORDER BY [Name]

	SELECT RoleId, TypeId, SubTypeId FROM RoleAccessMapping
END
GO
