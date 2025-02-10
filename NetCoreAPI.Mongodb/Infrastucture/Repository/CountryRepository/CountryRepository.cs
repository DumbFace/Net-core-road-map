using Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.Repository.Base;

namespace Infrastucture.Repository.CountryRepository
{
    public class CountryRepository : Repository<Country, SecondDbContext>, ICountryRepository
    {
        //public CountryRepository(IUnitOfWork<ExampleDbContext> unitOfWork) : base(unitOfWork)
        //{
        //}

        public CountryRepository(SecondDbContext context) : base(context)
        {
            System.Diagnostics.Debug.WriteLine($"Country Repository context ID:  {context.ContextId}");
        }
    }
}
