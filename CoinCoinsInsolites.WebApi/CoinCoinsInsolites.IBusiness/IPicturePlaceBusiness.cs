namespace CoinCoinsInsolites.IBusiness
{
    using CoinCoinsInsolites.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPicturePlaceBusiness : IDisposable
    {
        Task<KeyValuePair<bool, List<PicturePlace>>> CreateOrUpdateFromList(int id, List<PicturePlace> listPicturePlace);

        Task DeleteAllFromPlaceId(int PlaceId);

        Task<List<PicturePlace>> FindAllFromListId(List<int> listId);

        Task<List<PicturePlace>> FindAllFromPlaceId(int placeId);

        KeyValuePair<bool, Place> ValidateList(List<PicturePlace> listPicturePlace, Place place);
    }
}