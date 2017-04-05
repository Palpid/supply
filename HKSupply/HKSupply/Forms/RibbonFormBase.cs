using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.ComponentModel.DataAnnotations;

namespace HKSupply.Forms
{
    public partial class RibbonFormBase : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        #region Enums
        public enum ActionsStates
        {
            OnlyRead,
            OnlyEdit,
            OnlyEditNew,
            Edit,
            New
        }
        #endregion

        #region Private Members

        private ActionsStates _currentState;

        #endregion

        #region Public Properties

        public bool Read { get; set; }
        public bool New { get; set; }
        public bool Modify { get; set; }

        public ActionsStates CurrentState 
        { 
            get { return _currentState; }
            set 
            {
                _currentState = value;
                ConfigureByState(_currentState);
            }
        }

        #endregion

        #region Constructo
        public RibbonFormBase()
        {
            InitializeComponent();

            ConfigureRibbonEvents();
        }
        #endregion

        #region Public Functions
        public void RestoreInitState()
        {
            ConfigureActions();
        }
        #endregion

        #region Private Functions

        private void ConfigureActions()
        {
            if (Read == true && New == false && Modify == false)
                ConfigureByState(ActionsStates.OnlyRead);
            else if (Read == true && New == true && Modify == true)
                ConfigureByState(ActionsStates.OnlyEditNew);
            else if (Read == true && New == false && Modify == true)
                ConfigureByState(ActionsStates.OnlyEdit);
        }

        private void ConfigureByState(ActionsStates state)
        {
            _currentState = state;
            switch (state)
            {
                case ActionsStates.OnlyRead:
                    ribbonPageGroup1.Visible = false;
                    break;
                case ActionsStates.OnlyEdit:
                    ribbonPageGroup1.Visible = true;
                    bbiEdit.Visibility = BarItemVisibility.Always;
                    bbiNew.Visibility = BarItemVisibility.Never;
                    bbiSave.Visibility = BarItemVisibility.Never;
                    bbiCancel.Visibility = BarItemVisibility.Never;
                    break;
                case ActionsStates.OnlyEditNew:
                    ribbonPageGroup1.Visible = true;
                    bbiEdit.Visibility = BarItemVisibility.Always;
                    bbiNew.Visibility = BarItemVisibility.Always;
                    bbiSave.Visibility = BarItemVisibility.Never;
                    bbiCancel.Visibility = BarItemVisibility.Never;
                    break;
                case ActionsStates.Edit:
                case ActionsStates.New:
                    ribbonPageGroup1.Visible = true;
                    bbiEdit.Visibility = BarItemVisibility.Never;
                    bbiNew.Visibility = BarItemVisibility.Never;
                    bbiSave.Visibility = BarItemVisibility.Always;
                    bbiCancel.Visibility = BarItemVisibility.Always;
                    break;
            }
        }

        private void ConfigureRibbonEvents()
        {
            try
            {
                bbiEdit.ItemClick += bbiEdit_ItemClick;
                bbiNew.ItemClick += bbiNew_ItemClick;
                bbiCancel.ItemClick += bbiCancel_ItemClick;
                bbiSave.ItemClick += bbiSave_ItemClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events
        public virtual void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //Lanzamos el validate para finalizar cualquier accion de edición del formulario, ya que este control no tiene focus nunca y no lo lanza por si mismo
                Validate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CurrentState = ActionsStates.New;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                CurrentState = ActionsStates.Edit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}