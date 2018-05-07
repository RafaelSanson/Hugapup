using System.Threading.Tasks;
using Firebase.Auth;

namespace Hugapup.API.Implementations.Boundaries
{
	public interface IUserAuthentication
	{

		Task<FirebaseUser>  Authenticate(User user);
	}
}
