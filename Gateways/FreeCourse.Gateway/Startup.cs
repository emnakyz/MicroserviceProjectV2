using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace FreeCourse.Gateway
{
	public class Startup
	{
		private readonly IConfiguration Configuration;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
			{
				options.Authority = Configuration["IdentityServerUrl"];

				options.Audience = "resource_gateway";

				options.RequireHttpsMetadata = false;
			});
			services.AddOcelot();
		}

		async public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			await app.UseOcelot();

		}
	}
}
