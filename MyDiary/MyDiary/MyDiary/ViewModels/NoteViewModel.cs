using System;
using System.Collections.Generic;

namespace MyDiary.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string FullDescription { get; set; }

        public List<PhotoViewModel> Photos { get; set; }
    }
}