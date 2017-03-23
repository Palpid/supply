using CustomControls;
using HKSupply.Styles;
using HKSupply.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms
{
    public partial class DynamicFilters : Form
    {
        #region Constant
        private const string CMB_FIELD_NAME = "cmbField_";
        private const string CMB_CONDITION_NAME = "cmbCondition_";

        private const string TXT_FILTER_NAME = "txtFilter_";
        private const string CHK_FILTER_NAME = "chkFilter_";
        private const string DATETIMEPICKER_FILTER_NAME = "dateTimePickerFilter_";

        private const string CMB_UNION_NAME = "cmbUnion_";
        #endregion

        #region Private Members
        bool _isRowCreating;

        private Dictionary<string, string> _objectPropertiesDic = new Dictionary<string, string>();
        private Dictionary<string, string> _objectPropertiesTypesDic = new Dictionary<string, string>();

        private Dictionary<string, string> _stringConditionDic = new Dictionary<string, string>() 
        { 
            { "=", "=" },
            { "!=", "!=" },
            { "Contains", "Contains" },
        };

        private Dictionary<string, string> _numberConditionDic = new Dictionary<string, string>() 
        { 
            { "=", "=" },
            { "!=", "!=" },
            { ">", ">" },
            { "<", "<" },
        };

        private Dictionary<string, string> _unionDic = new Dictionary<string, string>() 
        { 
            { "", "" },
            { "OR", "OR" },
            { "AND", "AND" },
        };

        private List<ModelLinqFiltering> _filterList = new List<ModelLinqFiltering>();
        private List<ModelLinqFiltering> _prefilters;

        #endregion

        #region Public Properties
        public List<ModelLinqFiltering> FilterList 
        {
            get { return _filterList; } 
        }
        #endregion

        #region Constructor
        public DynamicFilters()
        {
            InitializeComponent();
            this.Shown += DynamicFilters_Shown;
        }

        #endregion

        #region Form events

        void DynamicFilters_Shown(object sender, EventArgs e)
        {
            try
            {
                if (_prefilters != null)
                    LoadPreFilter(_prefilters);
                else
                {
                    //hay que situarlo en el shown cuando ya esté pintado el formulario, si no no funciona
                    ComboBox cmb = Controls.Find(CMB_FIELD_NAME + "1", true).FirstOrDefault() as ComboBox;
                    if (cmb != null && cmb.Items.Count > 0)
                        cmb.SelectedIndex = -1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DynamicFilters_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeFormStyles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        void cmbUnion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                int number = 0;
                Int32.TryParse(cmb.Name.Replace(CMB_UNION_NAME, ""), out number);
                //Agregamos una nueva fila sí ha seleccionado un valor en el combo de "union" y es la última fila
                if (cmb.SelectedIndex > 0 && (number + 1 == tlpFilters.RowCount))
                    AddRowFilter();
                else if (cmb.SelectedIndex == 0)
                    RemoveRowsFilter(number);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void cmbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                if (cmb.SelectedIndex > -1)
                {
                    string number = cmb.Name.Replace(CMB_FIELD_NAME, "");
                    var type = _objectPropertiesTypesDic[cmb.SelectedValue.ToString()];

                    SetCmbConditionSource(number, type);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                GetFilters();
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Pintar las celdas del table layout panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tlpFilters_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
            {
                using (SolidBrush brush = new SolidBrush(AppStyles.EtniaRed))
                    e.Graphics.FillRectangle(brush, e.CellBounds);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Iniciar los datos necesarios para montar la pantalla antes del show.
        /// </summary>
        /// <param name="objectToFilter">Objeto sobre el que se hara el filtro</param>
        /// <param name="filters">Si queremos cargar unos filtros en la pantalla al abrirla</param>
        /// <remarks>Se tiene que llamar desde el formulario que instancia éste</remarks>
        public void InitData(object objectToFilter, List<ModelLinqFiltering> filters)
        {
            try
            {
                Type type = default(Type);
                Type propertyType = default(Type);
                PropertyInfo[] propertyInfo = null;

                type = objectToFilter.GetType();

                propertyInfo = type.GetProperties(BindingFlags.GetProperty |
                                                  BindingFlags.Public |
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.Instance);
                // Recorremos todas las propiedades 
                for (int propertyInfoIndex = 0; propertyInfoIndex <= propertyInfo.Length - 1; propertyInfoIndex++)
                {
                    propertyType = propertyInfo[propertyInfoIndex].PropertyType;
                    //Obtenemos el tipo en el caso de las nullable
                    if (propertyType.IsGenericType &&
                        propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        propertyType = propertyType.GetGenericArguments()[0];
                    }
                    //Guardamos en un diccionario los nombres de las propiedades para montar los combos 
                    //y en otro la propiedad y el tipo
                    _objectPropertiesDic.Add(propertyInfo[propertyInfoIndex].Name, propertyInfo[propertyInfoIndex].Name);
                    _objectPropertiesTypesDic.Add(propertyInfo[propertyInfoIndex].Name, propertyType.Name);
                }

                if (filters == null)
                {
                    //Agregamos la primera fila en blanco si no han indicado algún prefiltro
                    AddRowFilter();
                }
                else
                {
                    //cargamos los filtros que se han recibido
                    _prefilters = filters;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Inicializar algunos estilos del formulario
        /// </summary>
        private void InitializeFormStyles()
        {
            try
            {
                //TableLayoutPanel
                tlpFilters.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tlpFilters.CellPaint += tlpFilters_CellPaint;
                //TableLayoutPanel labels header
                lblColumn1Header.BackColor = AppStyles.EtniaRed;
                lblColumn2Header.BackColor = AppStyles.EtniaRed;
                lblColumn3Header.BackColor = AppStyles.EtniaRed;
                lblColumn4Header.BackColor = AppStyles.EtniaRed;
                lblColumn1Header.ForeColor = Color.White;
                lblColumn2Header.ForeColor = Color.White;
                lblColumn3Header.ForeColor = Color.White;
                lblColumn4Header.ForeColor = Color.White;

                //Filter button
                btnFilter.BackColor = AppStyles.EtniaRed;
                btnFilter.FlatStyle = FlatStyle.Flat;
                btnFilter.FlatAppearance.BorderColor = AppStyles.EtniaRed;
                btnFilter.FlatAppearance.BorderSize = 1;
                btnFilter.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agregar una fila al TableLayoutPanel
        /// </summary>
        private void AddRowFilter()
        {
            try
            {
                _isRowCreating = true;

                //***** Combo con las propiedades del objeto *****
                ComboBox cmbField = new ComboBox();
                cmbField.DataSource = new BindingSource(_objectPropertiesDic, null);
                cmbField.DisplayMember = "Value";
                cmbField.ValueMember = "Key";
                cmbField.Name = CMB_FIELD_NAME + tlpFilters.RowCount.ToString();
                cmbField.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                cmbField.SelectedIndexChanged += cmbField_SelectedIndexChanged;
                cmbField.DropDownStyle = ComboBoxStyle.DropDownList;

                //***** Combo para las condiciones *****
                ComboBox cmbCondition = new ComboBox();
                cmbCondition.DataSource = new BindingSource(_stringConditionDic, null);
                cmbCondition.DisplayMember = "Value";
                cmbCondition.ValueMember = "Key";
                cmbCondition.Name = CMB_CONDITION_NAME + tlpFilters.RowCount.ToString();
                cmbCondition.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                cmbCondition.DropDownStyle = ComboBoxStyle.DropDownList;

                //***** Panel para los filtros en función del tipo de propiedad (texbox, checkbox, datePicker) *****
                Panel filterPanel = new Panel();
                filterPanel.Dock = DockStyle.Fill;

                TextBox txtFilter = new TextBox();
                txtFilter.Name = TXT_FILTER_NAME + tlpFilters.RowCount.ToString();
                txtFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                txtFilter.Visible = false;

                CheckBox chkFilter = new CheckBox();
                chkFilter.Name = CHK_FILTER_NAME + tlpFilters.RowCount.ToString();
                chkFilter.Visible = false;

                NullableDateTimePicker dateTimePickerFilter = new NullableDateTimePicker();
                dateTimePickerFilter.Name = DATETIMEPICKER_FILTER_NAME + tlpFilters.RowCount.ToString();
                dateTimePickerFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                dateTimePickerFilter.Format = DateTimePickerFormat.Short;
                dateTimePickerFilter.Visible = false;

                filterPanel.Controls.Add(txtFilter);
                filterPanel.Controls.Add(chkFilter);
                filterPanel.Controls.Add(dateTimePickerFilter);

                //***** Combo para el union *****
                ComboBox cmbUnion = new ComboBox();
                cmbUnion.DataSource = new BindingSource(_unionDic, null);
                cmbUnion.DisplayMember = "Value";
                cmbUnion.ValueMember = "Key";
                cmbUnion.Name = CMB_UNION_NAME + tlpFilters.RowCount.ToString();
                cmbUnion.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                cmbUnion.SelectedIndexChanged += cmbUnion_SelectedIndexChanged;
                cmbUnion.DropDownStyle = ComboBoxStyle.DropDownList;


                tlpFilters.RowCount = tlpFilters.RowCount + 1;
                tlpFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                tlpFilters.Controls.Add(cmbField, 0, tlpFilters.RowCount - 1);
                tlpFilters.Controls.Add(cmbCondition, 1, tlpFilters.RowCount - 1);
                tlpFilters.Controls.Add(filterPanel, 2, tlpFilters.RowCount - 1);
                tlpFilters.Controls.Add(cmbUnion, 3, tlpFilters.RowCount - 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _isRowCreating = false;
            }
        }

        /// <summary>
        /// Eliminar filas a partir de un índice dado
        /// </summary>
        /// <param name="index"></param>
        /// <remarks>Hay que empezar a eliminar por la última</remarks>
        private void RemoveRowsFilter(int index)
        {
            try
            {
                if (_isRowCreating) return;

                for (int i = tlpFilters.RowCount - 1; i > index; i--)
                    tlpFilters.RemoveRow(i);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        /// <summary>
        /// Cargar los filtros que se han pasado desde otro formulario
        /// </summary>
        /// <param name="filters"></param>
        private void LoadPreFilter(List<ModelLinqFiltering> filters)
        {
            try
            {
                int i = 1;
                AddRowFilter();
                foreach (ModelLinqFiltering f in filters)
                {
                    ComboBox cmbField = GetCmbRow(CMB_FIELD_NAME + i);
                    ComboBox cmbCondition = GetCmbRow(CMB_CONDITION_NAME + i);
                    TextBox txtFilter = GetTxtRow(TXT_FILTER_NAME + i);
                    CheckBox chkFilter = GetChkRow(CHK_FILTER_NAME + i);
                    NullableDateTimePicker dateTimePickerFilter = GetNullDateRow(DATETIMEPICKER_FILTER_NAME + i);
                    ComboBox cmbUnion = GetCmbRow(CMB_UNION_NAME + i);

                    cmbField.SelectedIndex = -1;
                    cmbField.Text = f.PropertyName;
                    cmbCondition.Text = f.Condition;
                    cmbUnion.Text = f.Union;

                    switch (f.PropertyType)
                    {
                        case "DateTime":
                            dateTimePickerFilter.Value = f.DateFilter;
                            break;
                        case "Boolean":
                            chkFilter.Checked = bool.Parse(f.Filter);
                            break;
                        default:
                            txtFilter.Text = f.Filter;
                            break;
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Asignar el datasource del combo de condición en función del tipo de campo seleccionado
        /// </summary>
        /// <param name="number"></param>
        /// <param name="type"></param>
        private void SetCmbConditionSource(string number, string type)
        {
            try
            {
                ComboBox cmb = Controls.Find(CMB_CONDITION_NAME + number, true).FirstOrDefault() as ComboBox;
                TextBox txtFilter = Controls.Find(TXT_FILTER_NAME + number, true).FirstOrDefault() as TextBox;
                CheckBox chkFilter = Controls.Find(CHK_FILTER_NAME + number, true).FirstOrDefault() as CheckBox;
                NullableDateTimePicker dateTimePickerFilter = Controls.Find(DATETIMEPICKER_FILTER_NAME + number, true).FirstOrDefault() as NullableDateTimePicker;

                txtFilter.Visible = false;
                chkFilter.Visible = false;
                dateTimePickerFilter.Visible = false;

                if (cmb != null)
                {
                    cmb.DataBindings.Clear();

                    switch (type)
                    {
                        case "Decimal":
                        case "Double":
                        case "Single":
                        case "Int32":
                        case "Int16":
                            cmb.Visible = true;
                            cmb.DataSource = new BindingSource(_numberConditionDic, null);
                            cmb.DisplayMember = "Value";
                            cmb.ValueMember = "Key";

                            txtFilter.Visible = true;
                            break;

                        case "String":
                            cmb.Visible = true;
                            cmb.DataSource = new BindingSource(_stringConditionDic, null);
                            cmb.DisplayMember = "Value";
                            cmb.ValueMember = "Key";

                            txtFilter.Visible = true;
                            break;

                        case "DateTime":
                            cmb.Visible = true;
                            cmb.DataSource = new BindingSource(_numberConditionDic, null);
                            cmb.DisplayMember = "Value";
                            cmb.ValueMember = "Key";

                            dateTimePickerFilter.Visible = true;
                            break;

                        case "Boolean":
                            cmb.Visible = false;
                            chkFilter.Visible = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtener los filtros que han indicado en la pantalla
        /// </summary>
        private void GetFilters()
        {
            try
            {
                for (int i = 1; i < tlpFilters.RowCount; i++)
                {
                    ComboBox cmbField = GetCmbRow(CMB_FIELD_NAME + i);
                    ComboBox cmbCondition = GetCmbRow(CMB_CONDITION_NAME + i);
                    TextBox txtFilter = GetTxtRow(TXT_FILTER_NAME + i);
                    CheckBox chkFilter = GetChkRow(CHK_FILTER_NAME + i);
                    NullableDateTimePicker dateTimePickerFilter = GetNullDateRow(DATETIMEPICKER_FILTER_NAME + i);
                    ComboBox cmbUnion = GetCmbRow(CMB_UNION_NAME + i);

                    if (cmbField.SelectedValue == null)
                        throw new Exception(string.Format("Select column value at row {0}", i.ToString()));

                    string type = _objectPropertiesTypesDic[cmbField.SelectedValue.ToString()];

                    string propertyName = cmbField.SelectedValue.ToString();
                    string condition = cmbCondition.SelectedValue.ToString();
                    string union = cmbUnion.SelectedValue.ToString();

                    string filter = "";
                    DateTime? dateFilter = null;
                    switch (type)
                    {
                        case "Decimal":
                        case "Double":
                        case "Single":
                        case "Int32":
                        case "Int16":
                        case "String":
                            filter = txtFilter.Text;
                            break;
                        case "DateTime":
                            dateFilter = (DateTime)dateTimePickerFilter.Value;
                            break;
                        case "Boolean":
                            filter = chkFilter.Checked.ToString();
                            break;
                    }

                    ModelLinqFiltering modelFiltering = new ModelLinqFiltering 
                    {
                        PropertyName = propertyName,
                        PropertyType = type, 
                        Condition = condition,
                        Filter = filter,
                        DateFilter = dateFilter,
                        Union = union,
                    };

                    if (modelFiltering.IsValidData())
                        _filterList.Add(modelFiltering);
                    else
                        throw new Exception(string.Format("Filter format error at row {0}", i.ToString()));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ComboBox GetCmbRow(string name)
        {
            try
            {
                return Controls.Find(name, true).FirstOrDefault() as ComboBox;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TextBox GetTxtRow(string name)
        {
            try
            {
                return Controls.Find(name, true).FirstOrDefault() as TextBox;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CheckBox GetChkRow(string name)
        {
            try
            {
                return Controls.Find(name, true).FirstOrDefault() as CheckBox;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private NullableDateTimePicker GetNullDateRow(string name)
        {
            try
            {
                return Controls.Find(name, true).FirstOrDefault() as NullableDateTimePicker;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    /// <summary>
    /// Clase que representa una filtro dynámico de linq sobre una propiedad de un modelo.
    /// </summary>
    public class ModelLinqFiltering
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string Condition { get; set; }
        public string Filter { get; set; }
        public DateTime? DateFilter { get; set; }
        public string Union { get; set; }

        /// <summary>
        /// Obtener el string del where de linq
        /// </summary>
        /// <returns></returns>
        public string GetLinqFilterString()
        {
            try
            {
                string filter = string.Empty;


                if (IsValidData() == false)
                    throw new Exception("Data Filter Error");

                filter = string.Format("{0}{1} {2} ", PropertyName, GetCondition(), Union);

                return filter;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Validar los datos que ha indicado el usuario para filtrar.
        /// </summary>
        /// <returns></returns>
        public bool IsValidData()
        {
            //Validamos que estén todos los datos
            if (string.IsNullOrEmpty(PropertyName) == false &&
                string.IsNullOrEmpty(PropertyType) == false &&
                string.IsNullOrEmpty(Condition) == false &&
                (string.IsNullOrEmpty(Filter) == false ||
                    DateFilter != null))
            {
                //Validamos que el filtro/tipo sea correcto
                switch (PropertyType)
                {
                    case "String":
                        //Tratamos algunos caracteres que pueden dar problemas
                        Filter = Filter.Replace("'", "");
                        Filter = Filter.Replace("\"", "");
                        Filter = Filter.Replace("\\", "");
                        return true;

                    case "DateTime":
                        //los datetime tienen su propia propiedad
                        break;

                    case "Decimal":
                        Decimal dectmp;
                        if (Decimal.TryParse(Filter, out dectmp) == false)
                            return false;
                        else
                        {
                            Filter = dectmp.ToString();
                            return true;
                        }

                    case "Double":
                        Double doubletmp;
                        if (Double.TryParse(Filter, out doubletmp) == false)
                            return false;
                        else
                        {
                            Filter = doubletmp.ToString();
                            return true;
                        }

                    case "Single":
                        Single singletmp;
                        if (Single.TryParse(Filter, out singletmp) == false)
                            return false;
                        else
                        {
                            Filter = singletmp.ToString();
                            return true;
                        }

                    case "Int32":
                        Int32 int32tmp;
                        if (Int32.TryParse(Filter, out int32tmp) == false)
                            return false;
                        else 
                        {
                            Filter = int32tmp.ToString();
                            return true;
                        }

                    case "Int16":
                        Int16 int16tmp;
                        if (Int16.TryParse(Filter, out int16tmp) == false)
                            return false;
                        else
                        {
                            Filter = int16tmp.ToString();
                            return true;
                        }

                    case "Boolean":
                        bool booltmp;
                        return bool.TryParse(Filter, out booltmp);
                }
                return true;
            }
            else
            {
                return false;
            }
             
        }

        /// <summary>
        /// Obtener la condición en el formato correcto según su tipo
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Para las cadenas, es case sensitive, lo pasamos a ´minúsculas, 
        /// tanto el campo a filtrar como el texto introducido por el usuario
        /// </remarks>
        private string GetCondition()
        {
            try
            {
                string conditionString = string.Empty;

                switch (Condition)
                {
                    case "Contains":
                        conditionString = string.Format(".ToLower().Contains(\"{0}\")", Filter.ToLower());
                        break;
                    default:
                        switch(PropertyType)
                        {
                            case "String":
                                conditionString = string.Format(".ToLower() " + Condition + " \"{0}\"", Filter.ToLower());
                                break;
                            case "DateTime":
                                conditionString = string.Format(" " + Condition + " DateTime({0}, {1}, {2})", 
                                    DateFilter.Value.Year.ToString(),
                                    DateFilter.Value.Month.ToString(),
                                    DateFilter.Value.Day.ToString());
                                break;

                            case "Decimal":
                            case "Double":
                            case "Single":
                            case "Int32":
                            case "Int16":
                                conditionString = string.Format(" " + Condition + " {0}", Filter.ToLower());
                                break;
                            case "Boolean":
                                conditionString = string.Format(" =  {0}", Filter.ToLower());
                                break;
                        }
                        break;
                }

                return conditionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
