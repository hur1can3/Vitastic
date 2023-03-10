using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VitasticCore.EntityFramework.Extensions;
using VitasticCore.SharedKernal.Auth;
using VitasticCore.SharedKernal.Time;

namespace Vitastic.Data.EntityFramework;
public partial class FoodStuffsContext
{
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public FoodStuffsContext(DbContextOptions<FoodStuffsContext> options, IDateTimeService dateTimeService, ICurrentUserAccessor currentUserAccessor)
        : base(options)
    {
        _dateTimeService = dateTimeService;
        _currentUserAccessor = currentUserAccessor;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.Entries().SetAllAuditableProperties(_dateTimeService, _currentUserAccessor.User.Login);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries().SetAllAuditableProperties(_dateTimeService, _currentUserAccessor.User.Login);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
