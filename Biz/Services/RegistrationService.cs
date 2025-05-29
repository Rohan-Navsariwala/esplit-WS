using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Types;
using Common.Utils;
using DataAccess.Repositories;

namespace Biz.Services
{
	public class RegistrationService
	{
		Encryption _encryption;
		UserRepository _userRepository;

		public RegistrationService()
		{
			_userRepository = new UserRepository();
			_encryption = new Encryption();
		}

		public bool RegisterUser(User user)
		{
			if (!isExistingUser(user.UserName))
			{
				user.PasswordHash = _encryption.ComputeSHA256Hash(user.PasswordHash);
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
