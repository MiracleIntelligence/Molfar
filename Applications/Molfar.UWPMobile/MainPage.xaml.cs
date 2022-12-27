using Molfar.Core;
using Molfar.Core.Services;
using Molfar.Models.Services;
using Molfar.Notes;
using Molfar.UWPMobile.Services;
using SimpleInjector;
using System;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Molfar.UWPMobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Molfar.Core.Molfar _molfar;
        private StringBuilder _sbConsole = new StringBuilder();
        public MainPage()
        {
            this.InitializeComponent();

            Container container = new Container();

            container.Register<ISettingsService, SettingsService>();
            container.Register<IDatabaseService, DatabaseService>();


            _molfar = new Core.Molfar();
            _molfar.Answered += MolfarAnswered;

            _molfar.Initialize(container);
            _molfar.Install<MolfarNotesInstaller>();

            _molfar.SendMessage($".get {MolfarConstants.KEY_LAST_VISIT}");
            _molfar.SendMessage($".set {MolfarConstants.KEY_LAST_VISIT} {DateTime.Now.ToString("dd MMM yyyy HH:mm")}");
        }

        private async void MolfarAnswered(object sender, Core.Models.IMolfarAnswer e)
        {
            foreach (var row in e.GetAnswer())
            {
                await AddToConsole(row);
            }
        }

        private async Task AddToConsole(string message)
        {
            _sbConsole.AppendLine(message);
            LabelConsole.Text = _sbConsole.ToString();

            await Task.Delay(50);
            ScrollViewMain.ChangeView(0, LabelConsole.ActualHeight, null);
        }

        private async void EntryCommandKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                var newText = EntryCommand.Text;
                EntryCommand.Text = String.Empty;
                await AddToConsole(newText);
                _molfar.SendMessage(newText);
            }

        }
    }
}
