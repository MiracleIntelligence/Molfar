using Molfar.Core;
using Molfar.Core.Models;
using Molfar.Core.Services;
using Molfar.Notes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Molfar.Notes
{
    public class MolfarNotesProcessor : MolfarCommandProcessor
    {
        private const string PARAM_RANDOM = "random";
        private const string PARAM_AT = "at";
        private const string PARAM_SAVE = "save";
        private DatabaseService _databaseService;

        public MolfarNotesProcessor(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _databaseService.Connection.CreateTable<Note>();
        }

        public override bool CanExcecute(string message)
        {
            return true;
        }

        public override Task<IMolfarAnswer> ExcecuteCommand(List<string> nodes)
        {
            if (nodes.Count > 1)
            {
                switch (nodes[1])
                {
                    case PARAM_RANDOM: return GetRandomNote();
                    case PARAM_AT: return GetNote(Int32.Parse(nodes[2]));
                    case PARAM_SAVE: return SaveNote(nodes);
                    default: return GetAllNotes();
                }
            }
            else
            {
                return GetAllNotes();
            }
        }

        private Task<IMolfarAnswer> SaveNote(List<string> nodes)
        {
            var arg1 = nodes[1];
            var arg2 = nodes[2];
            var arg3 = nodes[3];
            var str = nodes[4];


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
