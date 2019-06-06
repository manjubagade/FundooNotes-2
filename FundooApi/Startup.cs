// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace FundooApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer;
    using BusinessLayer.Interfaces;
    using BusinessLayer.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Interface;
    using RepositoryLayer.Services;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureService Method
        /// </summary>
        /// <param name="services">the services.</param>
        //// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ////Inject Appsettings
            services.Configure<ApplicationSettings>(this.Configuration.GetSection("ApplicationSettings"));

            services.AddDbContext<RegistrationControl>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("IdentityConnections"), b => b.MigrationsAssembly("RepositoryLayer")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<RegistrationControl>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fundoo Notes", Version = "v1" });
            });

            //// Swagger code for file upload
            services.ConfigureSwaggerGen(options =>
            {
                //// Register File Upload Operation Filter
                options.OperationFilter<FileUploadOperation>();
            });
            ////Password Authorization
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            });

          //  services.Configure<EmailSender>(this.Configuration.GetSection("EmailSender"));
            services.AddTransient<IApplicationUserOperations, ApplicationUserOperations>();
            services.AddTransient<IUserDataOperations, UserDataOperations>();
            //services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<INotes, NotesCreation>();
            services.AddTransient<IRepositoryNotes, CreateNotes>();
            services.AddTransient<IEmailSender, EmailSenders>();
            services.AddTransient<ILabel, BusinessLabel>();
            services.AddTransient<IRepositoryLabel, LabelHandler>();
            services.AddTransient<IRepositoryCollaborators, CollaboratorsHandler>();
            services.AddTransient<ICollborators, CollaboratorsBusinessLayes>();
         

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost";
                option.InstanceName = "master";
            });

            var key = Encoding.UTF8.GetBytes(this.Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        /// <summary>
        /// Configure method
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="env">The env.</param>
        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //// Enables use of swagger
            app.UseSwagger();

            //// Enable generated json document and swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fundoo");
            });
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            builder.WithOrigins(this.Configuration["ApplicationSettings:JWT_Secret"].ToString())
            .AllowAnyHeader()
            .AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            ////calling use authentication function
            app.UseAuthentication();
            app.UseMvc();
        }

        public class SwaggerSecurityRequirementsDocumentFilter : IDocumentFilter
        {
            /// <summary>
            /// Applies the specified document.
            /// </summary>
            /// <param name="document">The document.</param>
            /// <param name="context">The context.</param>
            public void Apply(SwaggerDocument document, DocumentFilterContext context)
            {
                document.Security = new List<System.Collections.Generic.IDictionary<string, IEnumerable<string>>>()
                   {
                      new Dictionary<string, IEnumerable<string>>()
                      {
                          { "Bearer", new string[] { } },
                          { "Basic", new string[] { } }
                      }
                   };
            }
        }
        public class FileUploadOperation : IOperationFilter
   {
       /// <summary>
       /// Applies the specified operation.
       /// </summary>
       /// <param name="operation">The operation.</param>
       /// <param name="context">The context.</param>
       public void Apply(Operation operation, OperationFilterContext context)
       {
           if (operation.Parameters == null)
           {
               operation.Parameters = new List<IParameter>();
           }
           //// adding autherization header for api's
           operation.Parameters.Add(new NonBodyParameter
           {
               Name = "Authorization",
               In = "header",
               Type = "string",
               Required = true // set to false if this is optional
           });}}

    }
}