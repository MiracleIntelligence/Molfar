using Molfar.Console.Services;
using Molfar.Core;
using Molfar.Core.Services;
using Molfar.Models.Services;
using Molfar.Notes;

using SimpleInjector;

using System;
using System.Text;

namespace Molfar.Console
{
    internal class Program
    {
        private static Core.Molfar _molfar;

        private static void Main(string[] args)
        {
            System.Console.OutputEncoding = Encoding.UTF8;

            Container container = new Container();

            container.Register<ISettingsService, SettingsService>();
            container.Register<IDatabaseService, DatabaseService>();

            _molfar = new Core.Molfar();
            _molfar.Answered += OnMolfarAnswered;

            _molfar.Initialize(container);
            _molfar.Install<MolfarNotesInstaller>();

            _molfar.SendMessage($".get {MolfarConstants.KEY_LAST_VISIT}");
            _molfar.SendMessage($".set {MolfarConstants.KEY_LAST_VISIT} {DateTime.Now.ToString("dd MMM yyyy HH:mm")}");

            while (_molfar.Active)
            {
                _molfar.SendMessage(System.Console.ReadLine());
            }
        }

        private static void OnMolfarAnswered(object sender, Core.Models.IMolfarAnswer e)
        {
            foreach (var row in e.GetAnswer())
            {
                System.Console.WriteLine(row);
            }
        }
    }
}
