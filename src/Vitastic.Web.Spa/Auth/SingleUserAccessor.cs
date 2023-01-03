using System;
using VitasticCore.SharedKernal.Auth;

namespace Vitastic.Web.Spa.Auth;

public class SingleUserAccessor : ICurrentUserAccessor
{
    private static readonly DomainUser _singleUser = new("SingleUser", Array.Empty<string>());

    public DomainUser User => _singleUser;
}
