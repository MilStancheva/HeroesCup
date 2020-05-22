using System;

namespace ClubsModule.Security
{
    public interface IUserManager
    {
        bool IsCurrentUserAdmin();

        Guid? GetCurrentUserId();
    }
}