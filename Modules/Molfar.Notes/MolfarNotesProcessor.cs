using CommonServiceLocator;
using Molfar.Core;
using Molfar.Core.Models;
using Molfar.Core.Services;
using Molfar.Notes.Models;
using System;
using System.Threading.Tasks;

namespace Molfar.Notes
{
    public class MolfarNotesProcessor : MolfarCommandProcessor
    {
        private const string PARAM_RANDOM = "random";
        private const string PARAM_AT = "at";
        private const string PARAM_SAVE = "save";
        IDatabaseService _databaseService;

        public MolfarNotesProcessor(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _databaseService.Connection.CreateTable<Note>();
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
                    default: return GetAllNotes();
                }
            }
            else
            {
                return GetAllNotes();
            }
        }

        private Task<IMolfarAnswer> SaveNote(string message)
        {
            var parts = message.Split(' ');
            var arg1 = parts[1];
            var arg2 = parts[2];
            var arg3 = parts[3];
            var str = message.Substring(message.IndexOf(arg3));


            var answer = _databaseService.Connection.Insert(new Note { Title = arg2, Text = str });
            return Task.FromResult(new MolfarAnswer($"SAVED: {arg2}") as IMolfarAnswer);
        }

        private Task<IMolfarAnswer> GetNote(int v)
        {
            var note = _databaseService.Connection.Table<Note>().ElementAt(v);
            return Task.FromResult(new MolfarAnswer($"{note.Title} - {note.Text}") as IMolfarAnswer);
        }

        private Task<IMolfarAnswer> GetAllNotes()
        {
            var count = _databaseService.Connection.Table<Note>().Count();
            return Task.FromResult(new MolfarAnswer($"COUNT: {count}") as IMolfarAnswer);
        }

        private Task<IMolfarAnswer> GetRandomNote()
        {
            var count = _databaseService.Connection.Table<Note>().Count();
            var random = new Random();
            var note = _databaseService.Connection.Table<Note>().ElementAt(random.Next(count));
            return Task.FromResult(new MolfarAnswer($"{note.Title} - {note.Text}") as IMolfarAnswer);
        }
    }
}
