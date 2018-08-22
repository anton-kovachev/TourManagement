using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using TourManagement.API.Services;
using TourManagement.API.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace TourManagement.API {
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //Service added for MVC
            services.AddAuthorization(options => { 
                options.AddPolicy("IsAdmin", policyBuilder => {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireClaim(JwtClaimTypes.Role, new string[] {"Admin"});
                });

                options.AddPolicy("IsTourManager", policyBuilder => { 
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new RoleAuthorizationRequirement("Admin"));
                });

            });

            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options => {
                options.ApiName = "tourmanagementapi";
                //options.ApiSecret = "",
                options.Authority = "http://localhost:5010/";
                options.RequireHttpsMetadata = false;
            });

            services
            .AddMvc(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;

                var jsonOutputFormatters = setupAction.OutputFormatters.OfType<JsonOutputFormatter>()
                    .FirstOrDefault();

                jsonOutputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tour+json");
                jsonOutputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithestimatedprofits+json");
                jsonOutputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithestimatedprofitsandmanager+json");
                jsonOutputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithshows+json");
                jsonOutputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithestimatedprofitsandshows+json");
                jsonOutputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithestimatedprofitsandmanagerandshows+json");
                jsonOutputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.showcollection+json");

                var jsonInputFormatters = setupAction.InputFormatters.OfType<JsonInputFormatter>()
                    .FirstOrDefault();

                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourforcreation+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithmanagerforcreation+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithshowsforcreation+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithmanagerandshowsforcreation+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.showcollectionforcreation+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourforupdate-json-patch+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithshowsforupdate-json-patch+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithestimatedprofitsforupdate-json-patch+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithestimatedprofitsandmanagerforupdate-json-patch+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/vnd.toursltd.tourwithestimatedprofitsandmanagerandshowsforupdate-json-patch+json");
                jsonInputFormatters.SupportedMediaTypes.Add("application/json-patch+json");
            })
            .AddJsonOptions( options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            
            services.AddCors( corsOptions => {
                corsOptions.AddPolicy("AllowAllOriginsHeadersAndMethods", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddScoped<IAuthorizationHandler, IsTourManagerRequirementHandler>();

            services.AddScoped<ITourManagementRepository, TourManagementRepository>();

            services.AddDbContext<TourManagementContext>(options => { 
                options.UseSqlite("DataSource=TourManagementDB", o => o.ExecutionStrategy(c => new ConnectionResiliencyExecutionStrategy(c)));
            });

            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                ILoggerFactory loggerFactory, IHttpContextAccessor context, IAuthorizationService authorizationService)
        {
            //logger.AddConsole(LogLevel.Information);
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }
            //Logger
            loggerFactory.AddConsole();
            app.UseCors("AllowAllOriginsHeadersAndMethods");
            
            //Use Static Files
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes => {   
                   routes.MapRoute("default",  
                                    "{controller}/{action}/{id?}");            
             });

            Mapper.Initialize( config => {
                config.CreateMap<Entities.Band, Dtos.Band>();
                config.CreateMap<Entities.Manager, Dtos.Manager>();
                config.CreateMap<Entities.Show, Dtos.Show>();

                config.CreateMap<Entities.Tour, Dtos.Tour>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name))
                    .ForMember(d => d.IsUserTourManager,
                        o => o.MapFrom(s => authorizationService.AuthorizeAsync(context.HttpContext.User, s.ManagerId, "IsTourManager").Result.Succeeded));

                config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfits>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfitsAndManager>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
            
                config.CreateMap<Entities.Tour, Dtos.TourWithShows>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfitsAndShows>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfitsAndManagerAndShows>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name))
                    .ForMember(d => d.Shows, o => o.MapFrom(s => s.Shows.OrderBy(show => show.Date)));

                config.CreateMap<Entities.Tour, Dtos.TourForUpdate>().ReverseMap();
                config.CreateMap<Entities.Tour, Dtos.TourWithShowsForUpdate>().ReverseMap();
                config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfitsAndManagerForUpdate>().ReverseMap();
                config.CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfitsAndManagerAndShowsForUpdate>().ReverseMap();

                config.CreateMap<Dtos.TourForCreation, Entities.Tour>();
                config.CreateMap<Dtos.TourWithManagerForCreation, Entities.Tour>();
                config.CreateMap<Dtos.TourWithShowsForCreation, Entities.Tour>();
                config.CreateMap<Dtos.TourWithManagerAndShowsForCreation, Entities.Tour>();

                config.CreateMap<Dtos.ShowForCreation, Entities.Show>();
                config.CreateMap<Dtos.ShowForUpdate, Entities.Show>();
            });
        }
    }
}