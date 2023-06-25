namespace JWTAPI.Extensions;

public static class MiddlewareExtensions
{
	public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(cfg =>
		{
			cfg.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Server Payment API",
				Version = "v4",
				Description = ".",
				Contact = new OpenApiContact
				{
					Name = "Hooman",
					Url = new Uri("https://www.ponisha.ir")
				},
				License = new OpenApiLicense
				{
					Name = "Close",
				},
			});

			cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "JSON Web Token to access resources. Example: Bearer {token}",
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey
			});

			cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
					},
					new [] { string.Empty }
				}
			});
		});

		return services;
	}

	public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
	{
		app.UseSwagger().UseSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT API");
			options.DocumentTitle = "Server Payment API";
		});

		return app;
	}
}
