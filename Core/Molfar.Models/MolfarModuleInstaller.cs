using Molfar.Core;

namespace Molfar.Models
{
    public abstract class MolfarModuleInstaller
    {
        IMolfar _molfar;
        
        public void Setup(IMolfar molfar)
        {
            _molfar = molfar;
        }

        public abstract void Install();
        
        protected void RegisterCommandProcessor<T>(string key) where T : MolfarCommandProcessor
        {
            _molfar.RegisterCommandProcessor<T>(key);
        }
    }
}
