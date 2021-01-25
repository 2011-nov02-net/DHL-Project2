using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Project2.DataModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Project2.Domain.Interfaces;
using Project2.DataModel.Repositories;
using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore.Query;
using Swashbuckle.AspNetCore.Swagger;

namespace Project2.Api
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }
        /// <summary>
        /// This method gets called by the runtime. 
        /// Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DHLProject2SchoolContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Project2connection")));

            services.AddTransient<IBuildingRepository, BuildingRepository>();
            
            services.AddTransient<IRepositoryAsync<User>, Repository<User>>(serviceProvider =>
                new Repository<User>(
                    serviceProvider.GetService<DHLProject2SchoolContext>(),
                    context => context.Users,
                    users => users.Include(x => x.PermissionNavigation)
                )
            );
            services.AddTransient<IRepositoryAsync<Course>, Repository<Course>>(serviceProvider =>
                new Repository<Course>(
                    serviceProvider.GetService<DHLProject2SchoolContext>(),
                    context => context.Courses,
                    users => users.Include(x => x.Enrollments)
                        .ThenInclude(x => x.GradeNavigation)
                        .Include(x => x.Waitlists)
                        .Include(x => x.Instructors)
                )
            );
            services.AddTransient<IRepositoryAsync<Department>, Repository<Department>>(serviceProvider =>
                new Repository<Department>(
                    serviceProvider.GetService<DHLProject2SchoolContext>(),
                    context => context.Departments,
                    departments => departments.Include(x => x.Dean)
                )
            );
            services.AddTransient<IRepositoryAsync<Building>, Repository<Building>>(serviceProvider =>
                new Repository<Building>(
                    serviceProvider.GetService<DHLProject2SchoolContext>(),
                    context => context.Buildings,
                    departments => departments.Include(x => x.Rooms)
                )
            );
            services.AddTransient<IRepositoryAsync<Room>, Repository<Room>>(serviceProvider =>
                new Repository<Room>(
                    serviceProvider.GetService<DHLProject2SchoolContext>(),
                    context => context.Rooms,
                    departments => departments.Include(x => x.Reservations)
                )
            );
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200",
                                            "https://dhl-project2-site.azurewebsites.net")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-2875280.okta.com/oauth2/default";
                    options.Audience = "api://default";
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fakebook.ProfileRestApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints => endpoints.MapControllers() );
        }
    }
}
