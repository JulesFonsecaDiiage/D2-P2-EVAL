
using DAL.Entity;

namespace BLL.Encryption
{

	public class EncryptionStrategyFactory
	{
		public static IEncryptionStrategy GetStrategy(string appType)
		{
			return appType switch
			{
				nameof(ApplicationType.GrandPublic) => new AesEncryptionStrategy(),
				nameof(ApplicationType.Professionnelle) => new RsaEncryptionStrategy(),
				_ => throw new ArgumentException("Type d'application inconnu")
			};
		}
	}

}
