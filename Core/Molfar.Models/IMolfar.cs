using Molfar.Core;

namespace Molfar.Models
{
    public interface IMolfar
    {
        void RegisterCommandProcessor<T>(string key) where T : MolfarCommandProcessor;
    }
}
