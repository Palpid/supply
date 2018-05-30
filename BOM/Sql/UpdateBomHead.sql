UPDATE [ETN_BOM_HEAD]
SET [Subversion] = [Subversion] + 1, [VersionDate] = GETDATE()
WHERE Code = @codeBom