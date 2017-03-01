using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class CustomToolStripMenuItem : ToolStripMenuItem
    {
        string _formName;

        public string FormName { get { return _formName; } set { _formName = value; } }

        public CustomToolStripMenuItem(string text, Image image, EventHandler onClick, string name) : base (text, image, onClick, name)
        {
            InitializeComponent();
        }

        //public CustomToolStripMenuItem()
        //{
        //    InitializeComponent();
        //}
    }
}
