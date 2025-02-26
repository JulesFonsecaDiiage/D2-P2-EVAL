using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
	public class Application
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ApplicationType Type { get; set; }
		public List<Password>? Passwords { get; set; }
	}
}
