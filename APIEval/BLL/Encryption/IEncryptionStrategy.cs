using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Encryption
{
	public interface IEncryptionStrategy
	{
		string Encrypt(string plaintext);
		string Decrypt(string ciphertext);
	}
}
