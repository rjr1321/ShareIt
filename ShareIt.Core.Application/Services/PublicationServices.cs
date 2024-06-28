using AutoMapper;

using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class PublicationServices : GenericServices<Publication, PublicationSaveViewModel, PublicationViewModel>, IPublicationServices
    {
        public readonly IPublicationRepository _repository;

        public readonly IMapper _mapper;

  

        public PublicationServices(IPublicationRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public override async Task<PublicationSaveViewModel> AddSaveViewModel(PublicationSaveViewModel vm)
        {
          
            Publication publication = _mapper.Map<Publication>(vm);

            Publication addedPublication = await AddAsync(publication);

            if (vm.Photo != null)
            {
                publication.Photo = UploadFile("Publications", vm.Photo, addedPublication.Photo);

               await UpdateAsync(addedPublication, addedPublication.Id);
            }
          
            PublicationSaveViewModel savedVm = _mapper.Map<PublicationSaveViewModel>(addedPublication);

            return savedVm;
        
        }


        public override async Task<PublicationSaveViewModel> UpdateSaveViewModel(PublicationSaveViewModel vm, int id)
        {
            Publication publication = _mapper.Map<Publication>(vm);

            if (vm.Photo != null)
            {
                publication.Photo = UploadFile("Publications", vm.Photo, id.ToString());
            }

            publication.Edited = true;

             await UpdateAsync(publication, id);

            return _mapper.Map<PublicationSaveViewModel>(publication);
        }

        public void DeletePhotoFromStorage(int Id)
        {
            string basePath = $"/Images/Publications/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
        }




    }
}
