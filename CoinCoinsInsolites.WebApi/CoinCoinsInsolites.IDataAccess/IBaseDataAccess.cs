namespace CoinCoinsInsolites.IDataAccess
{
    using CoinCoinsInsolites.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseDataAccess<T> : IDisposable
            where T : IBaseEntity
    {
        Task<T> Create(T itemToCreate);

        Task Delete(params object[] keyValues);

        Task<T> Find(params object[] keyValues);

        Task<List<T>> List();

        Task<T> Update(T itemToUpdate, params object[] keyValues);
    }
}