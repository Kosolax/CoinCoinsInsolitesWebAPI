namespace CoinCoinsInsolites.DataAccess.Configuration
{
    using CoinCoinsInsolites.Entities;
    using Microsoft.EntityFrameworkCore;

    public class PicturePlaceConfiguration : ConfigurationManagement<PicturePlaceEntity>
    {
        public PicturePlaceConfiguration(ModelBuilder modelBuilder)
           : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(userEntity => userEntity.Id);
            this.EntityConfiguration.Property(userEntity => userEntity.SvgLink).IsRequired().HasColumnType("MEDIUMTEXT");
            this.EntityConfiguration.Property(userEntity => userEntity.Latitude).IsRequired().HasColumnType("double");
            this.EntityConfiguration.Property(userEntity => userEntity.Longitude).IsRequired().HasColumnType("double");
        }

        protected override void ProcessForeignKey()
        {
            this.EntityConfiguration.HasOne(x => x.PlaceEntity).WithMany().IsRequired().HasForeignKey(x => x.IdPlace).OnDelete(DeleteBehavior.Restrict);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("PicturePlaces");
        }
    }
}