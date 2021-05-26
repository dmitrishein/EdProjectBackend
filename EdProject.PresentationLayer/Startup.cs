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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            #region Classes with options
            services.Configure<EmailOptions>(Configuration.GetSection("EmailProvider"));
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.Configure<RoutingOptions>(Configuration.GetSection("ApiRoutes"));
            services.Configure<StripeOptions>(Configuration.GetSection("Stripe"));
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EdProject.PresentationLayer", Version = "v1" });
            });

            services.AddAuthentication(options =>
                    {
                       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                       options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = Configuration["Jwt:Issuer"],
                         ValidAudience = Configuration["Jwt:Issuer"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                       };
                    });

            services.AddIdentity<User, IdentityRole<long>>().AddEntityFrameworkStores<AppDbContext>()
                                                            .AddDefaultTokenProviders();
            services.AddAuthorization();

            services.AddDbContext<AppDbContext>(options => options
                                                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                                                .UseLazyLoadingProxies());

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

            services.AddCors();
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EdProject.PresentationLayer v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();   
            });
        }
    }
   
}
