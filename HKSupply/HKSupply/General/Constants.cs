using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.General
{
    public static class Constants
    {
        //** DB CONN **//
        public const string SQL_EXPRESS_CONN = "name=SqlExpressConn";
        public const string SQL_DEV_SERVER_CONN = "name=SqlDevServerConn";
        public const string SQL_DEV_EF_SERVER_CONN = "name=SqlDevEFServerConn";
        public const string SQL_PROD_SERVER_CONN = "";

        //** ITEM_GROUP **//
        public const string ITEM_GROUP_EY = "EY";
        public const string ITEM_GROUP_HW = "HW";
        public const string ITEM_GROUP_MT = "MT";
        public const string ITEM_GROUP_HF = "HF";
        public const string ITEM_GROUP_PROTO = "PROTO";

        //** DOCS FOLDERS **//
        public const string ITEMS_DOCS_PATH = "C:\\Temp\\ITEM_DOCS\\";
        public const string PROTO_DOCS_PATH = "C:\\Temp\\PROTO_DOCS\\";
        public const string ITEMS_PHOTOSWEB_PATH = "C:\\Temp\\ITEM_WEB_PHOTOS\\";

        public const string COLOR_PDF_FOLDER = "PDFCOLOR\\";
        public const string DRAWING_PDF_FOLDER = "PDFDRAWING\\";
        public const string DRAWING_DWG_FOLDER = "DWGDRAWING\\";
        public const string ITEM_PHOTO_FOLDER = "ITEMIMG\\";
        public const string ITEM_PHOTOWEB_FOLDER = "640x350\\";

        //** USER ATTRIBUTES **//
        public const string EY_USER_ATTR_01 = "EYATTR01";
        public const string EY_USER_ATTR_02 = "EYATTR02";
        public const string EY_USER_ATTR_03 = "EYATTR03";

        public const string HW_USER_ATTR_01 = "HWATTR01";
        public const string HW_USER_ATTR_02 = "HWATTR02";
        public const string HW_USER_ATTR_03 = "HWATTR03";

        public const string MT_USER_ATTR_01 = "MTATTR01";
        public const string MT_USER_ATTR_02 = "MTATTR02";
        public const string MT_USER_ATTR_03 = "MTATTR03";

        public const string HF_USER_ATTR_01 = "HFATTR01";
        public const string HF_USER_ATTR_02 = "HFATTR02";
        public const string HF_USER_ATTR_03 = "HFATTR03";

        //** HARDWARE GROUP TYPES **//
        public const string HW_GROUP_TYPE_DESING = "DESIGN";
        public const string HW_GROUP_TYPE_PRODUCTION = "PRODUCTION";

        //** ITEM TYPES IDs**//
        public const string ITEM_TYPE_FRONT = "02";
        public const string ITEM_TYPE_LEFT_TEMPLE = "03";
        public const string ITEM_TYPE_RIGHT_TEMPLE = "04";

        //** BREAKDOWN SUBGROUPS **//
        public const string BREAKDOWN_SUBGROUP_FRONT = "FRONT";
        public const string BREAKDOWN_SUBGROUP_TEMPLE = "TEMPLE";
        public const string BREAKDOWN_SUBGROUP_TEMPLE_LEFT = "TEMPLE_LEFT";
        public const string BREAKDOWN_SUBGROUP_TEMPLE_RIGHT = "TEMPLE_RIGHT";

        //** INTRANET ETNIA BARCELONA **//
        public const string INTRANET_ETNIA_BCN = "INTRANET";

        //** SUPPLY STATUS **//
        public const string SUPPLY_STATUS_OPEN = "OPN";
        public const string SUPPLY_STATUS_CLOSE = "CLS";
        public const string SUPPLY_STATUS_CANCEL = "CNL";

        //** SUPPLY DOC TYPES **//
        public const string SUPPLY_DOCTYPE_PO = "PO"; //Purchase Order
        public const string SUPPLY_DOCTYPE_SO = "SO"; //Sales Order
        public const string SUPPLY_DOCTYPE_QP = "QP"; //Quotation Proposal
        public const string SUPPLY_DOCTYPE_IV = "IV"; //Invoice
        public const string SUPPLY_DOCTYPE_PL = "PL"; //Paking List
        public const string SUPPLY_DOCTYPE_DN = "DN"; //Delivery Note

        //** ETNIA HK/BCN CODE **//
        public const string ETNIA_HK_COMPANY_CODE = "ETNLTD";
        public const string ETNIA_BCN_COMPANY_CODE = "ETNBCN";

        //**ROLES **//
        public const string ROLE_FACTORY = "FACTORY";

    }
}
