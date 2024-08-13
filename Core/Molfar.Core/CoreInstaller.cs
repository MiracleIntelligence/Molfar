using Molfar.Core.Processors;
using Molfar.Models;

namespace Molfar.Core
{
    public class CoreInstaller : MolfarModuleInstaller
    {
        internal const string CMD_ANY_KEY = ".";
        internal const string CMD_GET_SETTINGS_KEY = "get";
        internal const string CMD_SET_SETTINGS_KEY = "set";
        internal const string CMD_HELP_SETTINGS_KEY = "help";
        internal const string CMD_MOLFAR_KEY = "molfar";

        public override void Install()
        {
            RegisterCommandProcessor<TalkingProcessor>(CMD_ANY_KEY);
            RegisterCommandProcessor<GetSettingsProcessor>(CMD_GET_SETTINGS_KEY);
            RegisterCommandProcessor<SetSettingsProcessor>(CMD_SET_SETTINGS_KEY);
            RegisterCommandProcessor<HelpProcessor>(CMD_HELP_SETTINGS_KEY);
        }
    }
}
