using Application.Interfaces;
using Application.Mapper;
using Application.Services;
using Core.Repositories;
using Core.Configuration;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using System.Reflection;

namespace LibraryManagementApp
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

            services.Configure<ConnectionStringConfiguration>(Configuration.GetSection("ConnectionStringConfiguration"));
            var config = new ConnectionStringConfiguration();
            Configuration.GetSection("ConnectionStringConfiguration").Bind(config);
            
           services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer( 
                Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
          
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }); ;
            
            services.AddRazorPages();

            services.AddCors();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1",new OpenApiInfo {Title ="TestService",Version="v1" } );

                var xmlFilePath = Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                  c.IncludeXmlComments(xmlFilePath);
            });
           


            // Add Infrastructure Layer
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookTypeRepository, BookTypeRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            // Add Application Layer
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookTypeService, BookTypeService>();
            services.AddScoped<IAuthorService, AuthorService>();



       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()); 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
            });
           
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
