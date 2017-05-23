using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    public interface IItemBom
    {
        ItemBom GetItemBom(int IdBom);
        ItemBom GetItemBom(string IdItemBcn);
        bool EditIteBom(ItemBom bom);
    }
}
