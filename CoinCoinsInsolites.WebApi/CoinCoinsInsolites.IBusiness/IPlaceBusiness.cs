namespace CoinCoinsInsolites.IBusiness
{
    using CoinCoinsInsolites.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaceBusiness : IDisposable
    {
        Task<KeyValuePair<bool, Place>> Create(Place itemToCreate);

        Task<KeyValuePair<bool, Place>> Delete(int id);

        Task<Place> Get(int id);

        Task<List<Place>> List();

        Task<KeyValuePair<bool, Place>> Update(int id, Place itemToUpdate);

        Task<List<Place>> FindMostRecentPlace(int count);
    }
}