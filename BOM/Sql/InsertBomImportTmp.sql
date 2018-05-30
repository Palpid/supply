INSERT INTO [dbo].[ETN_BOM_IMPORT_TMP]
	([ImportGUID]
	,[InsertDate]
	,[Factory]
	,[ItemCode]
	,[ComponentCode]
	,[BomBreakdown]
	,[Length]
	,[Width]
	,[Height]
	,[Density]
	,[NumberOfParts]
	,[Coefficient1]
	,[Coefficient2]
	,[Scrap]
	,[Quantity]
	,[Imported]
	,[ImportDate]
	,[ErrorMsg])
SELECT
	@importGUID
	,GETDATE()  AS InsertDate
	,@factory
	,@itemCode
	,@componentCode
	,@bomBreakdown
	,@length
	,@width
	,@height
	,@density
	,@numberOfParts
	,@coefficient1
	,@coefficient2
	,@scrap
	,@quantity
	,0 AS Imported
	,NULL AS ImportDate
	,NULL AS ErrorMsg


