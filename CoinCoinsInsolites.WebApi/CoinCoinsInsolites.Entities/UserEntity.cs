namespace CoinCoinsInsolites.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class UserEntity : BaseEntity
    {
        public int Id { get; set; }

        [Encrypted]
        public string Mail { get; set; }

        [Encrypted]
        public string Password { get; set; }

        [Encrypted]
        public string Pseudonym { get; set; }
    }
}