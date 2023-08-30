using System.Security.Claims;
using Versteigerungs_App.Models;

namespace Versteigerungs_App.Utils;

public static class UserExtensions
{
    // TODO: Make configurable with environment variable
    private static readonly IEnumerable<string> Admins = new List<string> {"PF@diamant-software.de", "admin2"};
    public static User GetUser(this ClaimsPrincipal claimsPrincipal)
    {
        return new User { Username = claimsPrincipal.Identities.First().Name ?? throw new ArgumentException("no user name found")};
    }

    public static bool IsAdmin(this User user)
    {
        return Admins.Any(a => a == user.Username);
    }
}
