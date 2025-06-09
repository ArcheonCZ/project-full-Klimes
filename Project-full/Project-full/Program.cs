using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using Project_full.Data;
using Project_full.Models;
using Project_full.Utils;

namespace Project_full
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            //builder.Services.AddDefaultIdentity<Osoba>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			//builder.Services.AddAuthentication(); //přidá se automaticky
			builder.Services.AddAuthorization();
			builder.Services.AddSingleton<IEmailSender, NullEmailSender>();


            builder.Services.AddIdentity<Osoba, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages(options =>
			{
				options.Conventions.AddAreaPageRoute(
					areaName: "Identity",
					pageName: "/Account/Login",
					route: "Account/Login");

				options.Conventions.AddAreaPageRoute(
					areaName: "Identity",
					pageName: "/Account/Register",
					route: "Account/Register");

				options.Conventions.AddAreaPageRoute(
					areaName: "Identity",
					pageName: "/Account/Manage/Index",
					route: "UserManagement/Details");
			});

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint(); //zobrazení chyb v development módu
				app.UseDeveloperExceptionPage();
			}
            else
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
			app.MapRazorPages();

			

			using (IServiceScope scope = app.Services.CreateScope())
			{
				RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				UserManager<Osoba> userManager = scope.ServiceProvider.GetRequiredService<UserManager<Osoba>>();
				Osoba? defaultAdminUser = await userManager.FindByEmailAsync("admin@admin.cz");

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

				if (defaultAdminUser is not null && !await userManager.IsInRoleAsync(defaultAdminUser, UserRoles.Admin))
					await userManager.AddToRoleAsync(defaultAdminUser, UserRoles.Admin);
			}

			app.Run();
        }
    }
}
