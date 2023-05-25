using VGChat.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VGChat.Data;
using VGChat.Areas.Identity.Data;
using VGChat.Hubs.VGChat.Hubs;

namespace VGChat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            var connectionString = builder.Configuration.GetConnectionString("VGChatAuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'VGChatAuthDbContextConnection' not found.");

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<VGChatAuthDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<VGChatUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<VGChatAuthDbContext>();

			builder.Services.AddSignalR();
			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod();
					});
			});

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                 builder.Configuration.GetConnectionString("DefaultConnection")
            ));

            builder.Services.AddRazorPages();


			var app = builder.Build();

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
			app.UseCors();

            app.UseAuthentication();;
			app.UseAuthorization();
			app.MapHub<ChatHub>("/chatHub");

            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.MapRazorPages();
			app.Run();
		}
	}
}