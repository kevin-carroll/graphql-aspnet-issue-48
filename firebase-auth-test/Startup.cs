namespace Firebase.AuthTest
{
    using GraphQL.AspNet.Configuration.Mvc;
    using GraphQL.AspNet.Security;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        public const string FIREBASE_PROJECT_ID = "YOUR_PROJECT_ID";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQL(schemaoptions =>
            {
                schemaoptions.AuthorizationOptions.Method = AuthorizationMethod.PerRequest;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                //options.SaveToken = true;
                //options.RequireHttpsMetadata = false;
                options.Authority = "https://securetoken.google.com/" + FIREBASE_PROJECT_ID;
                options.Audience = "http://localhost:30674";
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = "https://securetoken.google.com/" + FIREBASE_PROJECT_ID,
                    ValidateIssuerSigningKey = false,
                };
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseGraphQL();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}