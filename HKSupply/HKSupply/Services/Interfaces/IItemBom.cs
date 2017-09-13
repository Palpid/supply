using HKSupply.Models;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    public interface IItemBom
    {
        ItemBom GetItemSupplierBom(int IdBom, string idSupplier, bool getPoco = false);
        ItemBom GetItemSupplierBom(string IdItemBcn, string idSupplier, bool getPoco = false);
        List<ItemBom> GetItemBom(string idItemBcn);
        //bool EditItemBom(ItemBom bom);
        bool EditItemSuppliersBom(List<ItemBom> itemSuppliersBom);
        List<ItemBom> GetRelatedItemBom(int idBomn);
    }
}
