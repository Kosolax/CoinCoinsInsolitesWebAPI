namespace CoinCoinsInsolites.BusinessObject
{
    using CoinCoinsInsolites.BusinessObject.Validation;
    using CoinCoinsInsolites.Entities;

    public class PictureUser : BaseBusinessObject<PictureUserEntity>
    {
        public PictureUser()
        {
            this.ValidationService = new PictureUserValidation();
        }

        public PictureUser(PictureUserEntity entity)
            : base(entity)
        {
            this.ValidationService = new PictureUserValidation();
            this.Id = entity.Id;
            this.Latitude = entity.Latitude;
            this.Longitude = entity.Longitude;
            this.SvgLink = entity.SvgLink;
            this.IdUser = entity.IdUser;
            this.User = default;
        }

        public int Id { get; set; }

        public int IdUser { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string SvgLink { get; set; }

        public User User { get; set; }

        public override PictureUserEntity CreateEntity()
        {
            return new PictureUserEntity
            {
                Id = this.Id,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                SvgLink = this.SvgLink,
                IdUser = this.IdUser,
            };
        }
    }
}