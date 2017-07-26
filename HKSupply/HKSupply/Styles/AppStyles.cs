using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Styles
{
    public static class AppStyles
    {
        public static Color EtniaRed = Color.FromArgb(228, 37, 24);

        //***** Supply Status *****//
        //Cancel
        public static Color SupplyStatusCnlBKGD1 = Color.Salmon;
        public static Color SupplyStatusCnlBKGD2 = Color.SeaShell;
        //??
        public static Color SupplyStatusOpdBKGD1 = Color.DodgerBlue;
        public static Color SupplyStatusOpdBKGD2 = Color.LightBlue;
        //Open
        public static Color SupplyStatusOpnBKGD1 = Color.Green;
        public static Color SupplyStatusOpnBKGD2 = Color.LightGreen;
        //Close
        public static Color SupplyStatusClsBKGD1 = Color.LightSlateGray;
        public static Color SupplyStatusClsBKGD2 = Color.LightGray;
    }
}
