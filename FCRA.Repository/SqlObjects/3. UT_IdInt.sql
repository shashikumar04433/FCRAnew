﻿IF NOT EXISTS (SELECT * FROM SYS.TABLE_TYPES WHERE NAME ='UT_IdInt')
CREATE TYPE UT_IdInt AS TABLE 
(
	Id INT
)
GO
