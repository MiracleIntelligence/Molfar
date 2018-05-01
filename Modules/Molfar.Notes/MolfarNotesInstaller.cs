using CommonServiceLocator;
using Molfar.Models;
using Molfar.Models.Services;
using Molfar.Notes.Models;

namespace Molfar.Notes
{
    public class MolfarNotesInstaller : MolfarModuleInstaller
    {
        private string CMD_NOTES_KEY = "notes";
        public override void Install()
        {
            RegisterCommandProcessor<MolfarNotesProcessor>(CMD_NOTES_KEY);
        }
    }
}
