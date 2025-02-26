using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceContract
{
	public interface IApplicationService
	{
		Task<IEnumerable<ApplicationDto>> GetAllAsync();
		Task<ApplicationDto> CreateAsync(ApplicationDto applicationDto);
	}
}
