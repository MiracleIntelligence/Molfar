using Molfar.Core.Models;
using System.Threading.Tasks;

namespace Molfar.Core.Processors
{
    public class HelpProcessor : MolfarCommandProcessor
    {
        public override bool CanExcecute(string message)
        {
            return true;
        }

        public override Task<IMolfarAnswer> ExcecuteCommand(string message)
        {
            var responseString = $".[cmd] [param1] [param2] ...";
            var response = new MolfarAnswer(responseString);
            return Task.FromResult(response as IMolfarAnswer);
        }
    }
}
