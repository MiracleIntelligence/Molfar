using Molfar.CoinCap;
using Molfar.Core.Services;
using Molfar.Models.Services;
using Molfar.NEM;
using Molfar.Notes;
using Molfar.Spoco;
using SimpleInjector;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Molfar
{
    public partial class MainPage : ContentPage
    {
        private Molfar.Core.Molfar _molfar;
        private StringBuilder _sbConsole = new StringBuilder();
        public MainPage()
        {
            InitializeComponent();

            Container container = new Container();

            container.Register<ISettingsService, SettingsService>();
            container.Register<IDatabaseService, DatabaseService>();


            _molfar = new Core.Molfar();
            _molfar.Answered += MolfarAnswered;

            _molfar.Initialize(container);
            _molfar.Install<MolfarNotesInstaller>();
            _molfar.Install<MolfarCoinCapInstaller>();
            _molfar.Install<NemInstaller>();
            _molfar.Install<MolfarSpocoInstaller>();
        }

        private async void MolfarAnswered(object sender, Core.Models.IMolfarAnswer e)
        {
            foreach (var row in e.GetAnswer())
            {
                await AddToConsole(row);
            }
        }

        public async void CommandEntryCompleted(object sender, EventArgs e)
        {
            var newText = EntryCommand.Text;
            EntryCommand.Text = String.Empty;
            await AddToConsole(newText);
            _molfar.SendMessage(newText);

        }

        private async Task AddToConsole(string message)
        {
            _sbConsole.AppendLine(message);
            LabelConsole.Text = _sbConsole.ToString();

            await Task.Delay(50);
            await ScrollViewMain.ScrollToAsync(0, ScrollViewMain.Height, true);
        }
    }
}
