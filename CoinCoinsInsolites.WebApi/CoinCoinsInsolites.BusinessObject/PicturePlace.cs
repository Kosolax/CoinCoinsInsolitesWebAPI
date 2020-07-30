namespace CoinCoinsInsolites.BusinessObject
{
    using CoinCoinsInsolites.BusinessObject.Validation;
    using CoinCoinsInsolites.Entities;

    public class PicturePlace : BaseBusinessObject<PicturePlaceEntity>
    {
        public PicturePlace()
        {
            this.ValidationService = new PicturePlaceValidation();
        }

        public PicturePlace(PicturePlaceEntity entity)
            : base(entity)
        {
            this.ValidationService = new PicturePlaceValidation();
            this.Id = entity.Id;
            this.Latitude = entity.Latitude;
            this.Longitude = entity.Longitude;
            this.SvgLink = entity.SvgLink;
            this.IdPlace = entity.IdPlace;
            this.Place = default;
        }

        public int Id { get; set; }

        public int IdPlace { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Place Place { get; set; }

        public string SvgLink { get; set; }

        public override PicturePlaceEntity CreateEntity()
        {
            return new PicturePlaceEntity
            {
                Id = this.Id,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                SvgLink = this.SvgLink,
                IdPlace = this.IdPlace,
            };
        }
    }
}