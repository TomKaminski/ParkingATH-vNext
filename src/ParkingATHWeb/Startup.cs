using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkingATHWeb.Resolver.Mappings;
using ParkingATHWeb.Resolver.Modules;
using ParkingATHWeb.Mappings;
using Microsoft.AspNet.Authentication.OAuthBearer;
using Microsoft.AspNet.Authorization;

namespace ParkingATHWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("DefaultSettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            FrontendMappingsProvider.InitMappings();
            BackendMappingProvider.InitMappings();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Create the Autofac container builder.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ContractModule());
            builder.RegisterModule(new EfModule());
            builder.RegisterModule(new RepositoryModule());

            // Build the container.'
            builder.Populate(services);
            var container = builder.Build();

            //RsaSecurityKey key;
            //using (var textReader = new System.IO.StreamReader(stream))
            //{
            //    RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
            //    publicAndPrivate.FromXmlString(textReader.ReadToEnd());

            //    key = new RsaSecurityKey(publicAndPrivate.ExportParameters(true));
            //}

            //services.AddInstance(new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest));

            //services.Configure<OAuthBearerAuthenticationOptions>(bearer =>
            //{
            //    bearer.TokenValidationParameters.IssuerSigningKey = key;
            //    bearer.TokenValidationParameters.ValidAudience = TokenAudience;
            //    bearer.TokenValidationParameters.ValidIssuer = TokenIssuer;
            //});

            //services.ConfigureAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationTypes(OAuthBearerAuthenticationDefaults.AuthenticationType)
            //        .RequireAuthenticatedUser().Build());
            //});

            // Resolve and return the service provider.
            return container.Resolve<IServiceProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //TODO: wait until Microsoft.AspNet.Authentication.OAuthBearer RC1...
            //app.UseOAuthBearerAuthentication();

            app.UseCookieAuthentication(opt =>
            {
                opt.LoginPath = new PathString("/Portal/Logowanie");
                opt.LogoutPath = new PathString("/Portal/Wyloguj");
                opt.ExpireTimeSpan = new TimeSpan(4, 0, 0, 0);
                opt.AutomaticAuthenticate = true;
                opt.ReturnUrlParameter = "ReturnUrl";
                opt.AutomaticChallenge = true;
            });

            app.UseCors(x => x.AllowAnyHeader());

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
                app.UseExceptionHandler("/Error");
            //}

            app.UseStatusCodePagesWithReExecute("/Error/Status/{0}");

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use((context, next) =>
            {
                context.Response.StatusCode = 404;
                return next();
            });
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
