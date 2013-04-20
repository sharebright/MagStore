﻿using System;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;

namespace RavenDbMembership
{
	public static class PasswordUtil
	{
		public static string CreateRandomSalt()
		{
			var saltBytes = new Byte[4];
			var rng = new RNGCryptoServiceProvider();
			rng.GetBytes(saltBytes);
			return Convert.ToBase64String(saltBytes);
		}

		public static string HashPassword(string pass, string salt)
		{
			var bytes = Encoding.Unicode.GetBytes(pass);
			var src = Encoding.Unicode.GetBytes(salt);
			var dst = new byte[src.Length + bytes.Length];
			Buffer.BlockCopy(src, 0, dst, 0, src.Length);
			Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
			var algorithm = HashAlgorithm.Create("SHA1");
		    Debug.Assert(algorithm != null, "algorithm != null");
		    var inArray = algorithm.ComputeHash(dst);
			return Convert.ToBase64String(inArray);
		}
	}
}
