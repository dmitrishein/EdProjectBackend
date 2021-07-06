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
using EdProject.PresentationLayer.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace EdProject.PresentationLayer
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
            services.AddCors();
            services.AddMvc();
            #region IOptionClasses
            services.Configure<EmailOptions>(Configuration.GetSection("EmailProvider"));
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.Configure<RoutingOptions>(Configuration.GetSection("ApiRoutes"));
            services.Configure<StripeOptions>(Configuration.GetSection("Stripe"));
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EdProject.PresentationLayer", Version = "v1" });

            });

            services.AddAuthentication(
                         options =>
                         {
                             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                         })
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = Configuration["Jwt:Audience"],
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        };
                    })
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                        options.LoginPath = new PathString("/Admin/Login");
                    })
                    .AddIdentityCookies();

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
                                   typeof(PaymentProfile),typeof(AuthorProfile), typeof(AccountProfile));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddControllers();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
                       options.WithOrigins("http://localhost:4200/")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin()
             );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseHsts();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EdProject.PresentationLayer v1"));
            }

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                       name: "AdminArea",
                       areaName: "Admin",
                       pattern: "Admin/{controller=Admin}/{action=Login}");
                endpoints.MapAreaControllerRoute(
                   name: "default",
                   areaName: "Admin",
                   pattern: "{controller=Admin}/{action=Login}");
                endpoints.MapControllers();
            });
        }
    }
   
}
