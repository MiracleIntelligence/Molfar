using Molfar.Core.Services;
using Molfar.Models.Services;
using SimpleInjector;
using System;
using System.Text;
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
        }

        private void MolfarAnswered(object sender, Core.Models.MolfarAnswer e)
        {
            AddToConsole(e.Message);
        }

        public void CommandEntryCompleted(object sender, EventArgs e)
        {
            var newText = EntryCommand.Text;
            EntryCommand.Text = String.Empty;
            AddToConsole(newText);
            _molfar.SendMessage(newText);

        }

        private void AddToConsole(string message)
        {
            _sbConsole.AppendLine(message);
            LabelConsole.Text = _sbConsole.ToString();


            ScrollViewMain.ScrollToAsync(0, ScrollViewMain.Height, true);
        }

    }
}
