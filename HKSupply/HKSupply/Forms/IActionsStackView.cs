using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Forms
{
    /// <summary>
    /// Interface para los métodos básicos que tienen que implementar los formularios que quieran tener la barra de acciones
    /// </summary>
    public interface IActionsStackView
    {
        void actionsStackView_EditButtonClick(object sender, EventArgs e);
        void actionsStackView_NewButtonClick(object sender, EventArgs e);
        void actionsStackView_SaveButtonClick(object sender, EventArgs e);
        void actionsStackView_CancelButtonClick(object sender, EventArgs e);
        void ConfigureActionsStackView();
    }
}
