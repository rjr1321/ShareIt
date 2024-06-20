﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Domain.Entities
{
    public class Publication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Photo>? Photos { get; set; }
        public string? VideoYoutube { get; set; }
        public string IdProfile { get; set; }
        public AppProfile Profile { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public DateTime DateTime { get; set; }

        public bool Edited { get; set; }

        public Publication() 
        {
        this.Photos = new Collection<Photo>();
        this.Comments = new List<Comment>();
        this.DateTime = DateTime.Now;
        this.Edited = false;
        
        }
    }
}