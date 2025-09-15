using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace CompuMedical.Core.IBasRepository;

public interface IRepositoryApp<T> where T : class
{
    #region main operation
    void Add(T entity);
    void AddRange(List<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    #endregion

    #region main operation Async
    Task<T> Insert(T entity);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(ICollection<T> entities);
    Task UpdateAsync(T entity);
    Task ModifyAsync(T entity);
    Task UpdateRangeAsync(ICollection<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(ICollection<T> entities);
    #endregion

    #region GetById Async
    Task<T> GetByIdAsync(int id);
    T GetById(int id);

    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);

    T GetById(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    #endregion

    #region GetALL Async =>(IEnumerable)
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
    #endregion

    #region  GetALL Async =>(IQueryable)
    IQueryable<T> GetAll();

    IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);

    IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition);

    IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
    #endregion

    #region  No Tracking =>(IQueryable)

    IQueryable<T> GetTableNoTracking();

    IQueryable<T> GetTableNoTracking(Expression<Func<T, bool>> whereCondition);

    #endregion

    #region  Count Async =>(IQueryable)
    Task<double> Count();
    Task<double> Count(params Expression<Func<T, object>>[] includes);

    Task<double> Count(Expression<Func<T, bool>> whereCondition);

    Task<double> Count(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);

    #endregion

    #region SingleOrDefault Async
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition);
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
    Task<T> SingleOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> whereCondition);
    T SingleOrDefaultAsNoTracking(Expression<Func<T, bool>> whereCondition);
    Task<T> SingleOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
    T SingleOrDefaultAsNoTracking(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);
    T SingleOrDefault(Expression<Func<T, bool>> whereCondition);
    T SingleOrDefault(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes);

    #endregion

    #region OrderBy
    IEnumerable<T> OrderBy(Expression<Func<T, int>> whereCondition);
    IEnumerable<T> OrderByDescending(Expression<Func<T, int>> whereCondition);
    #endregion

    #region checkState
    public bool checkState(T entity, string state);
    public DbSet<T> GetContext();

    #endregion

    #region Get first
    Task<T> FirstAsync();
    T First();
    Task<T> FirstAsNoTrackingAsync();
    #endregion

    #region Save
    IDbContextTransaction BeginTransaction();
    void Commit();
    void RollBack();
    Task SaveChangesAsync();
    #endregion
}
