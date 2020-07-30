namespace CoinCoinsInsolites.DataAccess.Seed
{
    using System.Threading.Tasks;

    public interface IContextSeed
    {
        CoinCoinsContext Context { get; set; }

        Task Execute(bool isProduction);
    }
}