using Molfar.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Molfar.Core
{
    public abstract class MolfarCommandProcessor
    {
        public abstract bool CanExcecute(string message);
        public abstract Task<IMolfarAnswer> ExcecuteCommand(List<string> message);
        protected Task<IMolfarAnswer> Say(string message)
        {
            return Task.FromResult(new MolfarAnswer(message) as IMolfarAnswer);
        }

    }
}
