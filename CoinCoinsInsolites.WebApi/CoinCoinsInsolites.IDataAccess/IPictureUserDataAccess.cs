namespace CoinCoinsInsolites.IDataAccess
{
    using CoinCoinsInsolites.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPictureUserDataAccess : IBaseDataAccess<PictureUserEntity>
    {
        Task CreateFromList(List<PictureUserEntity> listPictureUser);

        void DeleteAllFromList(List<PictureUserEntity> list);

        Task DeleteAllFromUserId(int userId);

        Task<List<PictureUserEntity>> FindAllPictureByUserId(int userId);

        Task<Dictionary<int, PictureUserEntity>> FindAllPictureByUserIdToDictionary(int id);

        Task UpdateFromList(List<PictureUserEntity> listPictureUser);

        Task<List<PictureUserEntity>> FindAllPictureByListUserId(List<int> listId);
    }
}