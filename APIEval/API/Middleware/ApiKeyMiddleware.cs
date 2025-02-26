﻿namespace API.Middleware
{
	public class ApiKeyMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _apiKey;

		public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			_apiKey = configuration["ApiKey"];
		}

		public async Task Invoke(HttpContext context)
		{
			if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
			{
				context.Response.StatusCode = 401;
				await context.Response.WriteAsync("API Key is missing.");
				return;
			}

			if (!_apiKey.Equals(extractedApiKey))
			{
				context.Response.StatusCode = 403;
				await context.Response.WriteAsync("Unauthorized.");
				return;
			}

			await _next(context);
		}
	}
}
