using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyDiary.Data
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<PhotoModel> Photos { get; set; }
    }
}