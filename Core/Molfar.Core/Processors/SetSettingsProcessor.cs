using Molfar.Core.Models;
using Molfar.Models.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Molfar.Core.Processors
{
    public class SetSettingsProcessor : MolfarCommandProcessor
    {
        private ISettingsService _settingsService;
        public SetSettingsProcessor(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public override bool CanExcecute(string message)
        {
            var parts = message.Split(' ');
            var arg1 = parts[1];
            var arg2 = parts[2];

            return !String.IsNullOrEmpty(arg1) && !String.IsNullOrEmpty(arg2);
        }

        public override Task<IMolfarAnswer> ExcecuteCommand(List<string> nodes)
        {
            var arg1 = nodes[1];
            var arg2 = nodes[2];


            var str = nodes[3];
            var answer = _settingsService.SaveSetting(arg1, str);
            return Task.FromResult(new MolfarAnswer($"SAVED: {arg1} : {str}") as IMolfarAnswer);
        }
    }
}
