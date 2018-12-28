SELECT        
	T0.Code
	, T0.Version
	, T0.Subversion
	, T0.VersionDate
	, T0.ItemCode
	, T0.Factory
	, T0.CreateDate
	, T1.CodeBom
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
FROM ETN_BOM_HEAD AS T0 WITH (NOLOCK)
INNER JOIN ETN_BOM_LINES AS T1 WITH (NOLOCK) ON T1.CodeBom = T0.Code
WHERE        
	T0.ItemCode = @item OR @item = ''