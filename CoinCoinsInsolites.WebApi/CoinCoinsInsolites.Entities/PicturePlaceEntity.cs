using System.ComponentModel.DataAnnotations;

namespace CoinCoinsInsolites.Entities
{
    public class PicturePlaceEntity : BaseEntity
    {
        public int Id { get; set; }

        public int IdPlace { get; set; }

        public PlaceEntity PlaceEntity { get; set; }

        [Encrypted]
        public double Latitude { get; set; }

        [Encrypted]
        public double Longitude { get; set; }

        [Encrypted]
        public string SvgLink { get; set; }
    }
}