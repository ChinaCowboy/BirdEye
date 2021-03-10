using System;
using BirdEyeDetector.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BirdEyeDetector.Areas.Identity.IdentityHostingStartup))]
namespace BirdEyeDetector.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BirdEyeDetectorContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BirdEyeDetectorContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<BirdEyeDetectorContext>();
            });
        }
    }
}