using CustomControls;
using HKSupply.General;
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
        ResourceManager resManager = new ResourceManager("HKSupply.Resources.HKSupplyRes", typeof(Login).Assembly);
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
            InitializeMainMenu();
        }

        public void ChildClick(object sender, System.EventArgs e)
        {
            //MessageBox.Show(string.Concat("You have Clicked '", sender.ToString(), "' Menu"), "Menu Items Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                bool found = false;

                CustomToolStripMenuItem menuItem = (CustomToolStripMenuItem)sender;

                Assembly frmAssembly = Assembly.LoadFile(Application.ExecutablePath);
                foreach (Type type in frmAssembly.GetTypes())
                {
                    if (type.BaseType == typeof(Form))
                    {
                        if (type.Name == menuItem.FormName)
                        {
                            found = true;

                            Form frmShow = (Form)frmAssembly.CreateInstance(type.ToString());
                            frmShow.MdiParent = this;
                            frmShow.WindowState = FormWindowState.Maximized;
                            frmShow.Show();
                        }
                    }
                }

                if (found == false)
                {
                    MessageBox.Show(resManager.GetString("FormNotFound"), "ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private Methods

        private void InitializeMainMenu()
        {
           
            try
            {
                //Get role categories
                IEnumerable<string> categoriesList = GlobalSetting.FunctionalityRoleCont.GetFunctionalitiesCategoriesRole(GlobalSetting.LoggedUser.RoleId);
                //Get role menu list
                IEnumerable<FunctionalityRole> menuList = GlobalSetting.FunctionalityRoleCont.GetFunctionalitiesRole(GlobalSetting.LoggedUser.RoleId);

                foreach (string category in categoriesList)
                {
                    var MnuStripItem = new ToolStripMenuItem(category);
                    msMainMenu.Items.Add(MnuStripItem);

                    var categoryMenu = menuList.Where(m => m.Functionality.Category.Equals(category));

                    foreach (FunctionalityRole submenu in categoryMenu)
                    {
                        //ToolStripMenuItem SSMenu = new ToolStripMenuItem(submenu.Functionality.FunctionalityName, null, ChildClick);
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

        #endregion

       


    }
}
