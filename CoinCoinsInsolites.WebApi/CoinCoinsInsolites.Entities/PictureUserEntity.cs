using System.ComponentModel.DataAnnotations;

namespace CoinCoinsInsolites.Entities
{
    public class PictureUserEntity : BaseEntity
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        [Encrypted]
        public double Latitude { get; set; }

        [Encrypted]
        public double Longitude { get; set; }

        [Encrypted]
        public string SvgLink { get; set; }

        public UserEntity UserEntity { get; set; }
    }
}