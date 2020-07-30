namespace CoinCoinsInsolites.IDataAccess
{
    using CoinCoinsInsolites.Entities;
    using System.Threading.Tasks;

    public interface IUserDataAccess : IBaseDataAccess<UserEntity>
    {
        Task<UserEntity> FindByMail(string mail);

        Task<UserEntity> FindByMailAndPassword(string mail, string password);
    }
}