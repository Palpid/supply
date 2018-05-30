INSERT INTO [dbo].[ETN_BOM_HEAD]
	([Version]
	,[Subversion]
	,[VersionDate]
	,[ItemCode]
	,[Factory]
	,[CreateDate])
VALUES(
	1
	,0
	,GETDATE()
	,@itemCode
	,@factory
	,GETDATE()
	);
SELECT @@IDENTITY