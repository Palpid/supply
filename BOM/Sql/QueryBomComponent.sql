SELECT        
	T0.Code, T0.[Version], T0.Subversion, T0.VersionDate, T0.ItemCode, T0.Factory, T0.CreateDate,
	CASE WHEN ISNULL(T2.CardFName,'') = '' THEN T2.CardFName ELSE T2.CardName END AS FactoryName,
	T3.U_ETN_stat, T1.ItemCode AS ComponentCode, T1.Quantity AS ComponentQuantity
FROM ETN_BOM_HEAD T0
INNER JOIN ETN_BOM_LINES T1 ON T1.CodeBom = T0.Code
INNER JOIN OCRD T2 ON T2.CardCode = T0.Factory
INNER JOIN OITM T3 ON  T3.ItemCode = T0.ItemCode
WHERE T1.ItemCode = @itemCode

/*
SELECT        
	T0.Code, T0.[Version], T0.Subversion, T0.VersionDate, T0.ItemCode, T0.Factory, T0.CreateDate,
	T2.CardCode, T2.CardName, T2.CardFName, T2.GroupCode
FROM ETN_BOM_HEAD T0
INNER JOIN ETN_BOM_LINES T1 ON T1.CodeBom = T0.Code
INNER JOIN OCRD T2 ON T2.CardCode = T0.Factory
WHERE T1.ItemCode = @itemCode
*/