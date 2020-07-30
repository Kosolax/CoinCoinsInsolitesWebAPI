namespace CoinCoinsInsolites.IDataAccess
{
    using CoinCoinsInsolites.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPicturePlaceDataAccess : IBaseDataAccess<PicturePlaceEntity>
    {
        Task CreateFromList(List<PicturePlaceEntity> listPicturePlace);

        void DeleteAllFromList(List<PicturePlaceEntity> list);

        Task DeleteAllFromPlaceId(int placeId);

        Task<List<PicturePlaceEntity>> FindAllPictureByListPlaceId(List<int> listId);

        Task<List<PicturePlaceEntity>> FindAllPictureByPlaceId(int placeId);

        Task<Dictionary<int, PicturePlaceEntity>> FindAllPictureByPlaceIdToDictionary(int id);

        Task UpdateFromList(List<PicturePlaceEntity> listPicturePlace);
    }
}