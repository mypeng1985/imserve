using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ImWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ImHelper.Initialization(new ImClientOptions
            {
                Redis = new CSRedis.CSRedisClient("118.25.209.177:26379,poolsize=5"),
                Servers = new[] { "118.25.209.177:6000" , "118.25.209.177:6001", "118.25.209.177:6002" }
            });

            ImHelper.EventBus(
                              t => {

                                 var onlineIds = ImHelper.GetClientListByOnline();
                                  ImHelper.SendMessage(t.clientId, onlineIds, $"我来啦");
                                  Console.WriteLine(t.clientId + "上线了");
                              },
                              t => Console.WriteLine(t.clientId + "下线了"));

            JsonConvert.DefaultSettings = () =>
            {
                var st = new JsonSerializerSettings();
                st.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                st.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                st.ContractResolver = new CamelCasePropertyNamesContractResolver();
                st.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
                return st;
            };
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


            services.AddMvc()        
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
