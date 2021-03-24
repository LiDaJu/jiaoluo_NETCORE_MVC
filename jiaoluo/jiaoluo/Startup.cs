using jiaoluo.BLL;
using jiaoluo.IBLL;
using jiaoluo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jiaoluo
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //AddDbContextPool比AddDbContext多一个数据库连接池，如果当前的连接池可以使用那就会直接使用不去创建；AddDbContextPool是core2.0以上的版本才有
            services.AddDbContextPool<AppDBContext>(
                optionsAction: options => options.UseSqlServer(_configuration.GetConnectionString("StudentDBConnection"))
            );

            services.AddMvc().AddXmlSerializerFormatters();//支持返回xml和json格式文件
            services.AddScoped<IStudentRepository, SQLSudentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            //异常中间件，在开发者模式进行调用，并尽可能的在其他中间件之前进行调用；
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
                developerExceptionPageOptions.SourceCodeLineCount = 1;//用于设置报错信息显示的行数；

                app.UseDeveloperExceptionPage();
            }



            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();

            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("demo.html");

            ////添加默认文件中间件（默认处理index.html;index.htm(默认);default.html;default.htm）
            //app.UseDefaultFiles(defaultFilesOptions);

            ////添加静态文件中间件
            app.UseStaticFiles();

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("demo.html");fileServerOptions

            //app.UseFileServer();//结合了app.UseDefaultFiles();app.UseStaticFiles();app.UseDirectoryBrowser();

            app.UseMvcWithDefaultRoute();//启用默认的mvc路由设置

            ////配置传统路由
            //app.UseMvc(routs =>
            //{
            //    routs.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});

            //app.UseMvc();
        }
    }
}
