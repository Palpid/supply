using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using System.Globalization;

namespace HKSupply.Reports
{
    public class B1Report
    {
        private string m_ReportFileName;
        private string m_SelectionFormula;
        private string m_Title;
        private bool m_Maximized;
        private string m_PrinterName;
        private int m_Copies;
        private string m_ExportFileName;
        private string m_serverdb;
        private string m_databasedb;
        private string m_usuariordb;
        private bool m_integratedSecuritydb;
        private string m_usuariopassword;
        private Dictionary<string, string> m_Parametros = new Dictionary<string, string>();
        private static readonly ILog _Debug = LogManager.GetLogger(typeof(B1Report));


        public Dictionary<string, string> Parametros
        {
            get { return m_Parametros; }
            set { m_Parametros = value; }
        }

        public string ReportFileName
        {
            get { return m_ReportFileName; }
            set { m_ReportFileName = value; }
        }

        public string PrinterName
        {
            get { return m_PrinterName; }
            set { m_PrinterName = value; }
        }

        public string ExportFileName
        {
            get { return m_ExportFileName; }
            set { m_ExportFileName = value; }
        }

        public string SelectionFormula
        {
            get { return m_SelectionFormula; }
            set { m_SelectionFormula = value; }
        }

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        public int Copies
        {
            get { return m_Copies; }
            set { m_Copies = value; }
        }

