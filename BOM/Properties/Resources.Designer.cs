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
        ///FROM ETN_BOM_HEAD AS T0 
        ///INNER JOIN ETN_BOM_LINES AS T1 ON T1.CodeBom = T0.Code
        ///INNER JOIN OITM T2 ON T2.ItemCode = T1.ItemCode
        ///LEFT JOIN [@ETN_TIPART] T3 ON T3.Code = T2.U_ETN_TIPART
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
        ///FROM ETN_BOM_HEAD AS T0 
        ///INNER JOIN ETN_BOM_LINES AS T1 ON T1.CodeBom = T0.Code
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
        ///	,T1.Code
        ///	,T1.[Name] 
        ///FROM OITM T0 
        ///LEFT JOIN [@ETN_MODELS] T1 ON T1.Code = T0.U_ETN_MODEL	
        ///WHERE 
        ///	U_OPN_CATSUP = &apos;01&apos;AND 
        ///	(ItemCode like &apos;%4 BORN%&apos; OR ItemCode like &apos;%MARAIS SUN%&apos;)
        ///.
        /// </summary>
        internal static string QueryItems {
            get {
                return ResourceManager.GetString("QueryItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string QueryPossibleItemsForBom {
            get {
                return ResourceManager.GetString("QueryPossibleItemsForBom", resourceCulture);
            }
        }
    }
}
