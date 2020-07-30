namespace CoinCoinsInsolites.DataAccess
{
    using CoinCoinsInsolites.Entities;
    using CoinCoinsInsolites.IDataAccess;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserDataAccess : BaseDataAccess<UserEntity>, IUserDataAccess
    {
        public UserDataAccess(CoinCoinsContext context)
            : base(context)
        {
        }

        public async Task<UserEntity> FindByMail(string mail)
        {
            return await this.Context.Users
                .Select(userEntity => new UserEntity
                {
                    Id = userEntity.Id,
                    CreatedDate = userEntity.CreatedDate,
                    Mail = userEntity.Mail,
                    Password = userEntity.Password,
                    Pseudonym = userEntity.Pseudonym,
                    UpdatedDate = userEntity.UpdatedDate
                })
                .Where(userEntity => userEntity.Mail == mail)
                .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> FindByMailAndPassword(string mail, string password)
        {
            return await this.Context.Users
                .Select(userEntity => new UserEntity
                {
                    Id = userEntity.Id,
                    CreatedDate = userEntity.CreatedDate,
                    Mail = userEntity.Mail,
                    Password = userEntity.Password,
                    Pseudonym = userEntity.Pseudonym,
                    UpdatedDate = userEntity.UpdatedDate
                })
                .Where(userEntity => userEntity.Mail == mail && userEntity.Password == password)
                .FirstOrDefaultAsync();
        }
    }
}