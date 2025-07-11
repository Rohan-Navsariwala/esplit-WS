﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils
{
	public static class Encryption
	{
		public static string ComputeSHA256Hash(string input)
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
