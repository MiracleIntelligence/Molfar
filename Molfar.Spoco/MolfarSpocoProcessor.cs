using Microsoft.WindowsAzure.MobileServices;
using Molfar.Core;
using Molfar.Core.Models;
using Molfar.Core.Services;
using Molfar.Spoco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Molfar.Spoco
{
    public class MolfarSpocoProcessor : MolfarCommandProcessor
    {
        private const string PARAM_RANDOM = "random";
        private const string PARAM_AT = "at";
        private const string PARAM_SAVE = "save";
        private const string PARAM_ALL = "all";
        private const string PARAM_TODAY = "today";
        private const string PARAM_HOUR = "hour";
        private const string PARAM_ADD = "add";
        private DatabaseService _databaseService;

        public static MobileServiceClient MobileService =
    new MobileServiceClient(
    "https://elspoco.azurewebsites.net"
);

        public MolfarSpocoProcessor(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _databaseService.Connection.CreateTable<TodoItem>();
        }

        public override bool CanExcecute(string message)
        {
            return true;
        }

        public override Task<IMolfarAnswer> ExcecuteCommand(string message)
        {
            var nodes = message.Split(' ');

            if (nodes.Length > 1)
            {
                switch (nodes[1])
                {
                    case PARAM_RANDOM: return GetRandomNote();
                    case PARAM_AT: return GetNote(Int32.Parse(nodes[2]));
                    case PARAM_SAVE: return SaveNote(message);
                    case PARAM_ALL: return GetNotes();
                    case PARAM_TODAY: return GetNotes(PARAM_TODAY);
                    case PARAM_HOUR: return GetNotes(PARAM_HOUR);
                    case PARAM_ADD: return AddNote(nodes[2]);
                    default: return GetNotes();
                }
            }
            else
            {
                return GetNotes();
            }
        }

        private Task<IMolfarAnswer> SaveNote(string message)
        {
            var parts = message.Split(' ');
            var arg1 = parts[1];
            var arg2 = parts[2];
            var arg3 = parts[3];
            var str = message.Substring(message.IndexOf(arg3));


            var answer = _databaseService.Connection.Insert(new TodoItem { Id = arg2, Text = str });
            return Task.FromResult(new MolfarAnswer($"SAVED: {arg2}") as IMolfarAnswer);
        }

        private async Task<IMolfarAnswer> GetNotes(string param = null)
        {
            var items = new List<KnockItem>();
            switch (param)
            {
                case PARAM_TODAY:
                    {
                        items = await MobileService.GetTable<KnockItem>().Where(i => i.CreatedAt > DateTimeOffset.Now.Date).ToListAsync();

                    }; break;
                case PARAM_HOUR:
                    {
                        items = await MobileService.GetTable<KnockItem>().Where(i => i.CreatedAt > DateTimeOffset.Now.AddHours(-1)).ToListAsync();

                    }; break;
                default:
                    {
                        items = await MobileService.GetTable<KnockItem>().ToListAsync();
                    }; break;
            }

            return new MolfarMultirowAnswer(items?.Select(i => i.CreatedAt.ToString() + "  " + i.Type));
        }

        private async Task<IMolfarAnswer> AddNote(string text)
        {
            bool result = true;
            try
            {
                var note = new KnockItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = text,
                    Type = "Test"
                };
                await MobileService.GetTable<KnockItem>().InsertAsync(note);
                result = true;
            }
            catch
            {
                result = false;
            }

            return new MolfarAnswer(result.ToString());

        }

        private async Task<IMolfarAnswer> GetNote(int v)
        {
            var note = _databaseService.Connection.Table<TodoItem>().ElementAt(v);
            return new MolfarAnswer($"{note.Id} - {note.Text}") as IMolfarAnswer;
        }

        private Task<IMolfarAnswer> GetAllSpoco()
        {
            var count = _databaseService.Connection.Table<TodoItem>().Count();
            return Task.FromResult(new MolfarAnswer($"COUNT: {count}") as IMolfarAnswer);
        }

        private Task<IMolfarAnswer> GetRandomNote()
        {
            var count = _databaseService.Connection.Table<TodoItem>().Count();
            var random = new Random();
            var note = _databaseService.Connection.Table<TodoItem>().ElementAt(random.Next(count));
            return Task.FromResult(new MolfarAnswer($"{note.Text} - {note.Text}") as IMolfarAnswer);
        }
    }
}
