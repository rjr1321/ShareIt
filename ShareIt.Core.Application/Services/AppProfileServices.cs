using AutoMapper;
using ShareIt.Core.Application.Interfaces.Repos;
using ShareIt.Core.Application.Interfaces.Services;
using ShareIt.Core.Application.ViewModels.AppProfile;
using ShareIt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.Services
{
    public class AppProfileServices : GenericServices<AppProfile, AppProfileSaveViewModel, AppProfileViewModel>, IAppProfileServices
    {
        public readonly IAppProfileRepository _repository;

        public readonly IMapper _mapper;

        public AppProfileServices(IAppProfileRepository repository, IMapper mapper): base (repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
        
        }
    }
}
