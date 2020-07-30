namespace CoinCoinsInsolites.DataAccess
{
    using CoinCoinsInsolites.Entities;
    using CoinCoinsInsolites.IDataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaceDataAccess : BaseDataAccess<PlaceEntity>, IPlaceDataAccess
    {
        public PlaceDataAccess(CoinCoinsContext context)
            : base(context)
        {
        }

        public async Task<List<PlaceEntity>> FindMostRecentPlaceEntity(int count)
        {
            return await this.Context.Places
                .Select(placeEntity => new PlaceEntity
                {
                    CreatedDate = placeEntity.CreatedDate,
                    UpdatedDate = placeEntity.UpdatedDate,
                    Description = placeEntity.Description,
                    Id = placeEntity.Id,
                    Latitude = placeEntity.Latitude,
                    Longitude = placeEntity.Longitude,
                    Title = placeEntity.Title
                })
                .OrderByDescending(placeEntity => placeEntity.UpdatedDate.HasValue ? placeEntity.UpdatedDate : placeEntity.CreatedDate)
                .Take(count)
                .ToListAsync();
        }
    }
}