using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    public interface IItemBom
    {
        ItemBom GetItemBom(int IdBom, bool getPoco = false);
        ItemBom GetItemBom(string IdItemBcn, bool getPoco = false);
        bool EditIteBom(ItemBom bom);
    }
}
