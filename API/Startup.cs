using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using API.Extensions;
using API.Helpers;
using Microsoft.Extensions.Options;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _env = env;
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config, _env, out IOptions<ApiSettings> apiSettings);

            services.AddCors();

            services.AddIdentityServices(apiSettings);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            if (_env.IsDevelopment())
                app.UseCors(policy => policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:4200"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
