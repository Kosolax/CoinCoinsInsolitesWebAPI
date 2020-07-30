namespace CoinCoinsInsolites.BusinessObject
{
    using CoinCoinsInsolites.BusinessObject.Validation;
    using CoinCoinsInsolites.Entities;
    using System.Collections.Generic;

    public class Place : BaseBusinessObject<PlaceEntity>
    {
        public Place()
        {
            this.ListPicturePlace = new List<PicturePlace>();
            this.ValidationService = new PlaceValidation();
        }

        public Place(PlaceEntity entity)
            : base(entity)
        {
            this.ListPicturePlace = new List<PicturePlace>();
            this.ValidationService = new PlaceValidation();
            this.Id = entity.Id;
            this.Description = entity.Description;
            this.Latitude = entity.Latitude;
            this.Longitude = entity.Longitude;
            this.Title = entity.Title;
        }

        public string Description { get; set; }

        public int Id { get; set; }

        public double Latitude { get; set; }

        public List<PicturePlace> ListPicturePlace { get; set; }

        public double Longitude { get; set; }

        public string Title { get; set; }

        public override PlaceEntity CreateEntity()
        {
            return new PlaceEntity
            {
                Id = this.Id,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                Description = this.Description,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Title = this.Title,
            };
        }
    }
}