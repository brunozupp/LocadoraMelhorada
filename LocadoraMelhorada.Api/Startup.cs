using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using LocadoraMelhorada.Api.Middlewares;
using LocadoraMelhorada.Domain.Handlers;
using LocadoraMelhorada.Domain.Interfaces.Repositories;
using LocadoraMelhorada.Infra.Data.DataContexts;
using LocadoraMelhorada.Infra.Data.Repositories.MongoDb;
using LocadoraMelhorada.Infra.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraMelhorada.Api
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
            #region Settings

            SqlServerSettings sqlServerSettings = new SqlServerSettings();
            Configuration.GetSection("SqlServerSettings").Bind(sqlServerSettings);
            services.AddSingleton(sqlServerSettings);

            JwtSettings jwtSettings = new JwtSettings();
            Configuration.GetSection("JwtSettings").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            MongoDbSettings mongoDbSettings = new MongoDbSettings();
            Configuration.GetSection("MongoDbSettings").Bind(mongoDbSettings);
            services.AddSingleton(mongoDbSettings);

            #endregion

            #region DataContexts

            services.AddScoped<SqlServerDataContext>();
            services.AddScoped<MongoDbDataContext>();

            #endregion

            #region Repositories

            //services.AddScoped(typeof(IUsuarioRepository<>), typeof(UsuarioRepositorySqlServer<>));
            //services.AddScoped(typeof(IFilmeRepository<>), typeof(FilmeRepositorySqlServer<>));
            //services.AddScoped(typeof(IVotoRepository<>), typeof(VotoRepositorySqlServer<>));

            services.AddScoped(typeof(IUsuarioRepository<>), typeof(UsuarioRepositoryMongoDb<>));
            services.AddScoped(typeof(IFilmeRepository<>), typeof(FilmeRepositoryMongoDb<>));
            services.AddScoped(typeof(IVotoRepository<>), typeof(VotoRepositoryMongoDb<>));

            #endregion

            #region Handlers

            services.AddScoped<UsuarioHandler>();
            services.AddScoped<FilmeHandler>();
            services.AddScoped<VotoHandler>();
            services.AddScoped<AutenticacaoHandler>();

            #endregion

            services.AddElmah();

            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/log";
            });

            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = sqlServerSettings.ConnectionString;
            });

            services.AddControllers();

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocadoraMelhorada.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"'Bearer' {Token JWT}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
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

            #endregion

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocadoraMelhorada.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
