using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

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

        #region Private Members
        //private static Color _colorHighlight = Color.FromArgb(247, 190, 186);
        private static Color _colorHighlight = Color.FromArgb(246, 229, 229);
        private static Color _colorControl = Color.White;
        private static Color _colorGridOddRow = Color.FromArgb(244, 244, 244);
        private static Color _colorGridEvenRow = Color.White;
        #endregion

        #region DevExpress, modify skin

        public static void SetEtniaSkinStyles()
        {
            try
            {
                //Cambiar el FormCaption y el TabHeaderBackground (el color de fondo de la parte superior del ribbon) a uno rojo "etnia". 
                //Sustituyo las dos imágenes originales del skin que usamos de office 2016
                SkinElement element = SkinManager.GetSkinElement(SkinProductId.Ribbon, UserLookAndFeel.Default, "FormCaption");
                element.SetActualImage(Properties.Resources.formcaption_red, true);
                element = SkinManager.GetSkinElement(SkinProductId.Ribbon, UserLookAndFeel.Default, "TabHeaderBackground");
                element.SetActualImage(Properties.Resources.tabheaderbackground_red, true);

                //Ribbon
                SetRibbonStyles();

                //Common
                SetCommonStyles();

                //Tab
                SetTabStyles();

                //Grid
                SetGridStyles();

                //Test
                //SkinElement elementButton = SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, "Button");

                //******************************** END ********************************/
                //Forzar la aplicación de los cambios que hemos hecho
                LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
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

        private static void SetCommonStyles()
        {
            try
            {
                Skin skinCommon = SkinManager.Default.GetSkin(SkinProductId.Common, UserLookAndFeel.Default);
                skinCommon.Colors["Highlight"] = _colorHighlight; //row grid seleccionada y con el foco
                skinCommon.Colors["HideSelection"] = _colorHighlight;  //row grid seleccionada, pero sin el foco en el grid
                skinCommon.Colors["HighlightText"] = EtniaRed;  //color del texto de una row seleccionada y con y sin el foco

                skinCommon.Colors["Control"] = _colorControl;
            }
            catch
            {
                throw;
            }
        }

        private static void SetTabStyles()
        {
            try
            {
                SkinElement elementTabHeader = SkinManager.GetSkinElement(SkinProductId.Tab, UserLookAndFeel.Default, "TabHeader");
                //Las imágenes tienen el border, mejor no quitarlas
                //elementTabHeader.Image.ImageCount = 0;
                //elementTabHeader.Color.BackColor = Color.White;
                //elementTabHeader.Color.BackColor2 = Color.White;
            }
            catch
            {
                throw;
            }
        }

        private static void SetGridStyles()
        {
            try
            {
                //para altenar el color de las líneas del grid
                //Para que funcione hay que poner a true las siguientes propiedades del grid view
                //gridView.OptionsView.EnableAppearanceOddRow = true;
                //gridView.OptionsView.EnableAppearanceEvenRow = true;
                SkinElement elementGridOddRow = SkinManager.GetSkinElement(SkinProductId.Grid, UserLookAndFeel.Default, "GridOddRow");
                SkinElement elementGridEvenRow = SkinManager.GetSkinElement(SkinProductId.Grid, UserLookAndFeel.Default, "GridEvenRow");
                elementGridOddRow.Color.BackColor = _colorGridOddRow;
                elementGridOddRow.Color.BackColor2 = _colorGridOddRow;
                elementGridEvenRow.Color.BackColor = _colorGridEvenRow;
                elementGridEvenRow.Color.BackColor2 = _colorGridEvenRow;
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
