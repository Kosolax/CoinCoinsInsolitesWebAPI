namespace CoinCoinsInsolites.IDataAccess
{
    using CoinCoinsInsolites.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaceDataAccess : IBaseDataAccess<PlaceEntity>
    {
        Task<List<PlaceEntity>> FindMostRecentPlaceEntity(int count);
    }
}