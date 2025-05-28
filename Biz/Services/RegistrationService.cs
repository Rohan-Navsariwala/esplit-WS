using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Types;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class RegistrationService
	{
		//UserRepository _userRepository;
		EncryptionService _encryptionService;
		UserRepository _userRepository;

		public RegistrationService()
		{
			_userRepository = new UserRepository();
			_encryptionService = new EncryptionService();
		}

		public bool RegisterUser(User user)
		{
			if (!isExistingUser(user.UserName))
			{
				user.PasswordHash = _encryptionService.ComputeSHA256Hash(user.PasswordHash);
				_userRepository.CreateUser(user);
				return true;
			}
			else
			{
				return false;
			}

		}

		public bool isExistingUser(string userName)
		{
			return _userRepository.GetUserByUserName(userName) == null ? false : true;
		}
	}
}
