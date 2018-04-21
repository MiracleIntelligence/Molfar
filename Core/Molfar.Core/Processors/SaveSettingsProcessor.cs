using Molfar.Core.Models;
using Molfar.Models.Services;
using System;
using System.Threading.Tasks;

namespace Molfar.Core.Processors
{
    public class SaveSettingsProcessor : MolfarCommandProcessor
    {
        ISettingsService _settingsService;
        public SaveSettingsProcessor(ISettingsService settingsService)
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

        public override Task<MolfarAnswer> ExcecuteCommand(string message)
        {
            var parts = message.Split(' ');
            var arg1 = parts[1];
            var arg2 = parts[2];
            var str = message.Substring(message.IndexOf(arg2));
            var answer = _settingsService.SaveSetting(arg1, str);
            return Task.FromResult(new MolfarAnswer($"SAVED: {arg1} : {str}"));
        }
    }
}
