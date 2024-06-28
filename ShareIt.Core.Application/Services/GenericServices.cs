using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using ShareIt.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class GenericServices<Entity, SaveViewModel, ViewModel> : IGenericServices<Entity, SaveViewModel, ViewModel>
 where Entity : class
 where ViewModel : class
 where SaveViewModel : class
    {
        readonly IGenericRepository<Entity> _repository;
        readonly IMapper _mapper;

     
        public GenericServices(IGenericRepository<Entity> repository,  IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
          
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            try
            {
                return await _repository.AddAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

            
        }

        public virtual async Task<SaveViewModel> AddSaveViewModel(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);

            
            SaveViewModel Vm = _mapper.Map<SaveViewModel>(await AddAsync(entity));

            return Vm;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                 await _repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
  
            }
        }

        public async Task<List<Entity>> GetAllAsync()
        {
            try
            {
                ICollection<Entity> collection = await _repository.GetAllAsync();

                return collection.ToList() ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
          

            return _mapper.Map<List<ViewModel>>(await GetAllAsync());
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            return _mapper.Map<SaveViewModel>(await GetByIdAsync(id));
        }

        public async Task<ViewModel> GetByIdViewModel(int id)
        {
            

            return _mapper.Map<ViewModel>(await GetByIdAsync(id));
        }

        public virtual async Task<Entity> UpdateAsync(Entity entity, int id)
        {
            try
            {
                return await _repository.UpdateAsync(entity, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public virtual Task UpdateSaveViewModel(SaveViewModel vm, int id)
        {
            return UpdateAsync(_mapper.Map<Entity>(vm), id);
        }

        public async Task<List<ViewModel>> GetAllWithInclude(List<string> properties)
        {
          

            return _mapper.Map<List<ViewModel>>(await _repository.GetAllWithIncludeAsync(properties));
        }


        public string UploadFile(string archive, IFormFile file, string id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/{archive}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }


    }
}
