namespace CoinCoinsInsolites.DataAccess
{
    using CoinCoinsInsolites.DataAccess.Configuration;
    using CoinCoinsInsolites.DataAccess.Seed;
    using CoinCoinsInsolites.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.DataEncryption;
    using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CoinCoinsContext : DbContext
    {
        private byte[] encryptionIV;

        private byte[] encryptionKey;

        private IEncryptionProvider provider;

        public CoinCoinsContext(DbContextOptions<CoinCoinsContext> options)
            : base(options)
        {
        }

        public DbSet<PicturePlaceEntity> PicturePlaces { get; set; }

        public DbSet<PictureUserEntity> PictureUsers { get; set; }

        public DbSet<PlaceEntity> Places { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public void SetIVAndKey(byte[] iv, byte[] key)
        {
            this.encryptionIV = iv;
            this.encryptionKey = iv;
            this.provider = new AesProvider(this.encryptionKey, this.encryptionIV);
        }

        public async Task EnsureSeedData(bool isProduction)
        {
            ContextInitializer initializer = new ContextInitializer();
            await initializer.Seed(this, isProduction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this.provider);
            List<Action> listConfiguration = new List<Action>
            {
                new UserConfiguration(modelBuilder).Execute,
                new PlaceConfiguration(modelBuilder).Execute,
                new PictureUserConfiguration(modelBuilder).Execute,
                new PicturePlaceConfiguration(modelBuilder).Execute,
            };

            foreach (Action action in listConfiguration)
            {
                action.Invoke();
            }
        }
    }
}