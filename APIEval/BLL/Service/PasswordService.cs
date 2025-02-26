using BLL.DTO;
using BLL.Encryption;
using BLL.ServiceContract;
using DAL.Entity;
using DAL.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Service
{
	public class PasswordService : IPasswordService
	{
		private readonly IPasswordRepository _passwordRepository;
		private readonly IApplicationRepository _applicationRepository;

		public PasswordService(IPasswordRepository passwordRepository, IApplicationRepository applicationRepository)
		{
			_passwordRepository = passwordRepository;
			_applicationRepository = applicationRepository;
		}

		public async Task<IEnumerable<PasswordDto>> GetAllAsync()
		{
			var passwords = await _passwordRepository.GetAll().ToListAsync();

			passwords.ForEach(p =>
			{
				var application = _applicationRepository.GetById(p.ApplicationId).Result;
				IEncryptionStrategy encryptionStrategy = EncryptionStrategyFactory.GetStrategy(application.Type.ToString());
				p.EncryptedPassword = encryptionStrategy.Decrypt(p.EncryptedPassword);
			});

			return passwords.Select(p => new PasswordDto
			{
				Id = p.Id,
				AccountName = p.AccountName,
				ApplicationId = p.ApplicationId,
				Password = p.EncryptedPassword
			}).ToList();
		}

		public async Task<PasswordDto> CreateAsync(PasswordDto passwordDto)
		{
			var application = await _applicationRepository.GetById(passwordDto.ApplicationId);
			if (application == null)
			{
				throw new ArgumentException("Invalid application id");
			}

			IEncryptionStrategy encryptionStrategy = EncryptionStrategyFactory.GetStrategy(application.Type.ToString());
			string encryptedPassword = encryptionStrategy.Encrypt(passwordDto.Password);

			var password = new Password
			{
				AccountName = passwordDto.AccountName,
				EncryptedPassword = encryptedPassword,
				ApplicationId = passwordDto.ApplicationId
			};

			await _passwordRepository.Create(password);

			return passwordDto;
		}

		public async Task<int?> DeleteAsync(int id)
		{
			return await _passwordRepository.DeleteById(id);
		}
	}
}
