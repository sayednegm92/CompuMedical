
using CompuMedical.Core.IBasRepository;
using CompuMedical.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;


namespace CompuMedical.Infrastructure.BasRepository;

public class RepositoryApp<T> : IRepositoryApp<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected DbSet<T> Entity = null;

    public RepositoryApp(ApplicationDbContext context)
    {
        _context = context;
        Entity = _context.Set<T>();
    }

    #region main operation
    public void Add(T entity)
    {
        Entity.Add(entity);

    }

    public void AddRange(List<T> entities)
    {
        Entity.AddRange(entities);
    }
    public void UpdateRange(IEnumerable<T> entities)
    {
        Entity.UpdateRange(entities);
    }
    public void Update(T entity)
    {
        Entity.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(T entity)
    {
        Entity.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        Entity.RemoveRange(entities);
    }

    #endregion

    #region main operation Async
    public virtual async Task<T> Insert(T entity)
    {
        Entity.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<T> AddAsync(T entity)
    {
        await Entity.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
    public virtual async Task AddRangeAsync(ICollection<T> entities)
    {
        await Entity.AddRangeAsync(entities);
        await _context.SaveChangesAsync();

    }
    public virtual async Task UpdateAsync(T entity)
    {
        Entity.Update(entity);
        await _context.SaveChangesAsync();

    }
    public virtual async Task ModifyAsync(T entity)
    {
        Entity.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

    }
    public virtual async Task UpdateRangeAsync(ICollection<T> entities)
    {

        Entity.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        Entity.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public virtual async Task DeleteRangeAsync(ICollection<T> entities)
    {
        foreach (var entity in entities)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
        await _context.SaveChangesAsync();
    }
    #endregion

    #region GetALL Async =>(IQueryable)
    public IQueryable<T> GetAll()
    {
        return Entity;

    }
    public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(i => true);

        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);

        return result;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition)
    {
        return Entity.Where(whereCondition);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);
        return result;
    }

    #endregion

    #region   No Tracking =>(IQueryable)
    public IQueryable<T> GetTableNoTracking()
    {
        return Entity.AsNoTracking().AsQueryable();
    }

    public IQueryable<T> GetTableNoTracking(Expression<Func<T, bool>> whereCondition)
    {
        return Entity.AsNoTracking().AsQueryable().Where(whereCondition);
    }
    #endregion

    #region GetALL Async =>(IQueryable)
    public async Task<double> Count()
    {
        return await Entity.CountAsync();
    }

    public async Task<double> Count(params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(i => true);

        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);

        return await result.CountAsync();
    }

    public async Task<double> Count(Expression<Func<T, bool>> whereCondition)
    {
        return await Entity.Where(whereCondition).CountAsync();
    }

    public async Task<double> Count(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);
        return await result.CountAsync(); ;
    }

    #endregion

    #region GetALL Async =>(IEnumerable)

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Entity.ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(i => true);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);
        return await result.ToListAsync(); ;
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition)
    {
        return await Entity.Where(whereCondition).ToListAsync();

    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);
        return await result.ToListAsync();
    }

    #endregion

    #region GetById Async  

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await Entity.FindAsync(id);

    }
    public async Task<T> GetByIdAsync(int id)
    {
        return await Entity.FindAsync(id);

    }

    public T GetById(int id)
    {
        return Entity.Find(id);

    }
    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);
        return await result.FirstOrDefaultAsync();

    }
    public T GetById(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);
        return result.FirstOrDefault();

    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Entity.AnyAsync(predicate);
    }
    #endregion

    #region  SingleOrDefaultAsync
    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
    {
        return await Entity.Where(whereCondition).FirstOrDefaultAsync();
    }
    public T SingleOrDefault(Expression<Func<T, bool>> whereCondition)
    {
        return Entity.Where(whereCondition).FirstOrDefault();
    }
    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);

        return await result.FirstOrDefaultAsync();
    }
    public async Task<T> SingleOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> whereCondition)
    {
        return await Entity.Where(whereCondition).AsNoTracking().FirstOrDefaultAsync();
    }
    public T SingleOrDefaultAsNoTracking(Expression<Func<T, bool>> whereCondition)
    {
        return Entity.Where(whereCondition).AsNoTracking().FirstOrDefault();
    }
    public async Task<T> SingleOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);

        return await result.AsNoTracking().FirstOrDefaultAsync();
    }
    public T SingleOrDefault(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);

        return result.FirstOrDefault();
    }
    public T SingleOrDefaultAsNoTracking(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
    {
        var result = Entity.Where(whereCondition);
        foreach (var includeExpression in includes)
            result = result.Include(includeExpression);

        return result.AsNoTracking().FirstOrDefault();
    }
    #endregion

    #region OrderBy
    public IEnumerable<T> OrderBy(Expression<Func<T, int>> whereCondition)
    {
        return Entity.OrderBy(whereCondition);
    }
    public IEnumerable<T> OrderByDescending(Expression<Func<T, int>> whereCondition)
    {
        return Entity.OrderByDescending(whereCondition);
    }
    #endregion

    #region checkState
    public bool checkState(T entity, string state)
    {
        var x = _context.Entry(entity).State;
        return (_context.Entry(entity).State.ToString().ToLower() == state.ToLower().Trim()) ? true : false;
    }

    public DbSet<T> GetContext()
    {
        return Entity;

    }

    public async Task<T> FirstAsync()
    {
        return await Entity.FirstOrDefaultAsync();
    }
    public async Task<T> FirstAsNoTrackingAsync()
    {
        return await Entity.AsNoTracking().FirstOrDefaultAsync();
    }
    public T First()
    {
        return Entity.AsNoTracking().FirstOrDefault();
    }
    public IQueryable<T> GetFromProcedures(string sql, params object[] parameters)
    {
        return Entity.FromSqlRaw(sql).IgnoreQueryFilters();
    }


    #endregion

    #region Save
    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.Database.CommitTransaction();

    }

    public void RollBack()
    {
        _context.Database.RollbackTransaction();

    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion
}
