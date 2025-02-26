using BLL.DTO;
using BLL.ServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/passwords")]
	public class PasswordController : ControllerBase
	{
		private readonly IPasswordService _passwordService;

		public PasswordController(IPasswordService passwordService)
		{
			_passwordService = passwordService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PasswordDto>>> GetAllPasswords()
		{
			var passwords = await _passwordService.GetAllAsync();

			return Ok(passwords);
		}

		[HttpPost]
		public async Task<ActionResult<PasswordDto>> CreatePassword([FromBody] PasswordDto passwordDto)
		{
			if (passwordDto == null) return BadRequest("Invalid Data");

			var createdPassword = await _passwordService.CreateAsync(passwordDto);

			return CreatedAtAction(nameof(GetAllPasswords), createdPassword);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeletePassword(int id)
		{
			var passwordId = await _passwordService.DeleteAsync(id);

			if (passwordId == null) return NotFound();

			return NoContent();
		}
	}
}
