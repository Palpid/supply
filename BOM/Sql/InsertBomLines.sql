INSERT INTO [dbo].[ETN_BOM_LINES]
	([CodeBom]
	,[ItemCode]
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
	,[Supplied])
SELECT
	@codeBom
	,@itemCode
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
	,@supplied