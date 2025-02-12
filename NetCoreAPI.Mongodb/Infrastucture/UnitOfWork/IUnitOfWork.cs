using Infrastucture.Repository.EmployeeRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
        //public interface IUnitOfWork<out TContext> where TContext : DbContext
    {
        public IEmployeeRepository Employees { get; }

        //The following Property is going to hold the context object
        public TContext Context { get; }
        //Start the database Transaction
        void CreateTransaction();
        //Commit the database Transaction
        void Commit();
        //Rollback the database Transactionf
        void Rollback();
        //DbContext Class SaveChanges method
        void Save();
    }
}
