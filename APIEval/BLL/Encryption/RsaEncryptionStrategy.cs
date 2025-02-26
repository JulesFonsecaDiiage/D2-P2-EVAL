using BLL.Encryption;
using System.Security.Cryptography;
using System.Text;

public class RsaEncryptionStrategy : IEncryptionStrategy
{
	private readonly RSA _rsa;
	private readonly string _privateKey;
	private readonly string _publicKey;

	public RsaEncryptionStrategy()
	{
		_rsa = RSA.Create();
		_rsa.KeySize = 2048;

		if (File.Exists("private_key.xml") && File.Exists("public_key.xml"))
		{
			_privateKey = File.ReadAllText("private_key.xml");
			_publicKey = File.ReadAllText("public_key.xml");
			_rsa.FromXmlString(_privateKey);
		}
		else
		{
			_privateKey = _rsa.ToXmlString(true);
			_publicKey = _rsa.ToXmlString(false);
			File.WriteAllText("private_key.xml", _privateKey);
			File.WriteAllText("public_key.xml", _publicKey);
		}
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
