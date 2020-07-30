namespace CoinCoinsInsolites.DataAccess.Configuration
{
    using CoinCoinsInsolites.Entities;
    using Microsoft.EntityFrameworkCore;

    public class UserConfiguration : ConfigurationManagement<UserEntity>
    {
        public UserConfiguration(ModelBuilder modelBuilder)
           : base(modelBuilder)
        {
        }

        protected override void ProcessConstraint()
        {
            this.EntityConfiguration.HasKey(userEntity => userEntity.Id);
            this.EntityConfiguration.Property(userEntity => userEntity.Mail).IsRequired().HasColumnType("nvarchar(255)");
            this.EntityConfiguration.Property(userEntity => userEntity.Password).IsRequired().HasColumnType("nvarchar(255)");
            this.EntityConfiguration.Property(userEntity => userEntity.Pseudonym).IsRequired().HasColumnType("nvarchar(255)");
        }

        protected override void ProcessForeignKey()
        {
        }

        protected override void ProcessIndex()
        {
        }

        protected override void ProcessTable()
        {
            this.EntityConfiguration.ToTable("Users");
        }
    }
}