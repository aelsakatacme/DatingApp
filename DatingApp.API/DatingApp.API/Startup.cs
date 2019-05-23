using System.Net;
using System.Text;
using AutoMapper;
using DatingApp.API.Context;
using DatingApp.API.Extensions;
using DatingApp.API.Helpers;
using DatingApp.API.IRepositories;
using DatingApp.API.Repositories;
using DatingApp.API.IServices;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace DatingApp.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hosting)
        {
            Configuration = configuration;
            Hosting.hosting = hosting;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DatingAppContext>(o =>
            {
                o.UseSqlServer(Configuration["ConnectionStrings:SqlServcerConnectionString"]);
                o.EnableSensitiveDataLogging(true);
            });
            services.AddCors();
            services.AddAutoMapper(o => { o.ValidateInlineMaps = false; });
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IDatingRepository, DatingRepository>();
            #region services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILookupService, LookupService>();
            #endregion

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:SignatureKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DatingAppContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async handler =>
                    {
                        handler.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = handler.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            handler.Response.AddApplicationError(error.Error.Message);
                            await handler.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                app.UseHsts();
            }

            context.SeedData(); //SEED

            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
