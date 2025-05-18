using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Types;
using DataAccess.Repositories;

namespace Services
{
	public class RegistrationService
	{
		//UserRepository _userRepository;
		EncryptionService _encryptionService;
		UserRepository _userRepository;

		public RegistrationService()
		{
			//_userRepository = new UserRepository();
			_encryptionService = new EncryptionService();
		}

		public async Task<bool> RegisterUser(User user)
		{
			user.PasswordHash = _encryptionService.ComputeSHA256Hash(user.PasswordHash);

			_userRepository.CreateUser(user);
			return false;

		}

		public bool isExistingUser(string email)
		{
			//return (_userRepository.GetUser(email) == null) ? false : true;
			return false;
		}
	}
}
