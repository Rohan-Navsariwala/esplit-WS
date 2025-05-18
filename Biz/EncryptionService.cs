using System;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
	public class EncryptionService
	{
		public string ComputeSHA256Hash(string input)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(input);
				byte[] hash = sha256.ComputeHash(bytes);
				return BitConverter.ToString(hash).Replace("-", "").ToLower();
			}
		}
	}
}
