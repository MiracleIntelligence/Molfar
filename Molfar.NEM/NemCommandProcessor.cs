using CSharp2nem.Connectivity;
using CSharp2nem.RequestClients;
using Molfar.Core;
using Molfar.Core.Models;
using System.Threading.Tasks;

namespace Molfar.NEM
{
    public class NemCommandProcessor : MolfarCommandProcessor
    {
        private const string PARAM_HELP = "help";
        private const string PARAM_INFO = "info";
        private const string PARAM_BALANCE = "balance";

        public override bool CanExcecute(string message)
        {
            return true;
        }

        public override Task<IMolfarAnswer> ExcecuteCommand(string message)
        {
            var nodes = message.Split(MolfarConstants.CMD_CHAR_SEPARATOR);
            if (nodes.Length > 1)
            {
                switch (nodes[1])
                {
                    case PARAM_HELP: return ProcessHelpCommand();
                    case PARAM_INFO: return ProcessInfoCommand(nodes[2]);
                    default: return ProcessHelpCommand();
                }
            }
            return ProcessHelpCommand();
        }

        private Task<IMolfarAnswer> ProcessInfoCommand(string address)
        {
            var connection = new Connection();
            connection.SetMainnet();
            var accountClient = new AccountClient(connection);

            var res = accountClient.BeginGetAccountInfoFromAddress(address);
            res.AsyncWaitHandle.WaitOne();
            var data = accountClient.EndGetAccountInfo(res);

            var answer = new MolfarMultirowAnswer();
            answer.AddRow($"Address: {data.Account.Address}");
            answer.AddRow($"Balance: {data.Account.Balance}");
            answer.AddRow($"Vasted : {data.Account.VestedBalance}");

            return Task.FromResult(answer as IMolfarAnswer);
        }

        private Task<IMolfarAnswer> ProcessHelpCommand()
        {
            var answer = new MolfarMultirowAnswer();
            answer.AddRow("=====================================");
            answer.AddRow("Parameters:");
            answer.AddRow("help           : help info.");
            answer.AddRow("info [address] : get info for account");

            return Task.FromResult(answer as IMolfarAnswer);
        }
    }
}
