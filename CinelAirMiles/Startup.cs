using CinelAirMiles.Helpers;
using CinelAirMilesLibrary.Common.Data;
using CinelAirMilesLibrary.Common.Data.Entities;
using CinelAirMilesLibrary.Common.Data.Repositories;
using CinelAirMilesLibrary.Common.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MilesBackOffice.Web.Helpers;
using System;
using System.Text;

namespace CinelAirMiles
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _env = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.SignIn.RequireConfirmedEmail = true;
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 6;

            })
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<DataContext>();



            services.AddAuthentication()

             .AddJwtBearer(cfg =>
             {
                 cfg.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidIssuer = Configuration["Tokens:Issuer"],
                     ValidAudience = Configuration["Tokens:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                 };
             });

            services
                .AddAuthentication()

                 .AddGoogle(options =>
                 {
                     options.ClientId = Configuration["App:GoogleClientId"];
                     options.ClientSecret = Configuration["App:GoogleClientSecret"];
                     options.SignInScheme = IdentityConstants.ExternalScheme;
                 })
                 .AddFacebook(options =>
                 {
                     options.ClientId = Configuration["App:FacebookClientId"];
                     options.ClientSecret = Configuration["App:FacebookClientSecret"];
                     options.SignInScheme = IdentityConstants.ExternalScheme;
                 })


                 .AddCookie(options =>
                 {
                     options.Cookie.Name = ".AspNet.ExternalCookie";
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                     options.LoginPath = new PathString("/Account/Login");
                     options.LogoutPath = new PathString("/Account/Logout");
                 });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/NotAuthorized";

            });

            services.AddDbContext<DataContext>(cfg =>
            {
                if (_env.IsDevelopment())
                {
                    cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
                else
                {
                    cfg.UseSqlServer(Configuration.GetConnectionString("SomeeConnection"));
                }
            });


            services.AddScoped<IClientConverterHelper, ClientConverterHelper>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IClientConverterHelper, ClientConverterHelper>();
            services.AddScoped<IBlobHelper, BlobHelper>();
            services.AddScoped<ILog, Log>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IAdvertisingRepository, AdvertisingRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<ITierChangeRepository, TierChangeRepository>();
            services.AddScoped<IPremiumRepository, PremiumRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionHelper, TransactionHelper>();
            services.AddScoped<IReservationRepository, ReservationRepository>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=IndexClient}/{id?}");
            });
        }
    }
}
