using HKSupply.General;
using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKSupply.Classes
{
    /// <summary>
    /// Clase auxiliar para representar el bom en un grid plano en lugar del arbol
    /// </summary>
    public class PlainBomAux
    {
        public string IdItemBcn { get; set; }
        public string ItemDescription { get; set; }
        public string ItemGroup { get; set; }
        public decimal Quantity { get; set; }
        public decimal Waste { get; set; }

        public static implicit operator PlainBomAux(DetailBomMt detailBomMt)
        {
            PlainBomAux aux = new PlainBomAux()
            {
                IdItemBcn = detailBomMt.IdItemBcn,
                ItemDescription = detailBomMt.Item.ItemDescription,
                ItemGroup = Constants.ITEM_GROUP_MT,
                Quantity = detailBomMt.Quantity,
                Waste = detailBomMt.Waste
            };

            return aux;
        }

        public static implicit operator PlainBomAux(DetailBomHw detailBomHw)
        {
            PlainBomAux aux = new PlainBomAux()
            {
                IdItemBcn = detailBomHw.IdItemBcn,
                ItemDescription = detailBomHw.Item.IdItemBcn,
                ItemGroup = Constants.ITEM_GROUP_HW,
                Quantity = detailBomHw.Quantity,
                Waste = detailBomHw.Waste
            };

            return aux;
        }
    }
}
