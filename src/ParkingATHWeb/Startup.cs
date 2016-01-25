using System;
using System.IdentityModel.Tokens;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNet.Authentication.JwtBearer;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkingATHWeb.Resolver.Mappings;
using ParkingATHWeb.Resolver.Modules;
using ParkingATHWeb.Mappings;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Diagnostics;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using ParkingATHWeb.Infrastructure.Attributes;
using ParkingATHWeb.Infrastructure.TokenAuth;

namespace ParkingATHWeb
{
    public class Startup
    {
        //private RsaSecurityKey _key;
        //private TokenAuthOptions _tokenOptions;
        //private string _appBasePath;

        public Startup(IHostingEnvironment env)
        {
            //_appBasePath = appEnv.ApplicationBasePath;

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
            //Admin requirement :-)
            services.Configure<AuthorizationOptions>(
                options => options.AddPolicy("Admin", policy => policy.Requirements.Add(new AdminRequirement())));

            #region TokenAuth
            //var keyParams = RSAKeyUtils.GetKeyParameters($"{_appBasePath}/RSAKey.json");

            //_key = new RsaSecurityKey(keyParams);
            //_tokenOptions = new TokenAuthOptions
            //{
            //    Audience = Configuration["TokenAuth:TokenAudience"],
            //    Issuer = Configuration["TokenAuth:TokenIssuer"],
            //    SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.RsaSha256Signature)
            //};

            //services.AddInstance(_tokenOptions);

            // Enable the use of an [Authorize("Bearer")] attribute on methods and classes to protect.
            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});
            #endregion

            // Add framework services.
            services.AddMvc();

            // Create the Autofac container builder.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ContractModule());
            builder.RegisterModule(new EfModule());
            builder.RegisterModule(new RepositoryModule());

            // Build the container
            builder.Populate(services);
            var container = builder.Build();



            // Resolve and return the service provider.
            return container.Resolve<IServiceProvider>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region TokenAuth
            //// Register a simple error handler to catch token expiries and change them to a 401, 
            //// and return all other errors as a 500. This should almost certainly be improved for
            //// a real application.
            //app.UseExceptionHandler(appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
            //        // This should be much more intelligent - at the moment only expired 
            //        // security tokens are caught - might be worth checking other possible 
            //        // exceptions such as an invalid signature.
            //        if (error != null && error.Error is SecurityTokenExpiredException)
            //        {
            //            context.Response.StatusCode = 401;
            //            // What you choose to return here is up to you, in this case a simple 
            //            // bit of JSON to say you're no longer authenticated.
            //            context.Response.ContentType = "application/json";
            //            await context.Response.WriteAsync(
            //                JsonConvert.SerializeObject(
            //                    new { authenticated = false, tokenExpired = true }));
            //        }
            //        else if (error != null && error.Error != null)
            //        {
            //            context.Response.StatusCode = 500;
            //            context.Response.ContentType = "application/json";
            //            // TODO: Shouldn't pass the exception message straight out, change this.
            //            await context.Response.WriteAsync(
            //                JsonConvert.SerializeObject
            //                (new { success = false, error = error.Error.Message }));
            //        }
            //        // We're not trying to handle anything else so just let the default 
            //        // handler handle.
            //        else await next();
            //    });
            //});

            //app.UseJwtBearerAuthentication(options =>
            //{
            //    // Basic settings - signing key to validate with, audience and issuer.
            //    options.TokenValidationParameters.IssuerSigningKey = _key;
            //    options.TokenValidationParameters.ValidAudience = _tokenOptions.Audience;
            //    options.TokenValidationParameters.ValidIssuer = _tokenOptions.Issuer;

            //    // When receiving a token, check that we've signed it.
            //    options.TokenValidationParameters.ValidateSignature = true;

            //    // When receiving a token, check that it is still valid.
            //    options.TokenValidationParameters.ValidateLifetime = true;

            //    // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
            //    // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
            //    // machines which should have synchronised time, this can be set to zero. Where external tokens are
            //    // used, some leeway here could be useful.
            //    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(0);
            //});
            #endregion

            app.UseCookieAuthentication(opt =>
            {
                opt.LoginPath = new PathString("/Portal/Konto/Start");
                opt.LogoutPath = new PathString("/Portal/Wyloguj");
                opt.ExpireTimeSpan = new TimeSpan(4, 0, 0, 0);
                opt.AutomaticAuthenticate = true;
                opt.ReturnUrlParameter = "ReturnUrl";
                opt.AutomaticChallenge = true;
            });

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());


            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Error/Status/{0}");

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
