SELECT        
	Id
	, ImportGUID
	, InsertDate
	, Factory
	, ItemCode
	, ComponentCode
	, BomBreakdown
	, [Length]
	, Width
	, Height
	, Density
	, NumberOfParts
	, Coefficient1
	, Coefficient2
	, Scrap
	, Quantity
	, Supplied
	, Imported
	, ImportDate
	, ErrorMsg
FROM ETN_BOM_IMPORT_TMP
WHERE ImportGUID = @importGUID