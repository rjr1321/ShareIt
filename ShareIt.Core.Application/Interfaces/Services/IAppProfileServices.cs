using ShareIt.Core.Application.ViewModels.AppProfile;
using ShareIt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.Interfaces.Services
{
    public interface IAppProfileServices : IGenericServices<AppProfile, AppProfileSaveViewModel, AppProfileViewModel>
    {
    }
}
