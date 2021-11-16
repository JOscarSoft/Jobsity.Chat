using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Jobsity.Chat.Core.Models;
using Jobsity.Chat.App.Infrastructure;
using Jobsity.Chat.Core.Contracts;
using Jobsity.Chat.Core.Services;
using Jobsity.Chat.App.Services;
using Jobsity.Chat.Core.DBContext;

namespace Jobsity.Chat.App
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
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default"))
            );
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<UserChat>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddDataProtection()
                .PersistKeysToDbContext<AppDbContext>();

            services.AddSignalR();
            services.AddSingleton<ICommandService, CommandService>();
            services.AddScoped<IMessageService, DBMessageService>();
            services.AddScoped<IUserService, DBUserService>();
            services.AddSingleton<IUserProducer, UserBotQueueProducer>();
            services.AddHostedService<BotUsersQueueConsumer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatter");
            });
        }

    }
}
