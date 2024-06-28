using Microsoft.AspNetCore.Http;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class PublicationSaveViewModel
    {

        public string IdProfile { get; set; }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? VideoYoutube { get; set; }
        public DateTime DateTime { get; set; }

        public string? ExistingPhotoPath { get; set; }

        public PublicationSaveViewModel()
        {
            this.DateTime = DateTime.Now;
        }

    }
}
