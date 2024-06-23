using Molfar.Core.Models;
using Molfar.Models.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Molfar.Core.Processors
{
    public class GetSettingsProcessor : MolfarCommandProcessor
    {
        private readonly ISettingsService _settingsService;

        public GetSettingsProcessor(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public override bool CanExcecute(string message)
        {
            var parts = message.Split(' ');
            var arg1 = parts[1];

            return !String.IsNullOrEmpty(arg1);
        }

        public override Task<IMolfarAnswer> ExcecuteCommand(List<string> nodes)
        {
            var arg1 = nodes[1];

            var value = _settingsService.GetSetting(arg1);

            return Task.FromResult(new MolfarAnswer($"{arg1} : {value}") as IMolfarAnswer);
        }
    }
}
