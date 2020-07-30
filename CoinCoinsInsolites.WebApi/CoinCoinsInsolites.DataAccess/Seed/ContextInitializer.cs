namespace CoinCoinsInsolites.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ContextInitializer
    {
        public async Task Seed(CoinCoinsContext context, bool isProduction)
        {
            List<IContextSeed> listSeed = new List<IContextSeed>
            {
                new PlaceSeed(context),
                new PicturePlaceSeed(context),
                new UserSeed(context),
                new PictureUserSeed(context),
            };

            foreach (IContextSeed contextSeed in listSeed)
            {
                await contextSeed.Execute(isProduction);
            }
        }
    }
}