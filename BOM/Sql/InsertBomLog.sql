

INSERT INTO ETN_BOM_LINES_LOG
	([CodeBom]
	,[Version]
	,[Subversion]
	,[VersionDate]
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
	,[User])
SELECT
	T1.[CodeBom]
	,T0.[Version]
	,T0.[Subversion]
	,T0.[VersionDate]
	,T1.[ItemCode]
	,T1.[BomBreakdown]
	,T1.[Length]
	,T1.[Width]
	,T1.[Height]
	,T1.[Density]
	,T1.[NumberOfParts]
	,T1.[Coefficient1]
	,T1.[Coefficient2]
	,T1.[Scrap]
	,T1.[Quantity]
	,@user
FROM ETN_BOM_HEAD T0
INNER JOIN ETN_BOM_LINES T1 ON T1.CodeBom = T0.Code
WHERE
	T0.Code = @codeBom