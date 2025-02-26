using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Encryption
{
	public class RsaEncryptionStrategy : IEncryptionStrategy
	{
		private readonly RSA _rsa;

		public RsaEncryptionStrategy()
		{
			_rsa = RSA.Create();
			_rsa.KeySize = 2048;
		}

		public string Encrypt(string plaintext)
		{
			byte[] data = Encoding.UTF8.GetBytes(plaintext);
			byte[] encryptedData = _rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
			return Convert.ToBase64String(encryptedData);
		}

		public string Decrypt(string ciphertext)
		{
			byte[] encryptedData = Convert.FromBase64String(ciphertext);
			byte[] decryptedData = _rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
			return Encoding.UTF8.GetString(decryptedData);
		}
	}
}
