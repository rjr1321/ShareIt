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

        public readonly IPhotoRepository _photoRepository;

        public PublicationServices(IPublicationRepository repository, IMapper mapper, IPhotoRepository photoRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _photoRepository = photoRepository;
        }

        public virtual async Task<PublicationSaveViewModel> AddSaveViewModel(PublicationSaveViewModel vm)
        {




         return await base.AddSaveViewModel(vm);




        }

        public virtual async Task UpdateSaveViewModel(PublicationSaveViewModel vm, int id)
        {
           await base.UpdateSaveViewModel(vm, id);
        }


    }
}
