using Molfar.Core.Models;
using System.Threading.Tasks;

namespace Molfar.Core.Processors
{
    public class TalkingProcessor : MolfarCommandProcessor
    {
        public override bool CanExcecute(string message)
        {
            return true;
        }

        public override Task<MolfarAnswer> ExcecuteCommand(string message)
        {
            return Task.FromResult(new MolfarAnswer($"UNKNOWN COMMAND: {message}"));
        }
    }
}
