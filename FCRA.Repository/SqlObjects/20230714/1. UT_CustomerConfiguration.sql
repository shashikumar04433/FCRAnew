IF NOT EXISTS (SELECT * FROM SYS.TABLE_TYPES WHERE NAME ='[UT_CustomerConfiguration]')
CREATE TYPE [dbo].[UT_CustomerConfiguration] AS TABLE(
	[FieldId] [int] NULL,
	[Visible] [bit] NULL
)
GO


