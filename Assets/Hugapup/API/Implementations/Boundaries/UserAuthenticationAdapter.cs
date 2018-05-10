using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Hugapup.API.Implementations.Models;

namespace Hugapup.API.Implementations.Boundaries
{
	public class UserAuthenticationAdapter : IUserAuthentication {

		public Task<FirebaseUser> Authenticate(User user)
		{
			var app = FirebaseApp.DefaultInstance;
			var firebaseAuth = FirebaseAuth.GetAuth(app);
			return firebaseAuth.SignInWithEmailAndPasswordAsync(user.Email, user.Password);
		}
	}
}
