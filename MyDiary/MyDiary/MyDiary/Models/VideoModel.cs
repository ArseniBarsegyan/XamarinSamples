using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyDiary.Models
{
    /// <summary>
    /// Store filepath to videos.
    /// </summary>
    [Table("Photos")]
    public class VideoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Path { get; set; }

        [ForeignKey(typeof(Note))]
        public int NoteId { get; set; }
    }
}