namespace CoinCoinsInsolites.DataAccess.Configuration
{
    using CoinCoinsInsolites.Entities;
    using Microsoft.EntityFrameworkCore;

    public class PlaceConfiguration : ConfigurationManagement<PlaceEntity>
    {
        public PlaceConfiguration(ModelBuilder modelBuilder)
           : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(userEntity => userEntity.Id);
            this.EntityConfiguration.Property(userEntity => userEntity.Description).IsRequired().HasColumnType("nvarchar(6000)");
            this.EntityConfiguration.Property(userEntity => userEntity.Latitude).IsRequired().HasColumnType("double");
            this.EntityConfiguration.Property(userEntity => userEntity.Longitude).IsRequired().HasColumnType("double");
            this.EntityConfiguration.Property(userEntity => userEntity.Title).IsRequired().HasColumnType("nvarchar(1000)");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Places");
        }
    }
}