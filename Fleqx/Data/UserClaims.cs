using System.Security.Claims;

namespace Fleqx.Helper
{
	public class UserClaims : ClaimsPrincipal
	{
		public UserClaims(ClaimsPrincipal principal)
			: base(principal)
		{
		}

		// Get the name.
		public string FirstName
		{
			get { return FindFirst(ClaimTypes.Name).Value; }
		}

		// Get the name.
		public string LastName
		{
			get { return FindFirst(ClaimTypes.Name).Value; }
		}
	}
}