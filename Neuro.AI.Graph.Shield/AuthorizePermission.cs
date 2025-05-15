namespace Neuro.AI.Graph.Shield;

using Microsoft.AspNetCore.Authorization;

public class AuthorizePermissionAttribute : AuthorizeAttribute
{
    public AuthorizePermissionAttribute(Permissions permission)
    {
        Policy = permission.ToString();
    }
}
