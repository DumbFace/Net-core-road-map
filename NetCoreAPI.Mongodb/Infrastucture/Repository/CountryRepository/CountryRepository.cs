using Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;
using Infrastucture.UnitOfWork;

namespace Infrastucture.Repository.CountryRepository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork<ExampleDbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
