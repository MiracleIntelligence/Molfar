using GalaSoft.MvvmLight.Ioc;
using Molfar.Console.Services;
using Molfar.Core.Services;
using Molfar.Models.Services;
using Molfar.Notes;
using SimpleInjector;
using System.Text;
namespace Molfar.Console
{
    class Program
    {
        static Core.Molfar _molfar;
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = Encoding.UTF8;

            Container container = new Container();

            container.Register<ISettingsService, SettingsService>();
            container.Register<DatabaseService>();

            _molfar = new Core.Molfar();
            _molfar.Answered += OnMolfarAnswered;

            _molfar.Initialize(container);
            _molfar.Install<MolfarNotesInstaller>();

            while (_molfar.Active)
            {
                _molfar.SendMessage(System.Console.ReadLine());
            }
            //System.Console.WriteLine("Hello World!");
            //System.Console.ReadKey(true);
        }

        private static void OnMolfarAnswered(object sender, Core.Models.MolfarAnswer e)
        {
            System.Console.WriteLine(e.Message);
        }
    }
}
