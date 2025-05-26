using Common.Types;
using DataAccess.Repositories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Biz.Services
{
	public class AuthService
	{
		private readonly EncryptionService _encryptionService;
		private readonly UserRepository _userRepository;

		public AuthService()
		{
			_encryptionService = new EncryptionService();
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


		private bool VerifyPassword(string enteredPassword, string storedHash)
		{
			return _encryptionService.ComputeSHA256Hash(enteredPassword) == storedHash;
		}
	}
}
