namespace CoinCoinsInsolites.Entities
{
    using System;

    public class BaseEntity : IBaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}