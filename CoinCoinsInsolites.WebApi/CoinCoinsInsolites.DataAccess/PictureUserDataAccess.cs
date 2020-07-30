namespace CoinCoinsInsolites.DataAccess
{
    using CoinCoinsInsolites.Entities;
    using CoinCoinsInsolites.IDataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading.Tasks;

    public class PictureUserDataAccess : BaseDataAccess<PictureUserEntity>, IPictureUserDataAccess
    {
        public PictureUserDataAccess(CoinCoinsContext context)
            : base(context)
        {
        }

        public async Task CreateFromList(List<PictureUserEntity> listPictureUser)
        {
            await this.Context.PictureUsers.AddRangeAsync(listPictureUser);
            await this.Context.SaveChangesAsync();
        }

        public void DeleteAllFromList(List<PictureUserEntity> list)
        {
            this.Context.PictureUsers.RemoveRange(list);
        }

        public async Task DeleteAllFromUserId(int userId)
        {
            List<PictureUserEntity> pictureUsers = await this.FindAllPictureByUserId(userId);
            this.Context.PictureUsers.RemoveRange(pictureUsers);
        }

        public async Task<List<PictureUserEntity>> FindAllPictureByListUserId(List<int> listId)
        {
            return await this.Context.PictureUsers
                .Select(pictureUserEntity => new PictureUserEntity
                {
                    CreatedDate = pictureUserEntity.CreatedDate,
                    Id = pictureUserEntity.Id,
                    IdUser = pictureUserEntity.IdUser,
                    Latitude = pictureUserEntity.Latitude,
                    Longitude = pictureUserEntity.Longitude,
                    SvgLink = pictureUserEntity.SvgLink,
                    UpdatedDate = pictureUserEntity.UpdatedDate
                })
                .Where(pictureUserEntity => listId.Contains(pictureUserEntity.IdUser))
                .ToListAsync();
        }

        public async Task<List<PictureUserEntity>> FindAllPictureByUserId(int userId)
        {
            return await this.Context.PictureUsers
                .Select(pictureUserEntity => new PictureUserEntity
                {
                    CreatedDate = pictureUserEntity.CreatedDate,
                    Id = pictureUserEntity.Id,
                    IdUser = pictureUserEntity.IdUser,
                    Latitude = pictureUserEntity.Latitude,
                    Longitude = pictureUserEntity.Longitude,
                    SvgLink = pictureUserEntity.SvgLink,
                    UpdatedDate = pictureUserEntity.UpdatedDate
                })
                .Where(pictureUserEntity => pictureUserEntity.IdUser == userId)
                .ToListAsync();
        }

        public async Task<Dictionary<int, PictureUserEntity>> FindAllPictureByUserIdToDictionary(int id)
        {
            return await this.Context.PictureUsers
                .Select(pictureUserEntity => new PictureUserEntity
                {
                    CreatedDate = pictureUserEntity.CreatedDate,
                    Id = pictureUserEntity.Id,
                    IdUser = pictureUserEntity.IdUser,
                    Latitude = pictureUserEntity.Latitude,
                    Longitude = pictureUserEntity.Longitude,
                    SvgLink = pictureUserEntity.SvgLink,
                    UpdatedDate = pictureUserEntity.UpdatedDate
                })
                .Where(pictureUserEntity => pictureUserEntity.IdUser == id)
                .ToDictionaryAsync(pictureUserEntity => pictureUserEntity.Id);
        }

        public async Task UpdateFromList(List<PictureUserEntity> listPictureUser)
        {
            this.Context.PictureUsers.UpdateRange(listPictureUser);
            await this.Context.SaveChangesAsync();
        }
    }
}