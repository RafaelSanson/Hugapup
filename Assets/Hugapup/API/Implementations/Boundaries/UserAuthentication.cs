using System.Threading.Tasks;
using Firebase.Auth;
using Hugapup.API.Implementations.Model;

namespace Hugapup.API.Implementations.Boundaries
{
	public interface IUserAuthentication
	{
		Task<FirebaseUser> Authenticate(User user);
	}
}
