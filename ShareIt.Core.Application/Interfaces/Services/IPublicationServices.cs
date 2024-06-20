using ShareIt.Core.Application.Interfaces.Services;
using ShareIt.Core.Application.ViewModels.Publication;
using ShareIt.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.Interfaces
{
    public interface IPublicationServices :  IGenericServices<Publication, PublicationSaveViewModel, PublicationViewModel>
    {
    }
}
