using Molfar.Models;

namespace Molfar.Spoco
{
    public class MolfarSpocoInstaller : MolfarModuleInstaller
    {
        private string CMD_SPOCO_KEY = "spoco";
        public override void Install()
        {
            RegisterCommandProcessor<MolfarSpocoProcessor>(CMD_SPOCO_KEY);
        }
    }
}
