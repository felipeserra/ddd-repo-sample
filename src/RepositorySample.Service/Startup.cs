using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RepositorySample.Framework;
using RepositorySample.Repositories.InMemory;
using RepositorySample.Repositories.MongoDB;

namespace RepositorySample.Service
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
            services.AddMvc();

            #region MongoDB 仓储实现
            //var mongoHost = Configuration["mongo:host"];
            //var mongoPort = Convert.ToInt32(Configuration["mongo:port"]);
            //var mongoDatabase = Configuration["mongo:database"];

            //var mongoRepositorySettings = new MongoRepositorySettings(mongoHost, mongoPort, mongoDatabase);

            //services.AddSingleton(mongoRepositorySettings);
            //services.AddTransient<IRepositoryContext, MongoRepositoryContext>();

            #endregion

            #region 内存数据库 仓储实现
            services.AddTransient<IRepositoryContext, InMemoryRepositoryContext>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
