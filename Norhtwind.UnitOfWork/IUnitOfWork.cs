using Northwind.Repositories;

namespace Norhtwind.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customer { get; }
    }
}
