using Alvarez_TareaMVVM.AlvarezModels;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alvarez_TareaMVVM.AlvarezViewsModels
{
    class AlvarezNotesViewModels : IQueryAttributable
    {

        public ObservableCollection<AlvarezViewsModels.AlvarezNoteViewModels> AllNotes { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public AlvarezNotesViewModels()
        {
            AllNotes = new ObservableCollection<AlvarezViewsModels.AlvarezNoteViewModels>(AlvarezModels.AlvarezNotes.LoadAll().Select(n => new AlvarezNoteViewModels(n)));
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<AlvarezViewsModels.AlvarezNoteViewModels>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(AlvarezViews.AlvarezNotePage));
        }

        private async Task SelectNoteAsync(AlvarezViewsModels.AlvarezNoteViewModels note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(AlvarezViews.AlvarezNotePage)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                AlvarezNoteViewModels matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                AlvarezNoteViewModels matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }

                // If note isn't found, it's new; add it.
                else
                    AllNotes.Insert(0, new AlvarezNoteViewModels(AlvarezModels.AlvarezNotes.Load(noteId)));
            }
        }
    }
}
