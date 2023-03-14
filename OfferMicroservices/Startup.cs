using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OfferMicroservice.Models;
using OfferMicroservice.Repository;
using OfferMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferMicroservice
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
            services.AddDbContext<OfferContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IOfferRepo, OfferRepo>();
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OfferMicroservice", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                { Description = "This site uses Bearer token and you have to pass" + 
                "it as Bearer<<space>>Token", Name = "Authorization", In = ParameterLocation.Header, 
                    Type = SecuritySchemeType.ApiKey, Scheme = "Bearer" });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() { { new OpenApiSecurityScheme 
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                   Scheme = "oauth2", Name = "Bearer", In = ParameterLocation.Header }, new List<string>() } });
          });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .         AddJwtBearer(options =>
               {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
             });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OfferMicroservice v1"));
            }
             app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
