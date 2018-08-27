﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BOM.Properties {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BOM.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap bill_of_materials {
            get {
                object obj = ResourceManager.GetObject("bill_of_materials", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Byte[].
        /// </summary>
        internal static byte[] BOM_Template {
            get {
                object obj = ResourceManager.GetObject("BOM_Template", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a DELETE  [dbo].[ETN_BOM_LINES]
        ///WHERE [CodeBom] = @codeBom.
        /// </summary>
        internal static string DeleteBomLines {
            get {
                return ResourceManager.GetString("DeleteBomLines", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Drawing.Icon similar a (Icono).
        /// </summary>
        internal static System.Drawing.Icon etnia_icon {
            get {
                object obj = ResourceManager.GetObject("etnia_icon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap excel_import {
            get {
                object obj = ResourceManager.GetObject("excel_import", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a EXECUTE [dbo].[ETN_sp_BOM_IMPORT] 
        ///   @guid
        ///  ,@user
        ///
        ///
        ///.
        /// </summary>
        internal static string ExecBomImport {
            get {
                return ResourceManager.GetString("ExecBomImport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a EXECUTE [ETN_sp_BOM_MASSIVE_UPDATE] 
        ///	@originalItem,
        ///	@changeTo,
        ///	@user
        ///
        ///
        ///.
        /// </summary>
        internal static string ExecBomMassiveUpdate {
            get {
                return ResourceManager.GetString("ExecBomMassiveUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[ETN_BOM_HEAD]
        ///	([Version]
        ///	,[Subversion]
        ///	,[VersionDate]
        ///	,[ItemCode]
        ///	,[Factory]
        ///	,[CreateDate])
        ///VALUES(
        ///	1
        ///	,0
        ///	,GETDATE()
        ///	,@itemCode
        ///	,@factory
        ///	,GETDATE()
        ///	);
        ///SELECT @@IDENTITY.
        /// </summary>
        internal static string InsertBomHead {
            get {
                return ResourceManager.GetString("InsertBomHead", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[ETN_BOM_IMPORT_TMP]
        ///	([ImportGUID]
        ///	,[InsertDate]
        ///	,[Factory]
        ///	,[ItemCode]
        ///	,[ComponentCode]
        ///	,[BomBreakdown]
        ///	,[Length]
        ///	,[Width]
        ///	,[Height]
        ///	,[Density]
        ///	,[NumberOfParts]
        ///	,[Coefficient1]
        ///	,[Coefficient2]
        ///	,[Scrap]
        ///	,[Quantity]
        ///	,[Imported]
        ///	,[ImportDate]
        ///	,[ErrorMsg])
        ///SELECT
        ///	@importGUID
        ///	,GETDATE()  AS InsertDate
        ///	,@factory
        ///	,@itemCode
        ///	,@componentCode
        ///	,@bomBreakdown
        ///	,@length
        ///	,@width
        ///	,@height
        ///	,@density
        ///	,@numberOfParts
        ///	,@coefficient1
        ///	,@coefficient [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string InsertBomImportTmp {
            get {
                return ResourceManager.GetString("InsertBomImportTmp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[ETN_BOM_LINES]
        ///	([CodeBom]
        ///	,[ItemCode]
        ///	,[BomBreakdown]
        ///	,[Length]
        ///	,[Width]
        ///	,[Height]
        ///	,[Density]
        ///	,[NumberOfParts]
        ///	,[Coefficient1]
        ///	,[Coefficient2]
        ///	,[Scrap]
        ///	,[Quantity])
        ///SELECT
        ///	@codeBom
        ///	,@itemCode
        ///	,@bomBreakdown
        ///	,@length
        ///	,@width
        ///	,@height
        ///	,@density
        ///	,@numberOfParts
        ///	,@coefficient1
        ///	,@coefficient2
        ///	,@scrap
        ///	,@quantity.
        /// </summary>
        internal static string InsertBomLines {
            get {
                return ResourceManager.GetString("InsertBomLines", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a 
        ///
        ///INSERT INTO ETN_BOM_LINES_LOG
        ///	([CodeBom]
        ///	,[Version]
        ///	,[Subversion]
        ///	,[VersionDate]
        ///	,[ItemCode]
        ///	,[BomBreakdown]
        ///	,[Length]
        ///	,[Width]
        ///	,[Height]
        ///	,[Density]
        ///	,[NumberOfParts]
        ///	,[Coefficient1]
        ///	,[Coefficient2]
        ///	,[Scrap]
        ///	,[Quantity]
        ///	,[User])
        ///SELECT
        ///	T1.[CodeBom]
        ///	,T0.[Version]
        ///	,T0.[Subversion]
        ///	,T0.[VersionDate]
        ///	,T1.[ItemCode]
        ///	,T1.[BomBreakdown]
        ///	,T1.[Length]
        ///	,T1.[Width]
        ///	,T1.[Height]
        ///	,T1.[Density]
        ///	,T1.[NumberOfParts]
        ///	,T1.[Coefficient1]
        ///	,T1.[Coefficient2]
        ///	,T1.[S [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string InsertBomLog {
            get {
                return ResourceManager.GetString("InsertBomLog", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT IdBomBreakdown, Description, SubGroup
        ///FROM ETN_BOM_BREAKDOWN.
        /// </summary>
        internal static string QueryBomBreakdown {
            get {
                return ResourceManager.GetString("QueryBomBreakdown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT DISTINCT        
        ///	T2.ItemCode
        ///	,T2.ItemName
        ///	,T2.U_ETN_MODEL
        ///	,T2.U_OPN_CATSUP
        ///	,T2.ItmsGrpCod
        ///	,T2.U_ETN_TIPART
        ///	,T3.[Name] as TipArtDesc
        ///	,T2.U_ETN_ETN_SUBTIPART
        ///	,T2.U_OPN_CAT
        ///FROM ETN_BOM_HEAD AS T0 WITH (NOLOCK)
        ///INNER JOIN ETN_BOM_LINES AS T1 WITH (NOLOCK) ON T1.CodeBom = T0.Code
        ///INNER JOIN OITM T2 WITH (NOLOCK) ON T2.ItemCode = T1.ItemCode
        ///LEFT JOIN [@ETN_TIPART] T3 WITH (NOLOCK) ON T3.Code = T2.U_ETN_TIPART
        ///WHERE        
        ///	T0.ItemCode = @item
        ///.
        /// </summary>
        internal static string QueryDetailItemsInBom {
            get {
                return ResourceManager.GetString("QueryDetailItemsInBom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT CardCode, CardName, GroupCode FROM OCRD
        ///WHERE CardType = &apos;S&apos;.
        /// </summary>
        internal static string QueryFactories {
            get {
                return ResourceManager.GetString("QueryFactories", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT        
        ///	Id
        ///	, ImportGUID
        ///	, InsertDate
        ///	, Factory
        ///	, ItemCode
        ///	, ComponentCode
        ///	, BomBreakdown
        ///	, [Length]
        ///	, Width
        ///	, Height
        ///	, Density
        ///	, NumberOfParts
        ///	, Coefficient1
        ///	, Coefficient2
        ///	, Scrap
        ///	, Quantity
        ///	, Imported
        ///	, ImportDate
        ///	, ErrorMsg
        ///FROM ETN_BOM_IMPORT_TMP
        ///WHERE ImportGUID = @importGUID.
        /// </summary>
        internal static string QueryImportBomTmp {
            get {
                return ResourceManager.GetString("QueryImportBomTmp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT        
        ///	T0.Code
        ///	, T0.Version
        ///	, T0.Subversion
        ///	, T0.VersionDate
        ///	, T0.ItemCode
        ///	, T0.Factory
        ///	, T0.CreateDate
        ///	, T1.CodeBom
        ///	, T1.ItemCode AS ItemCode
        ///	, T1.BomBreakdown
        ///	, T1.Length
        ///	, T1.Width
        ///	, T1.Height
        ///	, T1.Density
        ///	, T1.NumberOfParts
        ///	, T1.Coefficient1
        ///	, T1.Coefficient2
        ///	, T1.Scrap
        ///	, T1.Quantity
        ///FROM ETN_BOM_HEAD AS T0 WITH (NOLOCK)
        ///INNER JOIN ETN_BOM_LINES AS T1 WITH (NOLOCK) ON T1.CodeBom = T0.Code
        ///WHERE        
        ///	T0.ItemCode = @item --&apos;4 BORN BLSL&apos;.
        /// </summary>
        internal static string QueryItemBom {
            get {
                return ResourceManager.GetString("QueryItemBom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT 
        ///	T0.ItemCode
        ///	,T0.ItemName
        ///	,T0.U_ETN_MODEL
        ///	,T0.U_OPN_CATSUP
        ///	,T0.ItmsGrpCod
        ///	,T0.U_ETN_TIPART
        ///	,T0.U_ETN_ETN_SUBTIPART
        ///	,T0.U_OPN_CAT
        ///	,T0.CardCode
        ///	,T1.Code
        ///	,T1.[Name] 
        ///FROM OITM T0 WITH (NOLOCK)
        ///LEFT JOIN [@ETN_MODELS] T1 WITH (NOLOCK) ON T1.Code = T0.U_ETN_MODEL	
        ///WHERE 
        ///	U_OPN_CATSUP = &apos;01&apos;/*AND 
        ///	(ItemCode like &apos;%4 BORN%&apos; OR ItemCode like &apos;%MARAIS SUN%&apos; or ItemCode = &apos;5 KYOTO BKRD&apos;)*/
        ///ORDER BY T0.ItemName
        ///.
        /// </summary>
        internal static string QueryItems {
            get {
                return ResourceManager.GetString("QueryItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT DISTINCT        
        ///	T0.ItemCode
        ///	,T0.ItemName
        ///	,T0.U_ETN_MODEL
        ///	,T0.U_OPN_CATSUP
        ///	,T0.ItmsGrpCod
        ///	,T0.U_ETN_TIPART
        ///	,T0.CardCode
        ///	,T1.[Name] as TipArtDesc
        ///	,T0.U_ETN_ETN_SUBTIPART
        ///	,T0.U_OPN_CAT
        ///FROM  OITM T0 WITH (NOLOCK)
        ///LEFT JOIN [@ETN_TIPART] T1 WITH (NOLOCK) ON T1.Code = T0.U_ETN_TIPART
        ///WHERE  T0.ItmsGrpCod in (103,131,132,133,134)
        ///ORDER BY T0.ItmsGrpCod, ItemCode.
        /// </summary>
        internal static string QueryPossibleItemsForBom {
            get {
                return ResourceManager.GetString("QueryPossibleItemsForBom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap update {
            get {
                object obj = ResourceManager.GetObject("update", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE [ETN_BOM_HEAD]
        ///SET [Subversion] = [Subversion] + 1, [VersionDate] = GETDATE()
        ///WHERE Code = @codeBom.
        /// </summary>
        internal static string UpdateBomHead {
            get {
                return ResourceManager.GetString("UpdateBomHead", resourceCulture);
            }
        }
    }
}
