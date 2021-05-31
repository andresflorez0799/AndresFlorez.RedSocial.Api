using AndresFlorez.RedSocial.Api.Filter;
using AndresFlorez.RedSocial.Api.Hubs;
using AndresFlorez.RedSocial.Api.Models;
using AndresFlorez.RedSocial.Api.Services;
using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Logica.Implementacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AndresFlorez.RedSocial.Api
{
    public class Startup
    {
        readonly string MyCors = "Cors_Magico";
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //opcion 1
            _ = services.AddCors(options =>
            {
                options.AddPolicy(name: MyCors,
                      builder =>
                      {
                          builder.WithOrigins(new string[] {
                                "http://localhost:4200",                                
                                "https://localhost:44398",
                                "http://localhost:9001"                                 
                          }); //aqui puede ir la ip
                            builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                          builder.AllowCredentials();
                      });
            });

            services.AddCors();

            services.AddControllers();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = true;
                options.Filters.Add(typeof(ValidatorActionFilter));
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AndresFlorez.RedSocial.Api", Version = "v1" });
            //});

            //Documentation Api by Swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                swagger.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Red Social Api - v1",
                        Version = "1.0",
                        Contact = new OpenApiContact() { Email = "andresflorez0799@gmail.com", Name = "Mairon Andres Florez" },
                        Description = "Api para la gestion de proyecto web Red Social.",
                        License = new OpenApiLicense() { Name = "Proyecto de Prueba Tecnica" }
                    });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            //injection of User Login Api
            services.AddScoped<IUserAuth, UserAuth>();

            //injection controller api
            services.AddScoped<IUsuarioBl, UsuarioBl>();
            services.AddScoped<IPublicacionBl, PublicacionBl>();

            //Call App Setting from config file dependency Inyect
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //jwt configuration
            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.JWTSecret);
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(d =>
            {
                d.RequireHttpsMetadata = false;
                d.SaveToken = true;
                d.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
                //... Para validar las conexiones de chat
                d.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if ((context.Request.Path.Value.StartsWith("/chathub"))
                            && context.Request.Query.TryGetValue("token", out Microsoft.Extensions.Primitives.StringValues token)
                        )
                        {
                            context.Token = token;
                        }
                        return System.Threading.Tasks.Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var te = context.Exception;
                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });

            //SignalR
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(5);
                hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AndresFlorez.RedSocial.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options =>
            {
                options.WithOrigins(new string[] {
                    "http://localhost:4200",                    
                    "https://localhost:44398",
                    "http://localhost:9001"                     
                });
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowCredentials();
            });

            app.UseAuthentication();//jwt necesary

            app.UseAuthorization();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub", options =>
                {
                    options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
                }).RequireCors(MyCors);
            });
        }
    }
}
