namespace CoinCoinsInsolites.DataAccess
{
    using CoinCoinsInsolites.Entities;
    using CoinCoinsInsolites.IDataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PicturePlaceDataAccess : BaseDataAccess<PicturePlaceEntity>, IPicturePlaceDataAccess
    {
        public PicturePlaceDataAccess(CoinCoinsContext context)
            : base(context)
        {
        }

        public async Task CreateFromList(List<PicturePlaceEntity> listPicturePlace)
        {
            await this.Context.PicturePlaces.AddRangeAsync(listPicturePlace);
            await this.Context.SaveChangesAsync();
        }

        public void DeleteAllFromList(List<PicturePlaceEntity> list)
        {
            this.Context.PicturePlaces.RemoveRange(list);
        }

        public async Task DeleteAllFromPlaceId(int placeId)
        {
            List<PicturePlaceEntity> picturePlaces = await this.FindAllPictureByPlaceId(placeId);
            this.Context.PicturePlaces.RemoveRange(picturePlaces);
        }

        public async Task<List<PicturePlaceEntity>> FindAllPictureByListPlaceId(List<int> listId)
        {
            return await this.Context.PicturePlaces
                .Select(picturePlaceEntity => new PicturePlaceEntity
                {
                    CreatedDate = picturePlaceEntity.CreatedDate,
                    Id = picturePlaceEntity.Id,
                    IdPlace = picturePlaceEntity.IdPlace,
                    Latitude = picturePlaceEntity.Latitude,
                    Longitude = picturePlaceEntity.Longitude,
                    SvgLink = picturePlaceEntity.SvgLink,
                    UpdatedDate = picturePlaceEntity.UpdatedDate
                })
                .Where(picturePlaceEntity => listId.Contains(picturePlaceEntity.IdPlace))
                .ToListAsync();
        }

        public async Task<List<PicturePlaceEntity>> FindAllPictureByPlaceId(int placeId)
        {
            return await this.Context.PicturePlaces
                .Select(picturePlaceEntity => new PicturePlaceEntity
                {
                    CreatedDate = picturePlaceEntity.CreatedDate,
                    Id = picturePlaceEntity.Id,
                    IdPlace = picturePlaceEntity.IdPlace,
                    Latitude = picturePlaceEntity.Latitude,
                    Longitude = picturePlaceEntity.Longitude,
                    SvgLink = picturePlaceEntity.SvgLink,
                    UpdatedDate = picturePlaceEntity.UpdatedDate
                })
                .Where(picturePlaceEntity => picturePlaceEntity.IdPlace == placeId)
                .ToListAsync();
        }

        public async Task<Dictionary<int, PicturePlaceEntity>> FindAllPictureByPlaceIdToDictionary(int id)
        {
            return await this.Context.PicturePlaces
                .Select(picturePlaceEntity => new PicturePlaceEntity
                {
                    CreatedDate = picturePlaceEntity.CreatedDate,
                    Id = picturePlaceEntity.Id,
                    IdPlace = picturePlaceEntity.IdPlace,
                    Latitude = picturePlaceEntity.Latitude,
                    Longitude = picturePlaceEntity.Longitude,
                    SvgLink = picturePlaceEntity.SvgLink,
                    UpdatedDate = picturePlaceEntity.UpdatedDate
                })
                .Where(picturePlaceEntity => picturePlaceEntity.IdPlace == id)
                .ToDictionaryAsync(picturePlaceEntity => picturePlaceEntity.Id);
        }

        public async Task UpdateFromList(List<PicturePlaceEntity> listPicturePlace)
        {
            this.Context.PicturePlaces.UpdateRange(listPicturePlace);
            await this.Context.SaveChangesAsync();
        }
    }
}