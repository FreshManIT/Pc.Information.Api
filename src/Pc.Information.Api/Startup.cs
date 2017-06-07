using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Pc.Information.Api.MiddleWares;
using Pc.Information.Interface.IUserInfoBll;
using Pc.Information.Business.UserInfoBll;
using Pc.Information.Interface.ILogHistoryBll;
using Pc.Information.Business.LogHistoryBll;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.Swagger.Model;
using Pc.Information.Interface.IQuestionBll;
using Pc.Information.Business.QuestionBll;
using Pc.Information.Interface.IQuestionReplyBll;
using Pc.Information.Business.QuestionReplyBll;
using FreshCommonUtility.CoreModel;

namespace Pc.Information.Api
{
    /// <summary>
    /// start up class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// construct function
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// interface server
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc(opt =>
            {
                opt.UseControllRoutePrefix(new RouteAttribute("api/"));
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Add options
            services.AddOptions();
            services.Configure<AppSettingsModel>(Configuration.GetSection("AppSettings"));
            //Add transiend
            services.AddTransient<IUserInfoBll, UserInfoBll>();
            //Add Error log server
            services.AddTransient<IErrorLogBll, ErrorLogBll>();
            //Add ChatInfo history server
            services.AddTransient<IInformationHistoryBll, InformationHistoryBll>();
            //Add Question server info
            services.AddTransient<IQuestionBll, QuestionBll>();
            //Add Question Reply server info
            services.AddTransient<IQuestionReplyBll, QuestionReplyBll>();
            services.AddSwaggerGen();
            //Add the detail information for the API.http://localhost:port/swagger/ui/index.html
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "PcInformation.Api",
                    Description = "A simple example ASP.NET Core PcInformation.Api",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "FreshMan", Email = "qinbocai@sina.cn", Url = "https://github.com/Yinghuochongxiaoq" },
                    //License = new License { Name = "Use under LICX", Url = "http://url.com" }
                });

                //Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                System.Console.WriteLine(basePath);
                //Set the comments path for the swagger json and ui.
                options.IncludeXmlComments(basePath + "\\Pc.Information.Api.xml");
                options.IncludeXmlComments(basePath + "\\Pc.Information.Business.xml");
                options.IncludeXmlComments(basePath + "\\Pc.Information.CoreModel.xml");
                options.IncludeXmlComments(basePath + "\\Pc.Information.DataAccess.xml");
                options.IncludeXmlComments(basePath + "\\Pc.Information.Interface.xml");
                options.IncludeXmlComments(basePath + "\\Pc.Information.Model.xml");
                options.IncludeXmlComments(basePath + "\\Pc.Information.Utility.xml");
                options.DescribeAllEnumsAsStrings();
            });

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();
            //异常处理中间件
            app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));
            app.UseSwagger();
            app.UseSwaggerUi();
            app.UseMvc();
        }
    }
}
