using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVC_Start.DataAccess;
using Microsoft.Extensions.Hosting;


namespace MVC_Start
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:IEXTrading:ConnectionString"]));
        // Added from MVC template
        services.AddMvc();
    }

    // this is the version from the MVC template. Compare to above for changes
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
         if (env.IsDevelopment())
         {
           app.UseBrowserLink();
           app.UseDeveloperExceptionPage();
         }
         else
         {
           app.UseExceptionHandler("/Home/Error");
         }

    //This ensures that the database and tables are created as per the Models.
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }

    app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
