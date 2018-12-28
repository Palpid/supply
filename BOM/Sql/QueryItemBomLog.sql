SELECT        
	T0.Code
	, T1.Version
	, T1.Subversion
	, T1.VersionDate
	, T0.ItemCode
	--, T0.Factory
	,	CASE 
			WHEN isnull(T2.CardFName,'') != '' THEN T2.CardFName 
			WHEN isnull(T2.CardName,'') != '' THEN T2.CardName
			ELSE T0.Factory 
		END AS Factory
	, T0.CreateDate
	, T1.CodeBom
	, T0.Factory + ' v.' + CONVERT(nvarchar(10),T1.Version) + '.' + CONVERT(nvarchar(10),T1.Subversion) AS FactoryVersion
	, T1.ItemCode AS ItemCode
	, T1.BomBreakdown
	, T1.Length
	, T1.Width
	, T1.Height
	, T1.Density
	, T1.NumberOfParts
	, T1.Coefficient1
	, T1.Coefficient2
	, T1.Scrap
	, T1.Quantity
	, T1.Supplied
	, T1.[User]
	, T1.VersionDate
FROM ETN_BOM_HEAD AS T0 WITH (NOLOCK)
INNER JOIN ETN_BOM_LINES_LOG AS T1 WITH (NOLOCK) ON T1.CodeBom = T0.Code
INNER JOIN OCRD T2 ON T2.CardCode = T0.Factory
WHERE        
	T0.ItemCode = @item
ORDER BY T0.Factory, T1.Version	DESC, T1.Subversion DESC