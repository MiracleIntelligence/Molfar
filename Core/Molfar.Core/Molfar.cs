using Molfar.Core.Models;
using Molfar.Core.Services;
using Molfar.Models;
using Molfar.Models.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Molfar.Core
{
    public class Molfar : IMolfar
    {
        internal const string CMD_LIST = "list";

        internal const string ANS_INIT = "Здоров!";

        public bool Active { get; set; } = true;

        public event EventHandler<IMolfarAnswer> Answered;

        public delegate Task<IMolfarAnswer> MolfarCommand(string message);

        #region
        private Dictionary<string, Type> _knownCommands = new Dictionary<string, Type>();
        #endregion

        Container _defaultContainer;

        public void Initialize(Container container)
        {
            _defaultContainer = container;
            Install<CoreInstaller>();
            SendAnswer(ANS_INIT);
        }

        public void Install<T>() where T : MolfarModuleInstaller
        {
            var module = Activator.CreateInstance<T>() as MolfarModuleInstaller;
            module.Setup(this);
            module.Install();
        }

        public void RegisterCommandProcessor<T>(string key) where T : MolfarCommandProcessor
        {
            _defaultContainer.Register<T>();
            _knownCommands.Add(key, typeof(T));
        }

        public bool SendMessage(string message)
        {
            ProcessMessage(message);

            return true;
        }

        private async void ProcessMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return;
            }

            var nodes = message.Split(' ');

            Type commandProcessorType;

            try
            {
                if (nodes.Length > 0)
                {
                    var commandNode = nodes[0];

                    if (commandNode == MolfarConstants.CMD_PREFIX + CMD_LIST)
                    {
                        ShowListOfCommands();
                        return;
                    }

                    if (IsRunCommand(commandNode))
                    {
                        var settingsService = _defaultContainer.GetInstance<ISettingsService>();
                        message = settingsService.GetSetting(commandNode.Substring(2));
                        ProcessMessage(message);

                        return;
                    }

                    string commandKey;

                    if (IsCommand(commandNode))
                    {
                        commandKey = commandNode.Substring(1);
                    }
                    else
                    {
                        commandKey = CoreInstaller.CMD_ANY_KEY;
                    }

                    if (_knownCommands.ContainsKey(commandKey))
                    {
                        commandProcessorType = _knownCommands[commandKey];
                    }
                    else
                    {
                        commandProcessorType = _knownCommands[CoreInstaller.CMD_ANY_KEY];
                    }


                    var processor = _defaultContainer.GetInstance(commandProcessorType) as MolfarCommandProcessor;

                    if (processor.CanExcecute(message))
                    {
                        var answer = await processor.ExcecuteCommand(message);
                        SendAnswer(answer);
                    }
                    else
                    {
                        SendAnswer("unknown command");
                    }
                }
            }
            catch (Exception ex)
            {
                SendAnswer($"ERROR: {ex.Message}");
            }
        }

        private void ShowListOfCommands()
        {
            foreach (var item in _knownCommands)
            {
                SendAnswer($".{item.Key}");
            }
            return;
        }

        private bool IsCommand(string str)
        {
            return !String.IsNullOrEmpty(str) && str.Length > 1 && str.StartsWith(MolfarConstants.CMD_PREFIX);
        }

        private bool IsRunCommand(string str)
        {
            return !String.IsNullOrEmpty(str) && str.StartsWith(MolfarConstants.CMD_RUN_PREFIX);
        }

        private void SendAnswer(string message)
        {
            Answered?.Invoke(this, new MolfarAnswer(message));
        }

        private void SendAnswer(IMolfarAnswer answer)
        {
            Answered?.Invoke(this, answer);
        }
    }
}

