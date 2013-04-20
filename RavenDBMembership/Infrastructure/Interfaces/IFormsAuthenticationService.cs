﻿namespace RavenDbMembership.Infrastructure.Interfaces
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
