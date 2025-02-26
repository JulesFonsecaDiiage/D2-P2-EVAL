using BLL.DTO;
using BLL.ServiceContract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/applications")]
	public class ApplicationController : ControllerBase
	{
		private readonly IApplicationService _applicationService;

		public ApplicationController(IApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAllApplications()
		{
			var applications = await _applicationService.GetAllAsync();

			return Ok(applications);
		}

		[HttpPost]
		public async Task<ActionResult<ApplicationDto>> CreateApplication([FromBody] ApplicationDto applicationDto)
		{
			if (applicationDto == null) return BadRequest("Invalid Data");

			var createdApplication = await _applicationService.CreateAsync(applicationDto);
			return CreatedAtAction(nameof(GetAllApplications), new { id = createdApplication.Id }, createdApplication);
		}
	}
}
