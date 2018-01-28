namespace Companies {

    using Companies.Persistence;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContextPool<DatabaseContext>(options => options.UseInMemoryDatabase("Database"));
            services.AddMvc();
            services.Configure<RouteOptions>(options => { options.AppendTrailingSlash = true; options.LowercaseUrls = true; });
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "listCompanies",
                    template: "",
                    defaults: new { controller = "Company", action = "List" });
                routes.MapRoute(
                    name: "createCompany",
                    template: "create",
                    defaults: new { controller = "Company", action = "Create" });
                routes.MapRoute(
                    name: "editCompany",
                    template: "edit/{id:int}",
                    defaults: new { controller = "Company", action = "Edit" });
            });
        }

    }

}