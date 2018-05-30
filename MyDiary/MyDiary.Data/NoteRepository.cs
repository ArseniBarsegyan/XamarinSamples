using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace MyDiary.Data
{
    public class NoteRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public NoteRepository(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Note>().Wait();
            _db.CreateTableAsync<PhotoModel>().Wait();
        }

        /// <summary>
        /// Get all notes from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return await _db.Table<Note>().ToListAsync();
        }

        /// <summary>
        /// Get note from database by id
        /// </summary>
        /// <param name="id">Id of the note</param>
        /// <returns></returns>
        public Task<Note> GetNoteAsync(int id)
        {
            return _db.Table<Note>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create (if id = 0) or update note in database
        /// </summary>
        /// <param name="note">Note to be saved</param>
        /// <returns></returns>
        public async Task<int> SaveAsync(Note note)
        {
            if (note.Id != 0)
            {
                return await _db.UpdateAsync(note);
            }
            return await _db.InsertAsync(note);
        }

        /// <summary>
        /// Delete note from database
        /// </summary>
        /// <param name="note">Note to be deleted</param>
        /// <returns></returns>
        public Task<int> DeleteNoteAsync(Note note)
        {
            return _db.DeleteAsync(note);
        }
    }
}