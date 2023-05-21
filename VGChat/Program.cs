using VGChat.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VGChat.Data;
using VGChat.Areas.Identity.Data;

namespace VGChat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
               var connectionString = builder.Configuration.GetConnectionString("VGChatAuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'VGChatAuthDbContextConnection' not found.");

                           builder.Services.AddDbContext<VGChatAuthDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<VGChatUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<VGChatAuthDbContext>();


			// Add services to the container.
			builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
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