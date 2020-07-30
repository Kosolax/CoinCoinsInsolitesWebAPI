namespace CoinCoinsInsolites.DataAccess.Configuration
{
    using CoinCoinsInsolites.Entities;
    using Microsoft.EntityFrameworkCore;

    public class PictureUserConfiguration : ConfigurationManagement<PictureUserEntity>
    {
        public PictureUserConfiguration(ModelBuilder modelBuilder)
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
            this.EntityConfiguration.HasOne(x => x.UserEntity).WithMany().IsRequired().HasForeignKey(x => x.IdUser).OnDelete(DeleteBehavior.Restrict);
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("PictureUsers");
        }
    }
}