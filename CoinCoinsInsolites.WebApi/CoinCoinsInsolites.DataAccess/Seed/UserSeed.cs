namespace CoinCoinsInsolites.DataAccess.Seed
{
    using CoinCoinsInsolites.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserSeed : IContextSeed
    {
        public UserSeed(CoinCoinsContext context)
        {
            this.Context = context;
        }

        public CoinCoinsContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Users.Any() && !isProduction)
            {
                DateTime createdDate = DateTime.UtcNow;
                List<UserEntity> users = new List<UserEntity>
                {
                    new UserEntity
                    {
                        Mail = "admin@coincoin.com",
                        CreatedDate = createdDate,
                        Password = "Bonjour50!",
                        Pseudonym = "Admin Admin",
                    },
                };

                await this.Context.Users.AddRangeAsync(users);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}