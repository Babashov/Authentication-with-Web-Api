using System;
namespace AuthTesty
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        Customer = 1<<0,
        Admin = 1<<1,
        Any = ~0
    }
}