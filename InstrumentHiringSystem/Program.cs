using InstrumentHiringSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configurations = builder.Configuration;

var connectionString = configurations.GetConnectionString("InstrumentHiringSystemContextConnection") ;

services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
	.AddEntityFrameworkStores<ApplicationDbContext>();

services.AddControllersWithViews();

services.AddRazorPages();

services.ConfigureApplicationCookie(options =>
{
	options.LogoutPath = $"/Identity/Account/Logout";
	options.LoginPath = $"/Identity/Account/Login";
	options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{Area=User}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
