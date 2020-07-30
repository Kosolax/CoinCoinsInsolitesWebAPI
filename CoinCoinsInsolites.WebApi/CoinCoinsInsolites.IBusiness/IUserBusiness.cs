namespace CoinCoinsInsolites.IBusiness
{
    using CoinCoinsInsolites.BusinessObject;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserBusiness : IDisposable

    {
        Task<KeyValuePair<bool, User>> Authenticate(string mail, string password);

        Task<KeyValuePair<bool, User>> Create(User itemToCreate);

        Task<KeyValuePair<bool, User>> Delete(int id);

        Task<User> Get(int id);

        Task<List<User>> List();

        Task<KeyValuePair<bool, User>> Update(int id, User itemToUpdate);
    }
}