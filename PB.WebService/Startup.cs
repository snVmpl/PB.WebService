using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PB.Core.Interfaces.Services;
using PB.Core.Interfaces.Validators;
using PB.Core.Services;
using PB.Data;
using PB.Data.Entities;
using PB.Data.Interfaces;
using PB.Data.Providers;
using PB.Data.Repositories;
using PB.WebService.Security;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PB.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Fills collection of connection strings for switch db
            var connectionStrings = Configuration.GetSection("ConnectionStrings").GetChildren();

            foreach (var item in connectionStrings)
            {
                ConnectionStringProvider.Instance.AddConnectionString(item.Key, item.Value);
            }

            ConnectionStringProvider.Instance.SetCurrentConnection("DefaultConnection");

            // Factory for switch db
            services.AddTransient<DataBaseContextFactory>();
            services.AddTransient(provider => provider.GetService<DataBaseContextFactory>().CreateDatabaseContext());

            // Register custom media type
            services.AddControllers(options =>
            {
                var jsonInputFormatter = options.InputFormatters
                    .OfType<SystemTextJsonInputFormatter>()
                    .First();

                jsonInputFormatter.SupportedMediaTypes.Add("text/product");

                options.Conventions.Insert(0, new VersionRouteConvention(Configuration["ApiVersion"]));
            });

            // Auth scheme for check odd clientId
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddScheme<ClientIdAuthenticationOptions, ClientIdAuthenticationHandler>(ClientIdAuthenticationOptions.Scheme,
                    options => { })
                .AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OddParity", policy =>
                    {
                        policy.Requirements.Add(new ParityRequirement(false));
                        policy.RequireAuthenticatedUser();
                        policy.AuthenticationSchemes.Add(ClientIdAuthenticationOptions.Scheme);
                    }
                    );
                options.AddPolicy("EvenParity", policy =>
                {
                    policy.Requirements.Add(new ParityRequirement(true));
                    policy.RequireAuthenticatedUser();
                    policy.AuthenticationSchemes.Add(ClientIdAuthenticationOptions.Scheme);
                });
            });

            services.AddSingleton<IAuthorizationHandler, ParityHandler>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("clientId", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "clientId" }
                        },
                        new string[] { }
                    }
                });

            });

            // Register repos, services and validators
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IValidationService<Product>), typeof(ProductValidationService));
            services.AddTransient<IProductService, ProductService>();
            ValidatorsRegister(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // Register validators using reflection
        private void ValidatorsRegister(IServiceCollection services)
        {
            var interfaceType = typeof(IValidator);

            var interfaceTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(c => c.GetTypes())
                .Where(c => interfaceType != c && interfaceType.IsAssignableFrom(c) && c.IsInterface);

            foreach (var item in interfaceTypes)
            {
                var implements = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(c => c.GetTypes())
                    .Where(c => item.IsAssignableFrom(c) && c.IsClass);

                foreach (var implement in implements)
                {
                    services.AddTransient(item, implement);
                }
            }
        }
    }
}
