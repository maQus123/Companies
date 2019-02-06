namespace Companies {

    using Companies.Persistence;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
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
            services.Configure<RouteOptions>(options => { options.AppendTrailingSlash = true; options.LowercaseUrls = true; });
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            return;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "listCompanies",
                    template: "",
                    defaults: new { controller = "Company", action = "List" });
                routes.MapRoute(
                    name: "createCompany",
                    template: "create-company",
                    defaults: new { controller = "Company", action = "Create" });
                routes.MapRoute(
                    name: "editCompany",
                    template: "edit-company/{id:int}",
                    defaults: new { controller = "Company", action = "Edit" });
                routes.MapRoute(
                    name: "deleteCompany",
                    template: "delete-company/{id:int}",
                    defaults: new { controller = "Company", action = "Delete" });
            });
            return;
        }

    }

}