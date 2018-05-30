using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyDiary.Models;

namespace MyDiary.Extensions
{
    public static class ObservableExtension
    {
        public static ObservableCollection<Note> ToObservableCollection(this IEnumerable<Note> source)
        {
            return new ObservableCollection<Note>(source);
        }
    }
}