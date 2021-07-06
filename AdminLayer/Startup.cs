using EdProject.BLL;
using EdProject.BLL.Common.Options;
using EdProject.BLL.EmailSender;
using EdProject.BLL.Profiles;
using EdProject.BLL.Providers;
using EdProject.BLL.Providers.Interfaces;
using EdProject.BLL.Services;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.DataContext;
using EdProject.DAL.Entities;
using EdProject.DAL.Repositories;
using EdProject.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login");
                })
                .AddIdentityCookies();
               

            #region IOptionClasses
            services.Configure<EmailOptions>(Configuration.GetSection("EmailProvider"));
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.Configure<RoutingOptions>(Configuration.GetSection("ApiRoutes"));
            services.Configure<StripeOptions>(Configuration.GetSection("Stripe"));
            #endregion

            services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                           .AddRoles<IdentityRole<long>>()
                           .AddSignInManager()
                           .AddDefaultTokenProviders()
                           .AddEntityFrameworkStores<AppDbContext>();

            services.AddDbContext<AppDbContext>(options => options
                                                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                                                .UseLazyLoadingProxies());
            services.AddAuthorization();

            #region Providers Inject
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IEmailProvider, EmailProvider>();
            #endregion

            #region Services Inject
            services.AddScoped<IEditionService, EditionService>();
            services.AddScoped<IAccountService, AccountsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IOrdersService, OrderService>();
            #endregion

            #region Repositories Inject
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IEditionRepository, EditionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            #endregion

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(EditionProfile), typeof(OrderProfile), typeof(UserProfile),
                                   typeof(PaymentProfile), typeof(AuthorProfile), typeof(AccountProfile));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddControllersWithViews();
            
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
            //});
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
