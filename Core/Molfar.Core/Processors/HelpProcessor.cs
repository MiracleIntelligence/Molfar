using Molfar.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Molfar.Core.Processors
{
    public class HelpProcessor : MolfarCommandProcessor
    {
        public override bool CanExcecute(string message)
        {
            return true;
        }

        public override Task<IMolfarAnswer> ExcecuteCommand(List<string> nodes)
        {
            var response = new MolfarMultirowAnswer();
            response.AddRow($"{MolfarConstants.CMD_PREFIX}[cmd] [param1] [param2] ...");
            response.AddRow($"Command prefix : \"{MolfarConstants.CMD_PREFIX}\"");


            return Task.FromResult(response as IMolfarAnswer);
        }
    }
}
