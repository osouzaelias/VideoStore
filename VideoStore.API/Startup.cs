using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VideoStore.API.AutoMapper;
using VideoStore.Domain.CustomerAgg;
using VideoStore.Domain.MovieAgg;
using VideoStore.Infrastructure;
using VideoStore.Infrastructure.Repositories;

namespace VideoStore.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddControllers();
            services.AddDbContext< UnitOfWork >();

            services.AddTransient< ICustomerRepository, CustomerRepository >();
            services.AddTransient< IMovieRepository, MovieRepository >();
            services.AddTransient< ICustomerService, CustomerService >();
            services.AddTransient< IMovieService, MovieService >();
            services.AddTransient< ICustomerFactory, CustomerFactory >();

            services.AddAutoMapper( typeof( VideoStoreProfile ) );

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Video Store API",
                    Description = "API para gerenciar o ciclo mais básico de uma locadora, " +
                                  "cadastrar um filme, cadastrar locador, locar um filme e devolver um filme.",
                    Contact = new OpenApiContact
                    {
                        Name = "Elias Souza",
                        Email = "osouzaelias@gmail.com",
                        Url = new Uri( "https://www.linkedin.com/in/elias-souza-7572a224/" )
                    }
                } );

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine( AppContext.BaseDirectory, xmlFile );
                c.IncludeXmlComments( xmlPath );
            } );
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint( "/swagger/v1/swagger.json", "v1" );
                c.RoutePrefix = string.Empty;
            } );


            app.UseAuthorization();
            app.UseEndpoints( endpoints => { endpoints.MapControllers(); } );
        }
    }
}