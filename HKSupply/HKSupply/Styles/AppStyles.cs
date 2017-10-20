using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Skins;

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

        #region Public Methods

        #region DevExpress, modify skin

        public static void SetEtniaSkinStyles()
        {
            try
            {
                //Cambiar el FormCaption y el TabHeaderBackground (el color de fondo de la parte superior del ribbon) a uno rojo "etnia". 
                //Sustituyo las dos imágenes originales del skin que usamos de office 2016
                SkinElement element = SkinManager.GetSkinElement(SkinProductId.Ribbon, DevExpress.LookAndFeel.UserLookAndFeel.Default, "FormCaption");
                element.SetActualImage(Properties.Resources.formcaption_red, true);
                element = SkinManager.GetSkinElement(SkinProductId.Ribbon, DevExpress.LookAndFeel.UserLookAndFeel.Default, "TabHeaderBackground");
                element.SetActualImage(Properties.Resources.tabheaderbackground_red, true);

                //Ribbon
                SetRibbonStyles();

                //Test
                //SkinElement elementTest = SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, "HighlightedItem");
                //elementTest.Image.ImageCount = 0;
                //elementTest.Color.BackColor = EtniaRed;
                //elementTest.Color.BackColor2 = EtniaRed;

                //Skin skin = CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);


                //******************************** END ********************************/
                //Forzar la aplicación de los cambios que hemos hecho
                DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Private Methods

        #region DevExpress, modify skin

        private static void SetRibbonStyles()
        {
            try
            {
                //Backgroud de las tabs
                SkinElement elementTabPanel = SkinManager.GetSkinElement(SkinProductId.Ribbon, DevExpress.LookAndFeel.UserLookAndFeel.Default, "TabPanel");
                elementTabPanel.Image.ImageCount = 0;
                elementTabPanel.Color.BackColor = Color.FromArgb(255, 255, 255);
                elementTabPanel.Color.BackColor2 = Color.FromArgb(255, 255, 255);

                //Backgroud del título de las tabs
                SkinElement elementTabHeaderPage = SkinManager.GetSkinElement(SkinProductId.Ribbon, DevExpress.LookAndFeel.UserLookAndFeel.Default, "TabHeaderPage");
                elementTabHeaderPage.Image.ImageCount = 0;
                elementTabHeaderPage.Color.BackColor = Color.FromArgb(255, 255, 255);
                elementTabHeaderPage.Color.BackColor2 = Color.FromArgb(255, 255, 255);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #endregion
    }
}
