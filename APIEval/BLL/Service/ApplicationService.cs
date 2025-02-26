using BLL.DTO;
using BLL.ServiceContract;
using DAL.Entity;
using DAL.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
	public class ApplicationService : IApplicationService
	{
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationService(IApplicationRepository applicationRepository)
		{
			_applicationRepository = applicationRepository;
		}

		public async Task<IEnumerable<ApplicationDto>> GetAllAsync()
		{
			var applications = await _applicationRepository.GetAll().ToListAsync();
			return applications.Select(app => new ApplicationDto
			{
				Id = app.Id,
				Name = app.Name,
				Type = app.Type.ToString()
			}).ToList();
		}

		public async Task<ApplicationDto> CreateAsync(ApplicationDto applicationDto)
		{
			var application = new Application
			{
				Name = applicationDto.Name,
				Type = this.GetApplicationType(applicationDto.Type)
			};

			await _applicationRepository.Create(application);
			return applicationDto;
		}

		private ApplicationType GetApplicationType(string type)
		{
			if (Enum.TryParse<ApplicationType>(type, out var applicationType))
			{
				return applicationType;
			}
			throw new ArgumentException("Invalid application type");
		}
	}
}
