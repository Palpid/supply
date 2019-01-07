using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Classes
{
    public class Constants
    {
        //---- DB ----//
        public const string DB_USER = "sa";
        public const string DB_PASSWORD = "EiB1OsCt";
        public const string SAP_USER_PASSWORD = "etniac210";

        //---- TipArt ----//
        public const string TIPART_SPAREPARTS_HK = "131";   //356 en BCN
        public const string TIPART_HARDWARE_HK = "132";     //161 en BCN
        public const string TIPART_LENSES_HK = "133";       //681 en BCN
        public const string TIPART_RAWMATERIAL_HK = "134";  //1204 en BCN

        //---- U_OPN_CAT ----//
        public const string CAT_SUN = "02";

        //---- Excel Import Columns ----//

        public const string XLS_FACTORY = "Factory";
        public const string XLS_ITEM_CODE = "Item Code";
        public const string XLS_COMPONENT_CODE = "Component code";
        public const string XLS_BOMBREAKDOWN = "BomBreakdown";
        public const string XLS_LENGTH = "Length";
        public const string XLS_WIDTH = "Width";
        public const string XLS_HEIGHT = "Height";
        public const string XLS_DENSITY = "Density";

        public const string XLS_NUMBER_OF_PARTS = "NumberOfParts";
        public const string XLS_COEFFICIENT1 = "Coefficient1";
        public const string XLS_COEFFICIENT2 = "Coefficient2";
        public const string XLS_SCRAP = "Scrap";
        public const string XLS_QUANTITY = "Quantity";
        public const string XLS_SUPPLIED = "Supplied";
    }
}
