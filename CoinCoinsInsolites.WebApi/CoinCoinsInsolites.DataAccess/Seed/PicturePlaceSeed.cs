namespace CoinCoinsInsolites.DataAccess.Seed
{
    using CoinCoinsInsolites.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PicturePlaceSeed : IContextSeed
    {
        public PicturePlaceSeed(CoinCoinsContext context)
        {
            this.Context = context;
        }

        public CoinCoinsContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.PicturePlaces.Any() && !isProduction)
            {
                DateTime createdDate = DateTime.UtcNow;
                List<PicturePlaceEntity> picturePlaces = new List<PicturePlaceEntity>
                {
                    new PicturePlaceEntity
                    {
                        Id = 1,
                        CreatedDate = createdDate,
                        Latitude = 47.764877,
                        Longitude = -1.974038,
                        SvgLink = "svg",
                        IdPlace = 1,
                    },
                    new PicturePlaceEntity
                    {
                        Id = 2,
                        CreatedDate = createdDate,
                        Latitude = 48.667770,
                        Longitude = -3.858109,
                        SvgLink = "svg",
                        IdPlace = 2,
                    },
                    new PicturePlaceEntity
                    {
                        Id = 3,
                        CreatedDate = createdDate,
                        Latitude = 45.072347,
                        Longitude = 45.072347,
                        SvgLink = "svg",
                        IdPlace = 3,
                    },
                };

                await this.Context.PicturePlaces.AddRangeAsync(picturePlaces);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}