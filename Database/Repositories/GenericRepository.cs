using Database.Databases;
using Domain.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Database.Exceptions;

namespace Database.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly RealDatabase _realDatabase;

        public GenericRepository(RealDatabase realDatabase)
        {
            _dbSet = realDatabase.Set<T>();
            _realDatabase = realDatabase;
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity), "Can't be empty");

            try
            {
                _dbSet.Attach(entity);

                await _dbSet.AddAsync(entity);
                await _realDatabase.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                throw new RepositoryOperationExceptions("Couldn't add the thing", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }

            catch (Exception ex)
            {
                throw new RepositoryOperationExceptions("An error occurred during retrieval.", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity can't be null.");

            try
            {
                _dbSet.Remove(entity);
                await _realDatabase.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                throw new RepositoryOperationExceptions("An error occurred while delting the entity.", ex);
            }
        }

        public async Task<T?> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "*Predicate* cannot be null.");
            
            try
            {
                return await _dbSet.FirstOrDefaultAsync(predicate);
            }

            catch (Exception exception)
            {
                throw new RepositoryOperationExceptions("Error occurred while finding the entity.", exception);
            }
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The ID cannot be empty.", nameof(id));

            try
            {
                var entity = await _dbSet.FindAsync(id);
                return entity ?? throw new KeyNotFoundException($"Entity ID {id} not found.");
            }

            catch (Exception ex)
            {
                throw new RepositoryOperationExceptions("Error occurred while retrieving the entity.", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            try
            {
                _dbSet.Update(entity);
                await _realDatabase.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                throw new RepositoryOperationExceptions("An error occurred while updating the entity.", ex);
            }
        }
    }
}