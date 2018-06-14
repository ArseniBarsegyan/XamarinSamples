﻿using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyDiary.Models
{
    [Table("Notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PhotoModel> Photos { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<VideoModel> Videos { get; set; }
    }
}