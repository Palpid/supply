SELECT        
	T0.Code, T0.[Version], T0.Subversion, T0.VersionDate, T0.ItemCode, T0.Factory, T0.CreateDate,
	T1.CardCode, T1.CardName, T1.CardFName, T1.GroupCode
FROM ETN_BOM_HEAD T0
INNER JOIN OCRD T1 ON T1.CardCode = T0.Factory