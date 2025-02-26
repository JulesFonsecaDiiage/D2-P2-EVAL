using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Encryption
{
	public class AesEncryptionStrategy : IEncryptionStrategy
	{
		private readonly byte[] _key;
		private readonly byte[] _iv;

		public AesEncryptionStrategy()
		{
			// Génération de clé et IV (Stocker en BDD ou dans la config pour du vrai projet)
			_key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef"); // 32 bytes (AES-256)
			_iv = Encoding.UTF8.GetBytes("abcdef0123456789"); // 16 bytes
		}

		public string Encrypt(string plaintext)
		{
			using Aes aes = Aes.Create();
			aes.Key = _key;
			aes.IV = _iv;
			using MemoryStream ms = new();
			using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
			using StreamWriter sw = new(cs);
			sw.Write(plaintext);
			return Convert.ToBase64String(ms.ToArray());
		}

		public string Decrypt(string ciphertext)
		{
			using Aes aes = Aes.Create();
			aes.Key = _key;
			aes.IV = _iv;
			using MemoryStream ms = new(Convert.FromBase64String(ciphertext));
			using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
			using StreamReader sr = new(cs);
			return sr.ReadToEnd();
		}
	}
}
