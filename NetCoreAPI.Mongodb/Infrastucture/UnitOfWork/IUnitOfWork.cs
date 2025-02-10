using Microsoft.EntityFrameworkCore;

namespace Infrastucture.UnitOfWork
{
    public interface IUnitOfWork<out TContext> where TContext : DbContext
        //public interface IUnitOfWork<out TContext> where TContext : DbContext
    {
        //The following Property is going to hold the context object
        public TContext Context { get; }
        //Start the database Transaction
        void CreateTransaction();
        //Commit the database Transaction
        void Commit();
        //Rollback the database Transaction
        void Rollback();
        //DbContext Class SaveChanges method
        void Save();
    }
}
