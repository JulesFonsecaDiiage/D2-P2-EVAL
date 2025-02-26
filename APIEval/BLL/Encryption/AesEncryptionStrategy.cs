using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Encryption
{
	public class AesEncryptionStrategy : IEncryptionStrategy
	{
		private readonly byte[] _key;
		private readonly byte[] _iv;

		public AesEncryptionStrategy()
		{
			// Todo : Store key and iv in a secure place
			_key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef");
			_iv = Encoding.UTF8.GetBytes("abcdef0123456789");
		}

		public string Encrypt(string plaintext)
		{
			if (string.IsNullOrEmpty(plaintext))
				return string.Empty;

			Aes aes = Aes.Create();
			aes.Key = _key;
			aes.IV = _iv;
			MemoryStream ms = new();
			CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
			StreamWriter sw = new(cs);
			sw.Write(plaintext);
			sw.Flush();
			cs.FlushFinalBlock();

			return Convert.ToBase64String(ms.ToArray());
		}

		public string Decrypt(string ciphertext)
		{
			if (string.IsNullOrEmpty(ciphertext))
				return string.Empty;

			try
			{
				byte[] buffer = Convert.FromBase64String(ciphertext);
				Aes aes = Aes.Create();
				aes.Key = _key;
				aes.IV = _iv;
				MemoryStream ms = new(buffer);
				CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
				StreamReader sr = new(cs);

				return sr.ReadToEnd();
			}
			catch (CryptographicException)
			{
				throw new ArgumentException("Invalid ciphertext");
			}
		}
	}
}
