using System.Globalization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingHub.BLL.Mapper;
using Hangfire;
using ShoppingHub.BLL.Service.Implementaion;
using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.BLL.Services.Implementation;
using ShoppingHub.DAL.DataBase;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using ShoppingHub.DAL.Repository.Implementation;
using ShoppingHub.PL.Language;
using ShoppingHub.Serviese;

namespace ShoppingHub.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie  (CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Account/     Login");
                    options.AccessDeniedPath = new PathString("/    Account/Login");
                });


            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<shoppingHubDbContext>()
            .AddDefaultTokenProviders() ;

            var configuration = builder.Configuration;
            // Add services to the container.
            builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization(options =>
              {
                  options.DataAnnotationLocalizerProvider = (type,        factory)    =>
                      factory.Create(typeof(sharedResources));
              });
            var connectionString = builder.Configuration.GetConnectionString("Connection");

            builder.Services.AddDbContext<shoppingHubDbContext>(options =>
            options.UseSqlServer(connectionString));
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));


            builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            builder.Services.AddHangfireServer();
            builder.Services.AddScoped<IuserRepo, UserRepo>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICartItemRepo, CartItemRepo>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddTransient<IEmailSender, EmailConfirmeServer>();
            var app = builder.Build();
           
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roleNames = { "Admin", "User" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.UseHangfireDashboard("/hangfire");

            app.Run();
        }
    }
}
