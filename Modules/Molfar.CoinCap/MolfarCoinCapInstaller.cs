using Molfar.Models;

namespace Molfar.CoinCap
{
    public class MolfarCoinCapInstaller : MolfarModuleInstaller
    {
        private const string CMD_COINCAP_KEY = "coincap";
        public override void Install()
        {
            RegisterCommandProcessor<CoinCapProcessor>(CMD_COINCAP_KEY);
        }
    }
}
