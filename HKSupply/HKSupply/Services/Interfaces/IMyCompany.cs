using HKSupply.Models;

namespace HKSupply.Services.Interfaces
{
    public interface IMyCompany
    {
        MyCompany GetMyCompany();
        bool UpdateMyCompany(MyCompany myCompany);

    }
}
