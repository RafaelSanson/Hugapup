using Firebase.Auth;
using Hugapup.API.Implementations.Boundaries;
using Hugapup.API.Implementations.Models;
using NUnit.Framework;

namespace Hugapup.API.Tests.Editor.Boundaries
{
	public class UserAuthenticationTest {
	
		[Test]
		public async void WhenAuthenticatingWithUnexistingCredentialsThenUserWillBeNull()
		{
			FirebaseUser firebaseUser = null;
			var user = new User("teste@teste.com", "xyz");
			IUserAuthentication userAuthentication = new UserAuthenticationAdapter();
			await userAuthentication.Authenticate(user).ContinueWith(task => {
				firebaseUser = task.Result;
			});
			Assert.AreEqual(null, firebaseUser);
		}
		
		[Test]
		public async void WhenAuthenticatingThenResultUserMustHaveSameEmail()
		{
			FirebaseUser newUser = null;
			var user = new User("rafael@voorhees.in", "xyzxyz");
			IUserAuthentication userAuthentication = new UserAuthenticationAdapter();
			await userAuthentication.Authenticate(user).ContinueWith(task => {
				newUser = task.Result;
			});
			Assert.AreEqual(newUser.Email, "rafael@voorhees.in");
		}
	}
}