        public bool Maximized
        {
            get { return m_Maximized; }
            set { m_Maximized = value; }
        }
        public bool ExisteFichero(string Fichero)
        {
            return System.IO.File.Exists(Fichero);
        }
        //----------------------------------------------------------
        // PrintReport
        //----------------------------------------------------------
        public void PrintReport()
        {
            ReportDocument oDoc = null;
            try
            {
                if (ExisteFichero(m_ReportFileName))
                {
                    oDoc = new ReportDocument();
                    oDoc.Load(m_ReportFileName);
                    oDoc.RecordSelectionFormula = m_SelectionFormula;

                    //oDoc.PrintOptions.PrinterName = m_PrinterName;
                    SetConnectionInfo(oDoc);
                    oDoc.Refresh();
                    foreach (string Key in Parametros.Keys)
                    {
                        SetReportParameters(oDoc, Key, Parametros[Key]);
                    }


                    oDoc.PrintToPrinter(m_Copies, false, 1, 100);
                }
                else
                {
                    _Debug.Error("No existe el fichero de informe pongase en contacto con el proveedor");
                    //Globals.Application.SetStatusBarMessage("No existe el fichero de informe pongase en contacto con el proveedor", SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                }
            }
            catch (Exception ex)
            {
                _Debug.Error("PrintReport:" + ex.Message, ex);
                //Globals.Application.StatusBar.SetText("PrintReport:" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                if (oDoc != null)
                {
                    oDoc.Close();
                    oDoc.Dispose();
                    oDoc = null;
                }
            }
        }



        public void PrintLabel()
        {
            ReportDocument oDoc = null;
            try
            {
                oDoc = new ReportDocument();
                oDoc.Load(m_ReportFileName);
                oDoc.RecordSelectionFormula = m_SelectionFormula;


                oDoc.PrintOptions.PrinterName = m_PrinterName;
                //oDoc.PrintOptions.CustomPaperSource  =  
                oDoc.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                SetConnectionInfo(oDoc);
                oDoc.Refresh();
                foreach (string Key in Parametros.Keys)
                {
                    SetReportParameters(oDoc, Key, Parametros[Key]);
                }
                oDoc.PrintToPrinter(m_Copies, false, 1, 100);
            }
            catch (Exception ex)
            {
                // throw new Exception("PrintReport: " + ex.Message);
                _Debug.Error("PrintReport:" + ex.Message, ex);
                //Globals.Application.StatusBar.SetText("PrintReport:" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                if (oDoc != null)
                {
                    oDoc.Close();
                    oDoc.Dispose();
                    oDoc = null;
                }
            }
        }

        //----------------------------------------------------------
        // PreviewReport
        //----------------------------------------------------------
        public void PreviewReport()
        {
            frmVisor oVisor = null;
            ReportDocument oDoc = null;


            try
            {
                if (ExisteFichero(m_ReportFileName))
                {
                    oDoc = new ReportDocument();
                    oDoc.Load(m_ReportFileName);
                    oDoc.RecordSelectionFormula = m_SelectionFormula;


                    SetConnectionInfo(oDoc);
                    oDoc.Refresh();

                    foreach (string Key in Parametros.Keys)
                    {
                        SetReportParameters(oDoc, Key, Parametros[Key]);
                    }

                    oVisor = new frmVisor();
                    oVisor.Text = m_Title;
                    oVisor.crystalReportViewer1.ReportSource = oDoc;
                    oVisor.WindowState = m_Maximized ? System.Windows.Forms.FormWindowState.Maximized : System.Windows.Forms.FormWindowState.Normal;
                    oVisor.Visible = false;
                    oVisor.ShowInTaskbar = true;
                    oVisor.TopMost = true;
                    oVisor.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    oVisor.ShowDialog();
                    oVisor.Close();
                }
                else
                {
                    _Debug.Error("No existe el fichero de informe pongase en contacto con el proveedor");
                    //Globals.Application.SetStatusBarMessage("No existe el fichero de informe pongase en contacto con el proveedor", SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                }

                //ShowCrystal(oDoc);
            }
            catch (Exception ex)
            {
                _Debug.Error("PreviewReport:" + ex.Message, ex);
                //Globals.Application.SetStatusBarMessage("PreviewReport:" + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
            }
            finally
            {
                if (oDoc != null)
                {
                    oDoc.Close();
                    oDoc.Dispose();
                    oDoc = null;
                }

                if (oVisor != null)
                {
                    oVisor.Dispose();
                    oVisor = null;
                }
            }
        }


        //----------------------------------------------------------
        // ExportReport
        //----------------------------------------------------------
        public void ExportReport()
        {
            ReportDocument oDoc = null;

            try
            {
                oDoc = new ReportDocument();
                oDoc.Load(m_ReportFileName);
                oDoc.RecordSelectionFormula = m_SelectionFormula;
                SetConnectionInfo(oDoc);
                oDoc.Refresh();

                foreach (string Key in Parametros.Keys)
                {
                    SetReportParameters(oDoc, Key, Parametros[Key]);
                }

                if (System.IO.File.Exists(m_ExportFileName))
                    System.IO.File.Delete(m_ExportFileName);

                oDoc.ExportToDisk(ExportFormatType.PortableDocFormat, m_ExportFileName);
            }
            catch (Exception ex)
            {
                throw new Exception("ExportReport: " + ex.Message);
            }
            finally
            {
                if (oDoc != null)
                {
                    oDoc.Close();
                    oDoc.Dispose();
                    oDoc = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RecuperarDatosConexion()
        {
            try
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[General.GlobalSetting.ConnStringName].ConnectionString;
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);

                m_serverdb = builder.DataSource;
                m_databasedb = builder.InitialCatalog;
                m_usuariordb = builder.UserID;
                m_usuariopassword = builder.Password;
                m_integratedSecuritydb = builder.IntegratedSecurity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void SetConnectionInfo(ReportDocument oDoc)
        {
            try
            {
                RecuperarDatosConexion();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = m_serverdb;
                connectionInfo.DatabaseName = m_databasedb;

                connectionInfo.UserID = m_usuariordb;
                connectionInfo.Password = m_usuariopassword;

                connectionInfo.IntegratedSecurity = m_integratedSecuritydb;

                #region por borrar
                ////connectionInfo.Password = "B1Admin";
                // //connectionInfo.IntegratedSecurity = true;

                //connectionInfo.ServerName = "SRVBI";
                //connectionInfo.DatabaseName = "hk_supply";
                //connectionInfo.UserID = "hksupply";
                //connectionInfo.Password = "hksuppy@2017";
                #endregion

                SetDBLogonForReport(connectionInfo, oDoc);
                SetDBLogonForSubreports(connectionInfo, oDoc);
                connectionInfo = null;
            }
            catch (Exception ex)
            {
                throw new Exception("SetConnectionInfo: " + ex.Message);
            }
        }


        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportdocument)
        {
            try
            {
                Tables tables = reportdocument.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogonInfo);

                }
                tables = null;
            }
            catch (Exception ex)
            {
                throw new Exception("SetDBLogonForReport: " + ex.Message);
            }
        }

        private void SetDBLogonForSubreports(ConnectionInfo connectionInfo, ReportDocument reportdocument)
        {
            try
            {
                Sections sections = reportdocument.ReportDefinition.Sections;
                foreach (Section section in sections)
                {
                    ReportObjects reportObjects = section.ReportObjects;
                    foreach (ReportObject reportObject in reportObjects)
                    {
                        if (reportObject.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject subreportObject = (SubreportObject)reportObject;
                            ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                            SetDBLogonForReport(connectionInfo, subReportDocument);

                        }
                    }

                }
                sections = null;
            }
            catch (Exception ex)
            {
                throw new Exception("SetDBLogonForSubreports: " + ex.Message);
            }

        }

        protected void SetReportParameters(ReportDocument oDoc, string name, string value)
        {
            ParameterValues parameterValue = new ParameterValues();
            ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
            CultureInfo Iformat = CultureInfo.CurrentCulture;

            for (int i = 0; i < oDoc.DataDefinition.ParameterFields.Count; i++)
            {
                if (oDoc.DataDefinition.ParameterFields[i].Name == name)
                {


                    switch (oDoc.DataDefinition.ParameterFields[i].ValueType)
                    {

                        case FieldValueType.NumberField:

                            parameterDiscreteValue.Value = Convert.ToInt32(value);
                            parameterValue.Add(parameterDiscreteValue);
                            oDoc.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.CurrencyField:

                            parameterDiscreteValue.Value = Convert.ToDouble(value);
                            parameterValue.Add(parameterDiscreteValue);
                            oDoc.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.DateField:

                            parameterDiscreteValue.Value = Convert.ToDateTime(value, Iformat);
                            parameterValue.Add(parameterDiscreteValue);
                            oDoc.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.DateTimeField:

                            parameterDiscreteValue.Value = Convert.ToDateTime(value, Iformat);
                            parameterValue.Add(parameterDiscreteValue);
                            oDoc.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.BooleanField:

                            parameterDiscreteValue.Value = Convert.ToBoolean(value);
                            parameterValue.Add(parameterDiscreteValue);
                            oDoc.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.StringField:

                            parameterDiscreteValue.Value = Convert.ToString(value);
                            parameterValue.Add(parameterDiscreteValue);
                            oDoc.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                    }
                }
            }
        }

    }
}
