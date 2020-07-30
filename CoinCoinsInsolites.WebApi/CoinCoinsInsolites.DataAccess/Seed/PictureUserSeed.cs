namespace CoinCoinsInsolites.DataAccess.Seed
{
    using CoinCoinsInsolites.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PictureUserSeed : IContextSeed
    {
        public PictureUserSeed(CoinCoinsContext context)
        {
            this.Context = context;
        }

        public CoinCoinsContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.PictureUsers.Any() && !isProduction)
            {
                DateTime createdDate = DateTime.UtcNow;
                List<PictureUserEntity> pictureUsers = new List<PictureUserEntity>
                {
                    new PictureUserEntity
                    {
                        Id = 1,
                        CreatedDate = createdDate,
                        Latitude = 47.764877,
                        Longitude = -1.974038,
                        SvgLink = "svg",
                        IdUser = 1,
                    },
                };

                await this.Context.PictureUsers.AddRangeAsync(pictureUsers);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}