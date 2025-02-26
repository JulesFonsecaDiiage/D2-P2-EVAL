using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
	public class Password
	{
		public int Id { get; set; }
		public string AccountName { get; set; }
		public string EncryptedPassword { get; set; }
		public int ApplicationId { get; set; }
		public Application? Application { get; set; }
	}
}
