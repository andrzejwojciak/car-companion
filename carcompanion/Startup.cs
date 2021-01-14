using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using carcompanion.Data;
using carcompanion.Repositories;
using carcompanion.Repositories.Interfaces;
using carcompanion.Security;
using carcompanion.Services;
using carcompanion.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace carcompanion
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
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            
            var facebookAuthSettings = new FacebookAuthSettings();
            Configuration.Bind(nameof(facebookAuthSettings), facebookAuthSettings);

            services.AddSingleton(jwtSettings);
            services.AddSingleton(facebookAuthSettings);
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Database")));
            
            services.AddControllers();
                        
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                  {
                      x.SaveToken = true;
                      x.TokenValidationParameters = new TokenValidationParameters
                      {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                            ValidateIssuer = true,                      
                            ValidIssuer = jwtSettings.Issuer,
                            ValidateAudience = false,      
                            ValidateLifetime = true,
                            RequireExpirationTime = false,
                            ClockSkew = TimeSpan.Zero    
                      };
                  });            
                  
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });    
                  
             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "carcompanion-backend", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", 
                    new OpenApiSecurityScheme{
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http, 
                    Scheme = "bearer" 
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                { 
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", 
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });   

            services.AddAutoMapper(typeof(Startup));                
                
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRefreshtokenService, RefreshtokenService>();
            services.AddScoped<IShareCarService, ShareCarService>();
            services.AddScoped<IFacebookAuthService, FacebookAuthService>();
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<ISummaryService, SummaryService>();
            
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IUserCarRepository, UserCarRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();            
            services.AddTransient<IShareKeyRepository, ShareKeyRepository>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();          

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carcompanion API V1");
                c.RoutePrefix = string.Empty;
            });

            
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();
   
            app.UseAuthentication();
            app.UseAuthorization();         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
