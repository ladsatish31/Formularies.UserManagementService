using System;

namespace Formularies.UserManagementService.Api.Helper
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}
