using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
	public class PasswordDto
	{
		public int Id { get; set; }
		public string AccountName { get; set; }
		public string Password { get; set; }
		public int ApplicationId { get; set; }
	}
}
