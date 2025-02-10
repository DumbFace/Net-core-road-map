using Domain.EFCore.Entites;
using Infrastucture.Repository.Base;

namespace Infrastucture.Repository.CountryRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
    }
}
