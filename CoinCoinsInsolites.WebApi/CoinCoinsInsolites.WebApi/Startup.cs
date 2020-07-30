namespace CoinCoinsInsolites.WebApi
{
    using CoinCoinsInsolites.Business;
    using CoinCoinsInsolites.DataAccess;
    using CoinCoinsInsolites.IBusiness;
    using CoinCoinsInsolites.IDataAccess;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContextPool<CoinCoinsContext>(options => options
                .UseMySql(this.Configuration["ConnectionString"]));

            this.RegisterDataAccess(services);
            this.RegisterBusiness(services);
        }

        private void RegisterBusiness(IServiceCollection services)
        {
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IPlaceBusiness, PlaceBusiness>();
            services.AddScoped<IPictureUserBusiness, PictureUserBusiness>();
            services.AddScoped<IPicturePlaceBusiness, PicturePlaceBusiness>();
        }

        private void RegisterDataAccess(IServiceCollection services)
        {
            services.AddScoped<IUserDataAccess, UserDataAccess>();
            services.AddScoped<IPlaceDataAccess, PlaceDataAccess>();
            services.AddScoped<IPictureUserDataAccess, PictureUserDataAccess>();
            services.AddScoped<IPicturePlaceDataAccess, PicturePlaceDataAccess>();
        }
    }
}