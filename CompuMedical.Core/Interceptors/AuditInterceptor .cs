using CompuMedical.Core.BaseEntities;
using CompuMedical.Core.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CompuMedical.Core.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyAuditInfo(eventData.Context);
        ApplySoftDelete(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo(eventData.Context);
        ApplySoftDelete(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAuditInfo(DbContext context)
    {
        if (context == null) return;

        var currentUserId = _currentUserService.CurrentUserId;
        var currentTime = DateTime.Now;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = currentTime;
                entry.Entity.CreatedByUserId = currentUserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedDate = currentTime;
                entry.Entity.LastModifiedByUserId = currentUserId;
            }
        }
    }

    private void ApplySoftDelete(DbContext context)
    {
        if (context == null) return;

        var currentUserId = _currentUserService.CurrentUserId;
        var currentTime = DateTime.Now;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Deleted && entry.Entity is BaseEntity entity)
            {
                // Soft delete by setting the IsDeleted flag
                entry.State = EntityState.Modified;
                entity.IsDeleted = true;
                entity.DeletedDate = currentTime;
                entity.DeletedByUserId = currentUserId;
            }
        }
    }
}
