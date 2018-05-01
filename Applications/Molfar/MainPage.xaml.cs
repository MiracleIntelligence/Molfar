using Molfar.Core.Services;
using Molfar.Models.Services;
using Molfar.Notes;
using SimpleInjector;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Molfar
{
    public partial class MainPage : ContentPage
    {
        Molfar.Core.Molfar _molfar;
        StringBuilder _sbConsole = new StringBuilder();
        public MainPage()
        {
            InitializeComponent();

            Container container = new Container();

            container.Register<ISettingsService, SettingsService>();
            container.Register<DatabaseService>();


            _molfar = new Core.Molfar();
            _molfar.Answered += MolfarAnswered;

            _molfar.Initialize(container);
            _molfar.Install<MolfarNotesInstaller>();
        }

        private async void MolfarAnswered(object sender, Core.Models.MolfarAnswer e)
        {
            await AddToConsole(e.Message);
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
