using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using RepositorySample.Framework;
using RepositorySample.Repositories.InMemory;
using RepositorySample.Repositories.MongoDB;
using Swashbuckle.AspNetCore.Swagger;

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

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Repository RESTful API",
                    Version = "v1",
                    Description = "仓储模式实现案例 RESTful API.",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Sunny Chen", Email = "daxnet@live.com", Url = "https://github.com/daxnet" },
                    License = new License { Name = "Apache License 2.0", Url = "https://www.apache.org/licenses/LICENSE-2.0" }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xml = Path.Combine(basePath, "RepositorySample.Service.xml");
                if (File.Exists(xml))
                {
                    c.IncludeXmlComments(xml);
                }
            });

            #region MongoDB 仓储实现
            var mongoHost = Configuration["mongo:host"];
            var mongoPort = Convert.ToInt32(Configuration["mongo:port"]);
            var mongoDatabase = Configuration["mongo:database"];

            var mongoRepositorySettings = new MongoRepositorySettings(mongoHost, mongoPort, mongoDatabase);

            services.AddSingleton(mongoRepositorySettings);
            services.AddTransient<IRepositoryContext, MongoRepositoryContext>();

            #endregion

            #region 内存数据库 仓储实现
            // services.AddTransient<IRepositoryContext, InMemoryRepositoryContext>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Repository RESTful API");
            });

            app.UseMvc();
        }
    }
}
