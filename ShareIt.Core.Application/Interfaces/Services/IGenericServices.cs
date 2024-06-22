using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public interface IGenericServices<Entity, SaveViewModel, ViewModel>
where Entity : class
where ViewModel : class
where SaveViewModel : class
    {
        Task<Entity> UpdateAsync(Entity entity, int id);
        Task<Entity> AddAsync(Entity entity);
        Task DeleteAsync(int id);
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
        Task UpdateSaveViewModel(SaveViewModel vm, int id);
        Task<SaveViewModel> AddSaveViewModel(SaveViewModel vm);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<ViewModel> GetByIdViewModel(int id);
        Task<List<ViewModel>> GetAllViewModel();
        Task<List<ViewModel>> GetAllWithInclude(List<string> properties);
    }

}
