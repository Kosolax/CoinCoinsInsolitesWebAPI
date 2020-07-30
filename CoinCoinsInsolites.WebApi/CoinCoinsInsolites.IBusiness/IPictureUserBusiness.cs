namespace CoinCoinsInsolites.IBusiness
{
    using CoinCoinsInsolites.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPictureUserBusiness : IDisposable
    {
        Task<KeyValuePair<bool, List<PictureUser>>> CreateOrUpdateFromList(int id, List<PictureUser> listPictureUser);

        Task DeleteAllFromUserId(int userId);

        Task<List<PictureUser>> FindAllFromUserId(int userId);

        KeyValuePair<bool, User> ValidateList(List<PictureUser> pictureUsers, User user);

        Task<List<PictureUser>> FindAllFromListId(List<int> listId);
    }
}