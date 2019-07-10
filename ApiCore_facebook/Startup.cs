using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ApiCore_facebook.ClassController.log;
using ApiCore_facebook.Library;
using ApiCore_facebook.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiCore_facebook
{
    public class Startup
    {
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration; _logger = logger;
        }

        public IConfiguration Configuration { get; }
        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<db_facebook_vmContext>(opt =>
            //  opt.UseSqlServer(Configuration.GetConnectionString("MyDb")),ServiceLifetime.Scoped);
            
            #region Add Cros Website
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3002", "https://vietmyapp.com", "https://fb.vietmyapp.com").AllowAnyHeader().AllowAnyMethod());
              
            });
            #endregion
            //Caching từ bô nhớ server
            services.AddMemoryCache();

            #region Nén dữ liệu truyền tải
            //Nén ở cáp độ nào
            services.Configure<GzipCompressionProviderOptions>(options => {
                options.Level = CompressionLevel.Fastest;
                //Fastest:Thao tác nén phải hoàn thành càng nhanh càng tốt, ngay cả khi tệp kết quả không được nén tối ưu.
                //Optimal:  Hoạt động nén phải được nén tối ưu, ngay cả khi hoạt động mất nhiều thời gian hơn để hoàn thành.
            });
            services.AddResponseCompression(options => {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {
                    "application/xhtml+xml",
                    "application/atom+xml",
                    "image/svg+xml",
                });
            });
            #endregion

            #region Cấu hình teamplate Swagger api
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0", new Info
                { Title= "Core api", Description= "Bằng nguyễn" });
                c.SwaggerDoc("2.0", new Info
                { Title = "Core api", Description = "Bằng nguyễn" });




                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

           
            //services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            // Configure versions 
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Configure swagger
            services.AddSwaggerGen(options =>
            {
                // Specify two versions 
                options.SwaggerDoc("v1",
                    new Info()
                    {
                        Version = "v1",
                        Title = "Version 1",
                        Description = "v1 API Description" +
                        "</br> ka k a ",
                        //TermsOfService = "Terms of usage v1"
                    });

                options.SwaggerDoc("v2",
                    new Info()
                    {
                        Version = "v2",
                        Title = "v2 API",
                        Description = "v2 API Description",
                        TermsOfService = "Terms of usage v2"
                    });
                options.SwaggerDoc("v3",
                   new Info()
                   {
                       Version = "v3",
                       Title = "v3 API",
                       Description = "v3 API Description",
                       TermsOfService = "Terms of usage v3",
                       
                   });
                // This call remove version from parameter, without it we will have version as parameter 
                // for all endpoints in swagger UI
                options.OperationFilter<RemoveVersionFromParameter>();

                // This make replacement of v{version:apiVersion} to real version of corresponding swagger doc.
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // This on used to exclude endpoint mapped to not specified in swagger version.
                // In this particular example we exclude 'GET /api/v2/Values/otherget/three' endpoint,
                // because it was mapped to v3 with attribute: MapToApiVersion("3")
                options.DocInclusionPredicate((version, desc) =>
                {
                    //var versions = desc.ControllerAttributes()
                    //    .OfType<ApiVersionAttribute>()
                    //    .SelectMany(attr => attr.Versions);


                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);


                    var maps = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions).ToArray();


                    //var maps = desc.ActionAttributes()
                    //    .OfType<MapToApiVersionAttribute>()
                    //    .SelectMany(attr => attr.Versions)
                    //    .ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == version) && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == version));
                });
            });
            #endregion
            
            #region Xác thực token
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Add loging server
                services.AddSingleton<ILogRepository, LogRepository>();
                _logger.LogInformation("RUN APP API");
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //nếu môi trường là develop thì view ra chức năng description báo lỗi.
            if (env.IsDevelopment())
            {
                _logger.LogInformation("Moi truong code");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //Nén dữ liệu
            app.UseResponseCompression();
            //app.UseCors(builder =>builder.WithOrigins("http://localhost:3002"));
           
            //Add thư viện Swagger
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Version 1");
                c.SwaggerEndpoint($"/swagger/v3/swagger.json", $"v3");
                c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
                c.InjectStylesheet("/swagger/ui/css_custom.css");
                c.InjectJavascript("/swagger/ui/js_custom.js");
            });
            //Xác thự token trên app
            app.UseAuthentication();
            //Chạy https
            app.UseHttpsRedirection();
            var option = new RewriteOptions().AddRedirectToHttpsPermanent();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            app.UseMvc();
        }
    }
}
