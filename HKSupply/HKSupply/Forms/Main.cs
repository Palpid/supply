﻿using CustomControls;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Services.Implementations;
using HKSupply.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms
{
    public partial class Main : Form
    {
        #region Private members
        System.Drawing.Icon _gIcon = new Icon(@"Resources\Images\etnia_icon.ico");
        #endregion

        #region Constructor
        public Main()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Events

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetBevel(false);//quitar el border 3d del mdi container

                this.Icon = _gIcon;
                SetBackgroundColor();
                SetMenuStyle();
                InitializeMainMenu();

                msMainMenu.ItemAdded += msMainMenu_ItemAdded;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Para eliminar el icono del hijo dentro del menu strip cuando está maximizado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void msMainMenu_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e != null && e.Item != null && e.Item.GetType().Name == "SystemMenuItem")
            {
                this.msMainMenu.Items.RemoveAt(0);
            }
        }

        public void ChildClick(object sender, System.EventArgs e)
        {
            //MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                bool found = false;

                CustomToolStripMenuItem menuItem = (CustomToolStripMenuItem)sender;

                //if is already open, activate it
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name == menuItem.FormName)
                    {
                        found = true;
                        form.Activate();
                        //form.Dock = DockStyle.Fill;
                        //form.WindowState = FormWindowState.Maximized;

                    }
                    //else
                    //{
                    //    if (form.Name != "Main")
                    //        form.WindowState = FormWindowState.Normal;
                    //}
                }

                //if not, open it
                if (found == false)
                {
                    Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);
                    foreach (Type type in frmAssembly.GetTypes())
                    {
                        if (type.BaseType == typeof(Form) || type.BaseType == typeof(RibbonFormBase))
                        {
                            if (type.Name == menuItem.FormName)
                            {
                                found = true;

                                Form frmShow = (Form)frmAssembly.CreateInstance(type.ToString());
                                frmShow.MdiParent = this;
                                frmShow.ShowIcon = false;
                                //frmShow.Dock = DockStyle.Fill;
                                //frmShow.ControlBox = false;
                                frmShow.Show();
                                frmShow.WindowState = FormWindowState.Maximized;
                            }
                        }
                    }
                }

                if (found == false)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("FormNotFound"), "ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        private void InitializeMainMenu()
        {
           
            try
            {
                //Get role categories
                IEnumerable<string> categoriesList = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesCategoriesRole(GlobalSetting.LoggedUser.RoleId);
                //Get role menu list
                IEnumerable<FunctionalityRole> menuList = GlobalSetting.FunctionalityRoleService.GetFunctionalitiesRole(GlobalSetting.LoggedUser.RoleId);

                foreach (string category in categoriesList)
                {
                    var MnuStripItem = new ToolStripMenuItem(category);
                    msMainMenu.Items.Add(MnuStripItem);

                    var categoryMenu = menuList.Where(m => m.Functionality.Category.Equals(category));

                    foreach (FunctionalityRole submenu in categoryMenu)
                    {
                        CustomToolStripMenuItem SSMenu = new CustomToolStripMenuItem(submenu.Functionality.FunctionalityName, null, ChildClick, null);
                        SSMenu.FormName = submenu.Functionality.FormName;  //"Form1";
                        MnuStripItem.DropDownItems.Add(SSMenu);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Cambiar el estilo dle menú
        /// </summary>
        private void SetMenuStyle()
        {
            try
            {
                msMainMenu.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
                msMainMenu.Font = new Font("Brandon", 9);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cambiar el color de fondo
        /// </summary>
        /// <remarks>Al ser un mdi parent parece ser que se tiene que hacer así, no se puede directamente</remarks>
        private void SetBackgroundColor()
        {
            try
            {
                //Pintar de blanco el fondo del mdi parent
                MdiClient chld;
                foreach (Control ctrl in this.Controls)
                {
                    try
                    {
                        chld = (MdiClient)ctrl;
                        chld.BackColor = Color.White;
                    }
                    catch (InvalidCastException)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

    /// <summary>
    /// tabla de colores para el menú
    /// </summary>
    internal class CustomProfessionalColors : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin { get { return Color.White; } }
        public override Color ToolStripGradientMiddle { get { return Color.White; } }
        public override Color ToolStripGradientEnd { get { return Color.White; } }
        public override Color MenuStripGradientBegin { get { return Color.White; } }
        public override Color MenuStripGradientEnd { get { return Color.White; } }
    }
}
