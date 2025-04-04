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

                    builder.WithOrigins("https://spa.eoinom.now.sh")
                      .AllowAnyHeader()
                      .AllowAnyMethod();

                    builder.WithOrigins("https://wealthapp.io")
                      .AllowAnyHeader()
                      .AllowAnyMethod();

                    builder.WithOrigins("https://www.wealthapp.io")
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                });
            });
                        
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountValueRepository, AccountValueRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();
            services.AddTransient<ILoanValueRepository, LoanValueRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<Query>();
            services.AddSingleton<Mutation>();

            services.AddSingleton<AccountType>();
            services.AddSingleton<AccountInputType>();
            services.AddSingleton<AccountQuery>();
            services.AddSingleton<AccountMutation>();

            services.AddSingleton<AccountValueType>();
            services.AddSingleton<AccountValueInputType>();
            services.AddSingleton<AccountValueQuery>();
            services.AddSingleton<AccountValueMutation>();            

            services.AddSingleton<CountryType>();
            services.AddSingleton<CountryQuery>();

            services.AddSingleton<CurrencyType>();

            services.AddSingleton<LoanType>();
            services.AddSingleton<LoanInputType>();
            services.AddSingleton<LoanQuery>();
            services.AddSingleton<LoanMutation>();

            services.AddSingleton<LoanValueType>();
            services.AddSingleton<LoanValueInputType>();
            services.AddSingleton<LoanValueQuery>();
            services.AddSingleton<LoanValueMutation>();

            services.AddSingleton<UserType>();
            services.AddSingleton<UserInputType>();
            services.AddSingleton<UserQuery>();
            services.AddSingleton<UserMutation>();

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

            db.EnsureSeedData();
            app.UseGraphiQl();
            app.UseMvc();            
        }
    }
}
