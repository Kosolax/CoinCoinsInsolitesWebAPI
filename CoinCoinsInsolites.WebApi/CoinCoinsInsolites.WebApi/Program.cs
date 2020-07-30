namespace CoinCoinsInsolites.WebApi
{
    using CoinCoinsInsolites.DataAccess;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;

    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            Seed(host);
            host.Run();
        }

        /// <summary>
        /// Allow us to create database, create table and play seed when needed
        /// </summary>
        /// <param name="host"></param>
        public static void Seed(IHost host)
        {
            using (IServiceScope serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                IConfiguration configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();
                bool isProduction = Convert.ToBoolean(configuration["IsProduction"]);
                byte[] iv = GetByteArray(configuration["IV"]);
                byte[] key = GetByteArray(configuration["Key"]);
                serviceScope.ServiceProvider.GetService<CoinCoinsContext>().SetIVAndKey(iv, key);
                serviceScope.ServiceProvider.GetService<CoinCoinsContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<CoinCoinsContext>().EnsureSeedData(isProduction).GetAwaiter().GetResult();
            }
        }

        private static byte[] GetByteArray(string str)
        {
            String[] arr = str.Split('-');
            byte[] array = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++) array[i] = Convert.ToByte(arr[i], 16);
            return array;
        }
    }
}