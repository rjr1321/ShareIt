using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class PublicationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public string? VideoYoutube { get; set; }
        public string? Username { get; set; }
        public AppProfile Profile { get; set; }
        public List<CommentViewModel>? Comments { get; set; }
        public DateTime DateTime { get; set; }
        public bool Edited { get; set; }
        public CommentSaveViewModel? svm { get; set; }
    }
}
