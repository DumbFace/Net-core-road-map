using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Repository.Base
{
    public class Repository<T, TContext> : IRepository<T, TContext>, IDisposable where T
                                         : class where TContext : DbContext
    {

        private TContext _context;

        public TContext Context { get => _context; }

        private DbSet<T> _entity;
        public DbSet<T> Entity { get => _entity; }

        //TContext IRepository<T, TContext>.Context { get => _context; }
        //DbSet<T> IRepository<T, TContext>.Entity { get => _entity; }

        private string _errorMessage = string.Empty;
        private bool _isDisposed;

        //If you don't want to use Unit of Work, then use the following Constructor 
        //which takes the context Object as a parameter
        public Repository(TContext context)
        {
            //Initialize _isDisposed to false and then set the Context Object
            _isDisposed = false;
            _context = context;
            _entity = context.Set<T>();
            System.Diagnostics.Debug.WriteLine($"Context ID Repository: {Context.ContextId}");
        }
        //The following Property is going to return the Context Object

        //The following Property is going to set and return the Entity
        //protected virtual DbSet<T> Entities
        //{
        //    get { return _entities ?? (_entities = Context.Set<T>()); }
        //}
        //The following Method is going to Dispose of the Context Object
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        //Return all the Records from the Corresponding Table
        public virtual IEnumerable<T> GetAll()
        {
            return Entity.ToList();
        }
        //Return a Record from the Coresponding Table based on the Primary Key
        public virtual T GetById(object id)
        {
            return Entity.Find(id);
        }
        //The following Method is going to Insert a new entity into the table
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }
                //TODO
                if (Context == null || _isDisposed)
                {
                    //Context = new ExampleDbContext();
                }
                Entity.Add(entity);
                //commented out call to SaveChanges as Context save changes will be
                //called with Unit of work
                //Context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                HandleUnitOfWorkException(ex);
                throw new Exception(_errorMessage, ex);
            }
        }

        //The following Method is going to Update an existing entity in the table
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }
                //TODO
                if (Context == null || _isDisposed)
                {
                    //Context = new ExampleDbContext();
                }
                Context.Entry(entity).State = EntityState.Modified;
                //commented out call to SaveChanges as Context save changes will be called with Unit of work
                //Context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                HandleUnitOfWorkException(ex);
                throw new Exception(_errorMessage, ex);
            }
        }
        //The following Method is going to Delete an existing entity from the table
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }
                //TODO
                if (Context == null || _isDisposed)
                {
                    //Context = new ExampleDbContext();
                }

                Entity.Remove(entity);
                //commented out call to SaveChanges as Context save changes will be called with Unit of work
                //Context.SaveChanges(); 
            }
            catch (Exception ex)
            {
                HandleUnitOfWorkException(ex);
                throw new Exception(_errorMessage, ex);
            }
        }
        private void HandleUnitOfWorkException(Exception ex)
        {
            //foreach (var validationErrors in dbEx.EntityValidationErrors)
            //{
            //    foreach (var validationError in validationErrors.ValidationErrors)
            //    {
            //        _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
            //    }
            //}
        }
    }
}
