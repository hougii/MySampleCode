using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyRazorPage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            #region app.UseStaticFiles()��������
            //�M�w�w�]wwwroot�U��static file path��root���U�Gex:http://localhost/images/banner1.svg 
            //�Y�n�Ȼs�b�O���ؿ����U�Ghttp://bit.ly/2PBacxu
            //
            app.UseStaticFiles();

            //�N����ؿ�MyStaticFiles���U��/StaticFiles��m
#if false
             app.UseStaticFiles(new StaticFileOptions
             {
                 FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                 RequestPath = "/StaticFiles"
             });
#endif

            //�]�i�H�]�wStatic File Response��Header��T�Aex:�M�wcache�h������ http://bit.ly/2PEqHJ5
#if false
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });
#endif
            //�w�]wwwroot�O�L�{�Ҿ���A�Yasset�n���@�A�h���wwwroot���~�A�å�Action�h����  http://bit.ly/2PE7lUu
#if false
[Authorize]
            public IActionResult BannerImage()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(),
                                    "MyStaticFiles", "images", "banner1.svg");

            return PhysicalFile(file, "image/svg+xml");
        }
#endif
            #endregion


            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
