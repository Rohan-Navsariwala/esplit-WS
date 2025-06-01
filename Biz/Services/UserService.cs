using Common.Types;
using Common.Utils;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class UserService
	{
		UserRepository _userRepository;

		public UserService()
		{	
			_userRepository = new UserRepository();
		}

		public User Authenticate(string username, string password)
		{
			User user = _userRepository.GetUserByUserName(username);

			if (user == null || !VerifyPassword(password, user.PasswordHash))
			{
				return null;
			}

			return user;
		}

		//public async Task<User> Authenticate(string email, string password)
		//{
		//	using (var httpClient = new HttpClient())
		//	{
		//		string apiUrl = $"{Common.Constants.Credentials._apiBaseUrl}/user/email?email={Uri.EscapeDataString(email)}";
		//		var response = await httpClient.GetAsync(apiUrl);

		//		if (!response.IsSuccessStatusCode)
		//			return null; // User not found

		//		var json = await response.Content.ReadAsStringAsync();
		//		var user = JsonConvert.DeserializeObject<User>(json);

		//		if (user == null || !VerifyPassword(password, user.PasswordHash))
		//			return null;

		//		return user;
		//	}
		//}

		public bool RegisterUser(User user)
		{
			if (!isExistingUser(user.UserName))
			{
				user.PasswordHash = Encryption.ComputeSHA256Hash(user.PasswordHash);
				_userRepository.CreateUser(user);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool LogOut()
		{
			return true;
		}

		private bool VerifyPassword(string enteredPassword, string storedHash)
		{
			return Encryption.ComputeSHA256Hash(enteredPassword) == storedHash;
		}

		public bool isExistingUser(string userName)
		{
			return _userRepository.GetUserByUserName(userName) == null ? false : true;
		}

		public string GetUserName(int userID)
		{
			return _userRepository.GetUserNameByID(userID);
		}

		public int GetUserID(string userName)
		{
			return _userRepository.GetUserIDByUserName(userName);
		}
	}
}
