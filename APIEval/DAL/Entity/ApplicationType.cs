using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
	public enum ApplicationType
	{
		[Display(Name = "Grand public")]
		GrandPublic,

		Professionnelle
	}
}
