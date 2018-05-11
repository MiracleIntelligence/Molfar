using Molfar.Models;
using System;

namespace Molfar.NEM
{
    public class NemInstaller : MolfarModuleInstaller
    {
        public const string CMD_NEM_KEY = "nem";
        public override void Install()
        {
            RegisterCommandProcessor<NemCommandProcessor>(CMD_NEM_KEY);
        }
    }
}
