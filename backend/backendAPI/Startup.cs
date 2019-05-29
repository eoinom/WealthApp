using backendAPI.Mutations;
using backendAPI.Queries;
using backendAPI.Schema;
using backendAPI.Types;
using backendData;
using backendDataAccess.Repositories;
using backendDataAccess.Repositories.Contracts;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace backendAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    readonly string quasarOrigins = "_quasarOrigins";

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<backendDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

      services.AddCors(options =>
      {
          options.AddPolicy(quasarOrigins,
          builder =>
          {
            builder.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
          });
      });

            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<CountryType>();
            services.AddSingleton<CurrencyType>();
            services.AddSingleton<UserQuery>();
            services.AddSingleton<UserMutation>();            
            services.AddSingleton<UserType>();
            services.AddSingleton<UserInputType>();
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new backendSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, backendDbContext db)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseCors(quasarOrigins);

      app.UseHttpsRedirection();
      //app.UseStaticFiles();

      app.UseGraphiQl();
      app.UseMvc();
      db.EnsureSeedData();
    }
  }
}
